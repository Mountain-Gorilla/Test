using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
	// お皿
	private enum         InclineDirection{ Left,Right, Stay,None };  // 傾ける方向の管理
	private int          n_InclineState = (int)InclineDirection.None;
	private float        f_KeepTimer = 0.0f;        // 維持時間
	private const float  cf_KeepTimeMax = 2.0f;

	[SerializeField]
	private GameObject[] g_Dish = new GameObject[2];   // オブジェクト内のスクリプトを取得
	private Dish[]       g_DishScript = new Dish[2];

	// 傾ける
	private const float  cf_InclineMax = 45.0f;           // 最大角度
	private const float  cf_InclineMin = 0.0f;            // 最小角度
	private float        f_NowRotate = cf_InclineMin;     // 現在の角度 
	private float        f_RotateTimer = 0.0f;            // 回転を管理するタイマー
	private const float  cf_TimerOnce = 0.005f;

	public float InverseRotate() { return -f_NowRotate; }

	// Start is called before the first frame update
	void Start()
	{

		OnEnable();

		for (int i = 0; i < 2; i++) {
			g_DishScript[i] = g_Dish[i].GetComponent<Dish>();
			g_Dish[i].SetActive(true);
		}
	}

	// アクティブになった時
	void OnEnable()
	{
		n_InclineState = (int)InclineDirection.None;
		f_RotateTimer = 0.0f;
		f_KeepTimer = 0.0f;
		f_NowRotate = cf_InclineMin;

		for (int i = 0; i < 2; i++) g_Dish[i].SetActive(true);
	}

	void OnDisable()
	{
		transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
		f_NowRotate = cf_InclineMin;
	}

	// Update is called once per frame
	void Update()
    {
		CheckState();

		KeepingTimer();

		Incline();

		ValueDraw();
	}

	// 状態確認
	void CheckState()
	{
		if (g_DishScript[(int)InclineDirection.Left].IsDown()) {
			if (n_InclineState != (int)InclineDirection.Left) f_RotateTimer = 0.0f;
			n_InclineState = (int)InclineDirection.Left;
		}

		else if (g_DishScript[(int)InclineDirection.Right].IsDown()){
			if (n_InclineState != (int)InclineDirection.Right) f_RotateTimer = 0.0f;
			n_InclineState = (int)InclineDirection.Right;
		}

		else if (n_InclineState != (int)InclineDirection.None) n_InclineState = (int)InclineDirection.Stay;

	}

	// 状態によって傾ける
	void Incline()
	{
		switch(n_InclineState)
		{
			case (int)InclineDirection.Left:
				RotateAngle(f_NowRotate, cf_InclineMax);
				break;

			case (int)InclineDirection.Right:
				RotateAngle(f_NowRotate, -cf_InclineMax);
				break;

			case (int)InclineDirection.None:
				RotateAngle(cf_InclineMin, f_NowRotate);
				break;

		}
	}

	// もとに戻るまでの待機時間
	void KeepingTimer()
	{
		if (n_InclineState != (int)InclineDirection.Stay) return;

		f_KeepTimer += Time.deltaTime;
		if (f_KeepTimer < cf_KeepTimeMax) return;

		//f_RotateTimer = 0.0f;
		f_KeepTimer = 0.0f;
		n_InclineState = (int)InclineDirection.None;
		
	}


	// 指定角度まで回転
	void RotateAngle(float _start,float _end)
	{
		//f_RotateTimer += n_InclineState != (int)InclineDirection.None ? cf_TimerOnce : -cf_TimerOnce / 2.0f;

		float rot_timer = 0.0f;
		if (n_InclineState <= (int)InclineDirection.Right) rot_timer = cf_TimerOnce;
		else if (n_InclineState == (int)InclineDirection.None) rot_timer = -cf_TimerOnce;

		f_RotateTimer += rot_timer;

		if (f_RotateTimer >= 1.0f) f_RotateTimer = 1.0f;
		if (f_RotateTimer <= 0.0f) f_RotateTimer = 0.0f;

		f_NowRotate = Mathf.LerpAngle(_start, _end, f_RotateTimer);
		transform.eulerAngles = new Vector3(0, 0, f_NowRotate);
	}

	void ValueDraw()
	{
		//Debug.Log(f_NowRotate);
		//Debug.Log(f_RotateTimer);
		//Debug.Log(n_InclineState);
		//Debug.Log(g_DishScript[(int)InclineDirection.Left].IsDown());
		//Debug.Log(g_DishScript[(int)InclineDirection.Right].IsDown());
	}
}
