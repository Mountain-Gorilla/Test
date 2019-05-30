using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private GameObject g_Player; // プレイヤー
	private Player     g_Script;

	private bool b_CameraMoved = false;

	// ロード時に実行
	void Start()
    {
        g_Player = GameObject.Find("Player");
		g_Script = g_Player.GetComponent<Player>();
		transform.position = new Vector3(g_Player.transform.position.x, -40.0f, -30.0f);

		b_CameraMoved = false;
	}
	
	// 更新
	void Update ()
    {
		//float f_Center = Screen.width / 2.0f;

		float camera_y = 0.0f;
		if(g_Player.transform.position.y - this.gameObject.transform.position.y >= 2.5f) {
			camera_y = (g_Player.transform.position.y - this.gameObject.transform.position.y) / 15.0f;
		}
		else if(g_Player.transform.position.y - this.gameObject.transform.position.y <= -2.8f) {
			camera_y = (g_Player.transform.position.y - this.gameObject.transform.position.y) / 15.0f;
		}

		transform.position = new Vector3(g_Player.transform.position.x, this.gameObject.transform.position.y, -30.0f);

		if (g_Script.IsCameraMove()) {
			transform.Translate(0.0f, camera_y, 0.0f);
			b_CameraMoved = true;
			return;
		}

		float distance = this.gameObject.transform.position.y - 2.8f;
		if ((b_CameraMoved && distance < g_Player.transform.position.y) || (!b_CameraMoved && distance > g_Player.transform.position.y)) {
			distance -= g_Player.transform.position.y;

			//Debug.Log(distance);

			if (distance <= -0.5f) distance = -0.5f;
			else if(distance >= -0.1f && distance <= 0.1f) { b_CameraMoved = false; }
			transform.Translate(0.0f, -distance, 0.0f);
		}
	}
}
