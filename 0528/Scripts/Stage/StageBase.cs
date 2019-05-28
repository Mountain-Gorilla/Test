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
	
	private const float cf_MoveRate = 0.7f;

	// Use this for initialization
	void Start ()
    {
        g_Camera = GameObject.Find("Main Camera");
		v_StartCameraPos = g_Camera.transform.position;
		v_StartCameraPos.z = 0.0f;

		g_Player = GameObject.Find("Player");
		v_StartPlayerPos = g_Player.transform.position;

	}

	// Update is called once per frame
	void LateUpdate ()
    {

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SceneManager.LoadScene("TitleScene");
        }

		Vector3 v = (g_Player.transform.position - v_StartPlayerPos) * cf_MoveRate;
		v.z = 0.0f;

		transform.position = new Vector3(g_Camera.transform.position.x, g_Camera.transform.position.y, 0.0f);
	}
}
