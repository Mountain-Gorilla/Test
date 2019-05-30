using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

	private Move g_Move;       // 移動
	private const float f_JumpForce = 1300.0f;    // ジャンプ力
	private bool b_JumpFlag;

	private Vector3 v_PositionSave;  // 落ちた時用

	// パラメータ設定
	private HP g_HP;
	private const float cf_HPMax = 600.0f;  // 最大値
	private const float cf_Damage = 10.0f;  // ダメージ量
	private bool b_AliveFlag = false;

	// 横移動
	private const float cf_MaxSpeed = 0.15f;       // 最大値
	private const float cf_AccelOnce = 0.15f;       // 一度の加速量(未使用)
	private const float cf_DecelOnce = 0.15f;       // 一度の減速量(未使用)
	private enum Direction { Left, Right };  // 自機が向いている方向
	private int n_LeftRightFlag = (int)Direction.Right;  // 方向の判定

	// アニメーション
	private Animator an_Mortion;
	private enum Mortion { Stay, Move, Jump, Fall };     // アニメーション管理
	private int n_MortionState = (int)Mortion.Stay; // 現在のアニメーションの管理変数

	private Rigidbody2D g_right2D; // ジャンプの処理用

	/*=============================*/
	// エフェクト
	/*=============================*/
	private GameObject g_Damege;
	private FlushController g_DamegeScript;

	private GameObject g_Jump;
	private JumpEffect je_Effect;

	private AudioSource[] as_Sound;

	// カメラの移動用フラグ
	private bool b_CameraMoveFlag;
	//private bool 



	/*=======================*/
	//  ゲッター
	/*=======================*/
	public bool IsAlive() { return b_AliveFlag; }

	/*======================*/
	//  初期化
	/*======================*/
	void Awake()
	{
		// 初期座標を設定
		transform.position = new Vector3(-175.3f, -42.0f, 0.0f);

		b_JumpFlag = false;
		b_CameraMoveFlag = false;

		g_Move = new Move();

		g_HP = GetComponent<HP>();
		g_HP.SetHP(cf_HPMax);
		b_AliveFlag = true;

		an_Mortion = GetComponent<Animator>();
		n_MortionState = (int)Mortion.Stay;

		g_right2D = GetComponent<Rigidbody2D>();

		n_LeftRightFlag = (int)Direction.Right;

		g_Damege = GameObject.Find("DamageEffect");
		g_DamegeScript = g_Damege.GetComponent<FlushController>();

		g_Jump = GameObject.Find("Jump_Effect");
		je_Effect = g_Jump.GetComponent<JumpEffect>();
		g_Jump.SetActive(false);

		as_Sound = GetComponents<AudioSource>();

	}

	/*======================*/
	//  ゲッター
	/*======================*/

	// 向いている方向(左 ： -1,右 ： 1)
	public int IsDirection() { return n_LeftRightFlag; }

	// アニメーションの状態
	public int GetMortionState() { return n_MortionState; }

	public bool IsJump() { return b_JumpFlag; }

	public bool IsCameraMove() { return b_CameraMoveFlag; }

	/*======================*/
	// セッター
	/*======================*/
	public void MortionJump() { n_MortionState = (int)Mortion.Jump; }

	/*=====================*/
	//  更新関係
	/*=====================*/

	//---横移動関係---//
	void SideMove()
	{
		// 左が押されたとき
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			transform.Translate(g_Move.AccelerateLeft(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
			if (n_MortionState != (int)Mortion.Jump) n_MortionState = (int)Mortion.Move;
			n_LeftRightFlag = -1;
			SoundSide();
		}

		// 右が押されたとき
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			transform.Translate(g_Move.AccelerateRight(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
			if (n_MortionState != (int)Mortion.Jump) n_MortionState = (int)Mortion.Move;
			n_LeftRightFlag = 1;

			SoundSide();
		}

		// どちらも押されてないとき
		else SideKeyRelease();
	}

	void SoundSide()
	{
		if (!as_Sound[1].isPlaying)
		{
			as_Sound[1].Play();
		}
	}

	// キーが離されているとき
	void SideKeyRelease()
	{
		// 右が離されたとき
		if (g_Move.GetMove() > 0.0f) transform.Translate(g_Move.DecelerateRight(cf_DecelOnce), 0.0f, 0.0f);

		// 左が離されたとき
		if (g_Move.GetMove() < 0.0f) transform.Translate(g_Move.DecelerateLeft(cf_DecelOnce), 0.0f, 0.0f);

		as_Sound[1].Stop();
	}

	//---縦移動関係---//
	void VerticalMove()
	{
		// スペースが押されたときジャンプ
		if (Input.GetKeyDown(KeyCode.Space) && g_right2D.velocity.y < 0.3 && g_right2D.velocity.y > -0.3 && !b_JumpFlag)
		{
			g_right2D.AddForce(transform.up * f_JumpForce);
			n_MortionState = (int)Mortion.Jump;
			g_Jump.SetActive(true);
			g_Jump.transform.position = this.gameObject.transform.position;
			b_JumpFlag = true;
			as_Sound[0].Play();
		}

		if (g_Jump.activeSelf && je_Effect.IsAnimeEnd()) g_Jump.SetActive(false);

		// ジャンプ中
		if (g_right2D.velocity.y > 2.5f) n_MortionState = (int)Mortion.Jump;

		// 落下中
		if (g_right2D.velocity.y < -2.5f) n_MortionState = (int)Mortion.Fall;

	}

	void OnTriggerEnter2D(Collider2D _collider)
	{
		if (_collider.gameObject.tag == "TileMap" || _collider.gameObject.tag == "Libra")
		{
			b_JumpFlag = false;
		}

		if (_collider.gameObject.tag == "CameraMove") { b_CameraMoveFlag = true; }

		if (_collider.gameObject.tag == "CheckPoint")
		{
			v_PositionSave = _collider.gameObject.transform.position;
		}


		if (_collider.gameObject.tag == "EnemyAttack")
		{
			HpDecrease(5.0f);
		}

		if (_collider.gameObject.tag == "Fall")
		{

			int tmp = 1;
			if ((transform.position.x - v_PositionSave.x) > 0) tmp *= -1;
			transform.position = new Vector3(v_PositionSave.x, v_PositionSave.y, 0.0f);

			b_CameraMoveFlag = true;

			g_HP.Decrease(100.0f);
			GameObject director = GameObject.Find("HPDirector");
			director.GetComponent<HPDirector>().DecreaseHP(g_HP.GetHp(), cf_HPMax);
			g_right2D.AddForce(transform.right * -10.0f);
		}
	}

	void OnTriggerStay2D(Collider2D _chara)
	{
		if (_chara.gameObject.tag == "EnemyAttack")
		{
			HpDecrease(5.0f);
		}

	}

	void OnTriggerExit2D(Collider2D _collider)
	{
		if (_collider.gameObject.tag != "EnemyAttack")
		{
			g_DamegeScript.DestoryFlag();
		}

		if (_collider.gameObject.tag == "TileMap")
		{
			//v_PositionSave = transform.position;
			b_JumpFlag = true;
		}

		if (_collider.gameObject.tag == "CameraMove")
		{
			b_CameraMoveFlag = false;
		}

		if (_collider.gameObject.tag == "Fall")
		{
			b_CameraMoveFlag = false;
		}
	}
	void OnCollisionEnter2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "EnemyAttack")
		{
			HpDecrease(cf_Damage);
		}

		if (_collision.gameObject.tag == "Fall")
		{

			int tmp = 1;
			if ((transform.position.x - v_PositionSave.x) > 0) tmp *= -1;
			transform.position = new Vector3(v_PositionSave.x + tmp * 3.0f, v_PositionSave.y + 10.0f, 0.0f);
		}
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "EnemyAttack")
		{
			g_DamegeScript.DestoryFlag();
		}
	}

	void HpDecrease(float _decrease)
	{
		g_HP.Decrease(_decrease);
		GameObject director = GameObject.Find("HPDirector");
		director.GetComponent<HPDirector>().DecreaseHP(g_HP.GetHp(), cf_HPMax);
		g_right2D.AddForce(transform.right * -10.0f);
		g_DamegeScript.OnFlag();
	}

	/*============================*/
	// 値をすべて表示(デバック用)
	/*============================*/
	void ValueDraw()
	{
		//g_Move.ValueDraw();
		//Debug.Log(n_MortionState);
		//Debug.Log(g_right2D.velocity.y);
		Debug.Log(b_JumpFlag);

	}

	/*=============================*/
	// モーション管理
	/*=============================*/
	void MortionManager()
	{
		// 突進時スキップ
		AnimatorStateInfo anime_state = an_Mortion.GetCurrentAnimatorStateInfo(0);
		if (anime_state.IsName("Rush")) return;

		switch (n_MortionState)
		{

			case (int)Mortion.Jump:
				an_Mortion.Play("Jump");
				break;

			case (int)Mortion.Fall:
				an_Mortion.Play("Fall");
				break;

			case (int)Mortion.Move:
				an_Mortion.Play("Move");
				break;

			case (int)Mortion.Stay:
				an_Mortion.Play("Stay");
				break;
		}
	}

	// HP管理
	void HPManagement()
	{
		if (g_HP.GetHp() <= 0.0f) b_AliveFlag = false;
	}

	public void ReSpawn()
	{
		b_AliveFlag = true;
		transform.position = v_PositionSave;
		g_HP.SetHP(cf_HPMax);
		GameObject director = GameObject.Find("HPDirector");
		director.GetComponent<HPDirector>().DecreaseHP(g_HP.GetHp(), cf_HPMax);
	}

	/*===========*/
	//   更新
	/*===========*/
	void Update()
	{
		if (!b_AliveFlag) return;

		// 横移動
		SideMove();

		// 縦移動
		VerticalMove();

		// HP管理
		HPManagement();

		if ((g_Move.GetMove() == 0.0f && n_MortionState == (int)Mortion.Move) ||
			(n_MortionState >= (int)Mortion.Jump && !b_JumpFlag)) n_MortionState = (int)Mortion.Stay;

		MortionManager();

		if (n_LeftRightFlag != 0) transform.localScale = new Vector3(n_LeftRightFlag * 1.5f, 1.5f, 1);

		// デバック用
		ValueDraw();
	}
}
