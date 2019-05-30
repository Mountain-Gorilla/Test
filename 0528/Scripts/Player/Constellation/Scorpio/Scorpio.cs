using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpio : MonoBehaviour
{
	[SerializeField]
	Animator an_Needle = default;

	[SerializeField]
	private GameObject g_Player = default;

	[SerializeField]
	private Player p_Script = default;

	// 拡縮用
	float f_Scale = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

	void OnEnable()
	{
		Vector3 position = g_Player.transform.position;
		position.x += p_Script.IsDirection() * f_Scale;
		position.y += 1.0f;

		transform.localScale = new Vector3(p_Script.IsDirection() * 1.73f * f_Scale, 2.162f * f_Scale, 0.0f);

		transform.position = position;
		an_Needle.Play("Needle");
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
