using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private GameObject g_Player; // プレイヤー
	private Player     g_Script;

	// ロード時に実行
	void Start ()
    {
        g_Player = GameObject.Find("Player");
		g_Script = g_Player.GetComponent<Player>();
	}
	
	// 更新
	void Update ()
    {
		//float f_Center = Screen.width / 2.0f;
	
		Vector3 v_PlayerPos = g_Player.transform.position;
		transform.position = new Vector3(v_PlayerPos.x, v_PlayerPos.y + 3.8f, -30.0f);
	}
}
