using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageBase : MonoBehaviour
{
    GameObject g_Camera;
	private Vector3 v_StartCameraPos;

	GameObject g_Player;
	private Vector3 v_StartPlayerPos;
	private Player p_Script;

	[SerializeField]
	GameObject g_Boss;
	BossState  bs_IsAlive;

	[SerializeField]
	FadeActive fa_IsCheck;
	private bool b_FadeOut;
	private bool b_FadeIn;

	private const float cf_MoveRate = 0.7f;

	// Use this for initialization
	void Start ()
    {
        g_Camera = GameObject.Find("Main Camera");
		v_StartCameraPos = g_Camera.transform.position;
		v_StartCameraPos.z = 0.0f;

		g_Player = GameObject.Find("Player");
		v_StartPlayerPos = g_Player.transform.position;
		p_Script = g_Player.GetComponent<Player>();

		bs_IsAlive = g_Boss.GetComponent<BossState>();

		b_FadeOut = true;
		b_FadeIn = false;
		fa_IsCheck.FadeOut();
	}

	void OnEnable()
	{
		fa_IsCheck.FadeOut();
	}

	// Update is called once per frame
	void Update ()
    {
		if ((!p_Script.IsAlive() || bs_IsAlive.IsGameClear()) && !b_FadeIn)
		{
			b_FadeIn = true;
			fa_IsCheck.FadeIn();
			//SceneManager.LoadScene("ForestScene");
		}

		Debug.Log("Clear : " + b_FadeIn);

		if (b_FadeIn && fa_IsCheck.IsFadeFinish())
		{
			b_FadeIn = false;

			SceneManager.LoadScene("TitleScene");
		}

		if (b_FadeOut && fa_IsCheck.IsFadeFinish()) {
			b_FadeOut = false;
		}


        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("TitleScene");
        }

		Vector3 v = (g_Player.transform.position - v_StartPlayerPos) * cf_MoveRate;
		v.z = 0.0f;

		transform.position = new Vector3(g_Camera.transform.position.x, g_Camera.transform.position.y, 0.0f);
	}
}
