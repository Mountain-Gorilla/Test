using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libra : MonoBehaviour
{
	private GameObject g_Player;
	private Player g_PlayerScript;

	private GameObject g_Lever;

	// 落下して登場
	private float f_Fall;
	private const float cf_FallDistance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
		g_Player = GameObject.Find("Player");
		g_PlayerScript = g_Player.GetComponent<Player>();

		g_Lever = GameObject.Find("Lever");
		OnEnable();
    }


	void OnEnable()
	{
		Vector3 position = new Vector3(g_Player.transform.position.x + g_PlayerScript.IsDirection() * 3.0f, g_Player.transform.position.y + cf_FallDistance + 2.0f, 0);
		transform.position = position;

		f_Fall = 0.0f;
		g_Lever.SetActive(true);
	}

    // Update is called once per frame
    void Update()
    {
		if (f_Fall >= cf_FallDistance) return;
		float fall = 0.5f;
		f_Fall += fall;

		transform.Translate(0.0f, -fall, 0.0f);
		
    }
}
