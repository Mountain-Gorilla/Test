using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorpio : MonoBehaviour
{
	[SerializeField]
	Animator an_Needle;

	GameObject g_Player;
	Player p_Script;

    // Start is called before the first frame update
    void Start()
    {
		g_Player = GameObject.Find("Player");
		p_Script = g_Player.GetComponent<Player>();
    }

	void OnEnable()
	{
		Vector3 position = g_Player.transform.position;
		position.x += p_Script.IsDirection() * 2.5f;

		transform.localScale = new Vector3(-p_Script.IsDirection() * 1.73f, 2.162f, 0.0f);

		transform.position = position;
		an_Needle.Play("Needle");
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
