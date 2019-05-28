using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capricorn : MonoBehaviour
{
	private GameObject  g_Player;
	private Player      g_Script;
	private Rigidbody2D g_Rigit2D;     // ジャンプの処理用
	private const float f_JumpForce = 900.0f;    // ジャンプ力

	private bool b_JumpFlag;
	
    // Start is called before the first frame update
    void Start()
    {
		g_Player = GameObject.Find("Player");
		g_Script = g_Player.GetComponent<Player>();
		g_Rigit2D = g_Player.GetComponent<Rigidbody2D>();
		b_JumpFlag = false;
	}

    void OnEnable()
	{
		b_JumpFlag = false;
	}

    // Update is called once per frame
    void FixedUpdate()
    {
		if(Input.GetKeyDown(KeyCode.Space) && !b_JumpFlag && g_Script.IsJump()) {
			g_Rigit2D.AddForce(transform.up * f_JumpForce);
			g_Script.MortionJump();
			b_JumpFlag = true;
		}

		Debug.Log(b_JumpFlag);
    }

	void OnTriggerEnter2D(Collider2D _map)
	{
		if (_map.gameObject.tag == "TileMap") {
			b_JumpFlag = false;
		}

	}

	void OnTriggetStay2D(Collider2D _map)
	{
		

	}

	void OnTriggerExit2D(Collider2D _map)
	{
		
	}
}
