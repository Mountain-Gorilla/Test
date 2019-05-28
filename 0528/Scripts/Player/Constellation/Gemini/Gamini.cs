using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamini : MonoBehaviour
{
	// 座標の保存
	private const int   cn_PlayerMax = 15;
	private const float cf_Distance = 3.5f;    // 離れる距離
	Vector3[]           v3_Position = new Vector3[cn_PlayerMax];

	private const float f_JumpForce = 790.0f; //ジャンプ力

	private GameObject  g_Player;
	private Player      g_PlayerScript;

	//アニメーション
	private Animator    an_Mortion;
	private enum        Mortion { Stay, Move, Jump, Fall }; //アニメーション管理
	private int         n_MortionState = (int)Mortion.Stay;  //現在のアニメーションの管理変数

	private Rigidbody2D g_DebugRight2D; //デバック用

	/*=====================*/
	// テクスチャ変更
	/*=====================*/

	// 主人公の画像
	[SerializeField]
	private Texture tex_Gemini;

	// テクスチャのID
	private static int n_idMainTex = Shader.PropertyToID("_MainTex");  

	// 画像の変更用変数
	private SpriteRenderer s_Render;
	private MaterialPropertyBlock mpb_Block;

	// テクスチャの変更用変数
	public Texture overrideTexture
	{
		get { return tex_Gemini; }
		set
		{
			tex_Gemini = value;
			if (mpb_Block == null) TexChangeInit();

			mpb_Block.SetTexture(n_idMainTex, tex_Gemini);
		}
	}

	/*=============================*/
	//  テクスチャの変更するための初期化
	/*=============================*/
	void TexChangeInit()
	{
		mpb_Block = new MaterialPropertyBlock();
		s_Render = GetComponent<SpriteRenderer>();
		s_Render.GetPropertyBlock(mpb_Block);
	}

	void Awake()
	{
		TexChangeInit();
		overrideTexture = tex_Gemini;
		OnEnable();
	}

	void OnEnable()
	{
		g_Player = GameObject.Find("Player");
		g_PlayerScript = g_Player.GetComponent<Player>();
		for (int i = 0; i < cn_PlayerMax; i++) {
			v3_Position[i] = g_Player.transform.position;
			v3_Position[i].x -= g_PlayerScript.IsDirection() * cf_Distance;
		}

		transform.position = v3_Position[0];

		an_Mortion = GetComponent<Animator>();
		n_MortionState = (int)Mortion.Stay;

	}

	//=============================*/
	// モーション管理
	//=============================*/
	void MortionManager()
	{
		// 「Jump」
		if ((v3_Position[cn_PlayerMax - 2].y > v3_Position[cn_PlayerMax - 1].y || n_MortionState ==(int)Mortion.Jump) && 
			Mathf.Abs(transform.position.y - g_Player.transform.position.y) > 0.1f) {
			n_MortionState = (int)Mortion.Jump;
			an_Mortion.Play("Jump");
			return;
		}

		// 「Fall」
		if (v3_Position[cn_PlayerMax - 2].y < v3_Position[cn_PlayerMax - 1].y || n_MortionState == (int)Mortion.Fall) {
			n_MortionState = (int)Mortion.Fall;
			an_Mortion.Play("Fall");
			return;
		}

		// 「Move」
		if (v3_Position[cn_PlayerMax - 2].x != v3_Position[cn_PlayerMax - 1].x || n_MortionState == (int)Mortion.Move) {
			n_MortionState = (int)Mortion.Move;
			an_Mortion.Play("Move");
			return;
		}

		// 「Stay」
		if((v3_Position[cn_PlayerMax - 2].x == v3_Position[cn_PlayerMax - 1].x) ||  n_MortionState == (int)Mortion.Stay) {

			n_MortionState = (int)Mortion.Stay;
			an_Mortion.Play("Stay");
			return;
		}
	}

	// プレイヤーが待機中で遠ければ近づく
	void DistanceApproach()
	{
		if (g_PlayerScript.GetMortionState() != (int)Mortion.Stay) return;
		float distance = (transform.position - g_Player.transform.position).sqrMagnitude;
		
		// 遠ければ近づく
		if (distance > cf_Distance) {
			const float move_once = 0.1f; // 一度の移動距離

			// 分身から見てプレイヤーがいる方向に移動する(X座標)
			float right_or_left;
			right_or_left = transform.position.x < g_Player.transform.position.x ? move_once : -move_once;

			//(Y座標)
			float top_or_bottom;
			top_or_bottom = transform.position.y < g_Player.transform.position.y ? move_once : -move_once;

			transform.Translate(right_or_left, top_or_bottom, 0.0f);

			n_MortionState = (int)Mortion.Move;
		}
		else {
			n_MortionState = (int)Mortion.Stay;
		}
	}

	void LateUpdate()
	{
		// 座標更新
		for (int i = cn_PlayerMax - 1; i > 0; i--) v3_Position[i] = v3_Position[i - 1];
		v3_Position[0] = g_Player.transform.position;
		//v3_Position[0].x -= /*g_PlayerScript.IsDirection() */ cf_Distance;

		transform.position += v3_Position[cn_PlayerMax - 2] - v3_Position[cn_PlayerMax - 1];

		// プレイヤーが待機中で遠ければ近づく
		DistanceApproach();

		// モーション管理
		MortionManager();

		transform.localScale = new Vector3(g_PlayerScript.IsDirection() * 1.5f, 1.5f, 1.0f);

		// テクスチャの変更
		s_Render.SetPropertyBlock(mpb_Block);
		overrideTexture = tex_Gemini;

		// デバック用
		ValueDraw();

	}

	// デバック用
	void ValueDraw()
	{
		//Debug.Log(n_MortionState);
		//Debug.Log(transform.position.y - g_Player.transform.position.y);
	}

}
