using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leo : MonoBehaviour
{
	private GameObject g_Bite;
	private Bite g_Script;

    // ビルド時に実行
    void Start ()
    {
        g_Bite = GameObject.Find("Bite");
		g_Script = g_Bite.GetComponent<Bite>();
		g_Bite.SetActive(false);
	}
	
	// 更新
	void Update ()
    {
		if (!g_Script.IsAnimation()) g_Bite.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D _collider)
	{
		if(_collider.gameObject.tag == "Enemy" && !g_Bite.activeSelf) {
			g_Bite.SetActive(true);
			g_Script.GetEnemyPos(_collider.gameObject.transform.position);
		}
	}
}
