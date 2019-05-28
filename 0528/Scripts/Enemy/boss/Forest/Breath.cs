using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
	private GameObject g_Boss;
	// 突進用
	private Direction g_Direction;

	private GameObject g_Player;
    private Vector3    v_Direct;

    public  float      f_Speed=0.3f;      //スピード

	private bool b_ActiveFlag;
	private int  n_IsReflection;

	public bool IsActive() { return b_ActiveFlag; }

	void Start()
	{
		g_Boss = GameObject.Find("Boss");
		g_Direction = g_Boss.GetComponent<Direction>();

        g_Player = GameObject.Find("Player");

		b_ActiveFlag = true;

		OnEnable();
	}

   void OnEnable()
	{
        Vector3 playerpos = g_Player.transform.position;
        Vector3 bosspos = g_Boss.transform.position;
        Vector3 breathpos = new Vector3(bosspos.x - 0.3f, bosspos.y + 2.0f, 0);
        v_Direct = playerpos - breathpos;
        v_Direct = v_Direct.normalized;

        transform.position = breathpos;

		b_ActiveFlag = true;
		n_IsReflection = 1;

    }

	void OnDisable()
	{
		b_ActiveFlag = false;
	}

	// Update is called once per frame
	void Update()
    {
		transform.Translate(n_IsReflection * -g_Direction.IsDirection() * v_Direct.x * f_Speed, n_IsReflection * v_Direct.y * f_Speed, 0.0f);
    }

	void OnTriggerEnter2D(Collider2D _collider)
	{
		if(_collider.gameObject.tag == "Reflection") {
			n_IsReflection = -1;
		}

		if (_collider.gameObject.tag == "TileMap")
		{
			b_ActiveFlag = false;
		}
	}

	void OnTriggerStay2D(Collider2D _collider)
	{
		if (_collider.gameObject.tag == "Player")
		{
			//b_ActiveFlag = false;
		}
	}

}
