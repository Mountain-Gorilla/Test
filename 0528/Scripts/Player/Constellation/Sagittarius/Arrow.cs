using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
	//private Rigidbody2D g_Right2D;            // 当たり判定用
	private const float cf_Speed = 10.0f;    // 矢の速度

	// プレイヤーの情報取得
	private GameObject g_Player;
	private Player     g_Script;

	private GameObject g_Sagittarius;

	private bool       b_ActiveFlag = false; // アクティブかどうか
	private int        n_DirectionSide = 0;

	// 初期化
	void Start()
	{
		//g_Right2D = GetComponent<Rigidbody2D>();

		g_Player = GameObject.Find("Player");
		g_Script = g_Player.GetComponent<Player>();

		g_Sagittarius = GameObject.Find("Sagittarius");
		transform.position = g_Sagittarius.transform.position;

		b_ActiveFlag = false;
	}

	void OnEnable()
	{
		n_DirectionSide = g_Script.IsDirection();

		g_Sagittarius = GameObject.Find("Player/Sagittarius");
		transform.position = g_Sagittarius.transform.position;

		if (n_DirectionSide == -1) transform.localRotation = new Quaternion(0.0f, 0.0f, 180.0f,0.0f);
		else { transform.localRotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f); }

		b_ActiveFlag = true;
	}

	void OnDisable()
	{
		b_ActiveFlag = false;
	}

	public bool IsActive() { return b_ActiveFlag; }

    // Update is called once per frame
    void Update()
    {
		transform.Translate(/*n_DirectionSide */ 0.5f, 0.0f, 0.0f);
    }

	void OnCollisionEnter2D(Collision2D _collder)
	{
		b_ActiveFlag = false;
	}
}
