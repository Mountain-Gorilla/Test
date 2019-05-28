using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
	// 梃子(てこ、皿を支えている棒)
	private GameObject g_Lever;
	
	// 下がっているか
	private bool b_DownFlag = false;

	// 当たり判定の回数を減らす(タイマーで)
	private float       f_HitDerayTimer = 0.0f;
	private const float cf_DerayTime = 0.3f;    // 遅延秒数

	public bool IsDown() { return b_DownFlag; }

    // Start is called before the first frame update
    void Start()
    {
		b_DownFlag = false;

		g_Lever = GameObject.Find("Lever");
    }

	void OnEnable()
	{
		b_DownFlag = false;
		f_HitDerayTimer = 0.0f;
	}

    // Update is called once per frame
    void Update()
    {
		transform.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);

		if (b_DownFlag) f_HitDerayTimer += Time.deltaTime;
    }

	void OnCollisionEnter2D(Collision2D _collision)
	{
		b_DownFlag = true;
		if(f_HitDerayTimer > cf_DerayTime) f_HitDerayTimer = 0.0f;
	}

	void OnCollisionExit2D(Collision2D _collision)
	{
		if (f_HitDerayTimer < cf_DerayTime) return;
		b_DownFlag = false;
	}
}
