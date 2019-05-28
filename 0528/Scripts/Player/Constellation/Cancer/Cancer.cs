using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancer : MonoBehaviour
{
	[SerializeField]
	List<GameObject> lg_Nail = new List<GameObject>();

	[SerializeField]
	List<Collider2D> lc_Nail = new List<Collider2D>();

	private float       f_VerticalMove;
	private const float cf_VerticalMoveMax = 5.0f;

	private GameObject g_Player;
    
    // ビルド時に実行
    void Awake()
    {
		g_Player = GameObject.Find("Player");

		OnDisable();
		OnEnable();
    }

	void OnEnable()
	{
		f_VerticalMove = 0.0f;

	    Vector3 position = g_Player.transform.position;
	    position.x -= 1.7f;
		position.y -= cf_VerticalMoveMax;

	    for (int i = 0; i < lg_Nail.Count; i++) {
			if (i == 1) position.x += 3.4f;

			lg_Nail[i].SetActive(true);
			lg_Nail[i].transform.position = position;
		}
	}

	void OnDisable()
	{
		for (int i = 0; i < lg_Nail.Count; i++) {
			lg_Nail[i].SetActive(false);
		}
	}

    // 更新
	void Update ()
    {
		f_VerticalMove += cf_VerticalMoveMax / 10.0f;
		if (f_VerticalMove >= cf_VerticalMoveMax) {
			for (int i = 0; i < lg_Nail.Count; i++) lc_Nail[i].isTrigger = false; 
			
			return;
		}

		for (int i = 0; i < lg_Nail.Count; i++) {
			lg_Nail[i].transform.Translate(0.0f, cf_VerticalMoveMax / 10.0f, 0.0f);
		}

	}
}
