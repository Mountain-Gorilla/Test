using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Move    g_Move;       // 移動
    private const float f_JumpForce = 900.0f;    // ジャンプ力
	private bool        b_JumpFlag;

	private Vector3 v_PositionSave;  // 落ちた時用
	
    // パラメータ設定
    private HP g_HP;
	private const float cf_HPMax = 600.0f;  // 最大値
	private const float cf_Damage = 5.0f;  // ダメージ量

    // 横移動
    private const float cf_MaxSpeed  = 0.15f;       // 最大値
    private const float cf_AccelOnce = 0.15f;       // 一度の加速量(未使用)
    private const float cf_DecelOnce = 0.15f;       // 一度の減速量(未使用)
	private enum        Direction { Left ,Right};  // 自機が向いている方向
    private int         n_LeftRightFlag = (int)Direction.Right;  // 方向の判定

	// アニメーション
	private Animator    an_Mortion;  
	private enum        Mortion { Stay,Move,Jump,Fall};     // アニメーション管理
	private int         n_MortionState = (int)Mortion.Stay; // 現在のアニメーションの管理変数

    private Rigidbody2D g_right2D; // ジャンプの処理用

	// 敵関係
	private GameObject g_Enemy;

	// ダメージエフェクト
	private GameObject g_Damege;
	private FlushController g_DamegeScript;
	
    /*======================*/
    //  初期化
    /*======================*/
    void Start()
    {
		// 初期座標を設定
		//transform.position = new Vector3(-175.3f, -42.3f, 0.0f);
        transform.position = new Vector3(103.3f, 0.3f, 0.0f);

		b_JumpFlag = false;

        g_Move = new Move();
     
		g_HP = new HP();
        g_HP.SetHP(cf_HPMax);

        an_Mortion = GetComponent<Animator>();
		n_MortionState = (int)Mortion.Stay;

		g_right2D = GetComponent<Rigidbody2D>();

        n_LeftRightFlag = (int)Direction.Right;

		g_Enemy = GameObject.Find("Goblin");

		g_Damege = GameObject.Find("PlayerEffect");
		g_DamegeScript = g_Damege.GetComponent<FlushController>();

    }
	
    /*======================*/
    //  ゲッター
    /*======================*/

    // 向いている方向(左 ： -1,右 ： 1)
    public int IsDirection() { return n_LeftRightFlag; }

	// アニメーションの状態
	public int GetMortionState() { return n_MortionState; }

	public bool IsJump() { return b_JumpFlag; }

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
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(g_Move.AccelerateLeft(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
			if(n_MortionState != (int)Mortion.Jump)n_MortionState = (int)Mortion.Move;
			n_LeftRightFlag = -1;
        }

        // 右が押されたとき
        else if (Input.GetKey(KeyCode.RightArrow)) {
            transform.Translate(g_Move.AccelerateRight(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
			if(n_MortionState != (int)Mortion.Jump)n_MortionState = (int)Mortion.Move;
            n_LeftRightFlag = 1;
        }

        // どちらも押されてないとき
        else SideKeyRelease();
    }


    // キーが離されているとき
    void SideKeyRelease()
    {
        // 右が離されたとき
        if (g_Move.GetMove() > 0.0f) transform.Translate(g_Move.DecelerateRight(cf_DecelOnce), 0.0f, 0.0f);
        
        // 左が離されたとき
        if (g_Move.GetMove() < 0.0f) transform.Translate(g_Move.DecelerateLeft(cf_DecelOnce), 0.0f, 0.0f);
	}

    //---縦移動関係---//
    void VerticalMove()
    {
        // スペースが押されたときジャンプ
        if (Input.GetKeyDown(KeyCode.Space) && g_right2D.velocity.y < 0.3 && g_right2D.velocity.y > -0.3) {
            g_right2D.AddForce(transform.up * f_JumpForce);
			n_MortionState = (int)Mortion.Jump;
			b_JumpFlag = true;
		}
        
        // ジャンプ中
        if (g_right2D.velocity.y > 2.5f) n_MortionState = (int)Mortion.Jump;
		
		// 落下中
		if (g_right2D.velocity.y < -2.5f) n_MortionState = (int)Mortion.Fall;
       
    }

	void OnTriggerEnter2D(Collider2D _collider)
	{
		if(_collider.gameObject.tag == "TileMap") {
			b_JumpFlag = false;
		}

		if (_collider.gameObject.tag == "EnemyAttack") {
			HpDecrease();
		}

		if(_collider.gameObject.tag == "Fall") {

			int tmp = 1;
			if ((transform.position.x - v_PositionSave.x) > 0) tmp *= -1;
			transform.position = new Vector3(v_PositionSave.x + tmp * 3.0f, v_PositionSave.y, 0.0f);

			g_HP.Decrease(cf_Damage);
			GameObject director = GameObject.Find("HPDirector");
			director.GetComponent<HPDirector>().DecreaseHP(g_HP.GetHp(), cf_HPMax);
			g_right2D.AddForce(transform.right * -10.0f);
		}
	}

    void OnTriggerStay2D(Collider2D _chara)
    {
		if (_chara.gameObject.tag == "EnemyAttack") {
			HpDecrease();
		}

    }

	void OnTriggerExit2D(Collider2D _collider)
	{
		if(_collider.gameObject.tag == "EnemyAttack") {
			g_DamegeScript.DestoryFlag();
		}

		if (_collider.gameObject.tag == "TileMap") {
			v_PositionSave = transform.position;
			b_JumpFlag = true;
		}
	}

	void OnCollisionEnter2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "EnemyAttack") {
			HpDecrease();
		}

	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (_collision.gameObject.tag == "EnemyAttack") {
			g_DamegeScript.DestoryFlag();
		}
	}

	void HpDecrease()
	{
		g_HP.Decrease(cf_Damage);
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
        Debug.Log(g_HP.GetHp());
		
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


	// 当たり判定
	void HitJudge()
	{
		if (g_Enemy.activeSelf == false) return;

		Vector2 v_Player = transform.position;
		Vector2 v_Enemy= g_Enemy.transform.position;
		Vector2 v_Dir = v_Player - v_Enemy;

		float f_Dir = v_Dir.magnitude;
		float f_RadEnemy = 0.5f;
		float f_RadPlayer = 1.0f;

		if (f_Dir < f_RadEnemy + f_RadPlayer)
		{
			g_HP.Decrease(cf_Damage);
			GameObject director = GameObject.Find("HPDirector");
			director.GetComponent<HPDirector>().DecreaseHP(g_HP.GetHp(), cf_HPMax);
			g_right2D.AddForce(transform.right * -10.0f);
			g_DamegeScript.OnFlag();
		}
		else g_DamegeScript.DestoryFlag();
	}

	// HP管理
	void HPManagement()
	{
		// デバック用HP回復
		if (Input.GetKeyDown(KeyCode.M)) {
			g_HP.SetHP(cf_HPMax);
			GameObject director = GameObject.Find("HPDirector");
			director.GetComponent<HPDirector>().Reset();
		}
	}

	/*===========*/
	//   更新
	/*===========*/
	void Update ()
    {
        // 横移動
        SideMove();

        // 縦移動
        VerticalMove();

		// HP管理
		HPManagement();

        if ((g_Move.GetMove() == 0.0f && n_MortionState == (int)Mortion.Move) || 
			(n_MortionState >= (int)Mortion.Jump && !b_JumpFlag))  n_MortionState = (int)Mortion.Stay;

		MortionManager();

		if (n_LeftRightFlag != 0) transform.localScale = new Vector3(n_LeftRightFlag * 1.5f, 1.5f, 1);

        // デバック用
        ValueDraw();
    }
}
