﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bite : MonoBehaviour
{
	[SerializeField]
	private GameObject g_Player = default;

	[SerializeField]
	private Player g_Script = default;

	// アニメーション
	private Animator an_Bite = default;
	private bool b_Animation = false;

	public bool IsAnimation() { return b_Animation; }

	// 敵の座標を取得
	public void GetEnemyPos(Vector3 _enemy)
	{
		transform.position = _enemy;

		an_Bite.Play("Bite");
		b_Animation = true;
	}

    // Start is called before the first frame update
    void Awake()
    {
		an_Bite = GetComponent<Animator>();
		OnEnable();
	}

	void OnEnable()
	{
		Vector3 position = new Vector3(g_Player.transform.position.x + g_Script.IsDirection() * 3.5f, 
			                           g_Player.transform.position.y + 2.0f, 0.0f);
		transform.position = position;

		an_Bite.Play("Bite");
		b_Animation = true;
	}

    // Update is called once per frame
    void Update()
    {
       if(an_Bite.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0) {
			b_Animation = false;
       }
    }
}
