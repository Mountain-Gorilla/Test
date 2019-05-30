using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
	[SerializeField]
	GameObject g_Fade = default;
	FadeActive fa_IsCheck;

	private bool b_FadeFlag;

	// Use this for initialization
	void Start ()
	{
		fa_IsCheck = g_Fade.GetComponent<FadeActive>();
		b_FadeFlag = false;
	} 
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
			b_FadeFlag = true;
			fa_IsCheck.FadeIn();
            //SceneManager.LoadScene("ForestScene");
        }

		if (b_FadeFlag && fa_IsCheck.IsFadeFinish())
		{
			b_FadeFlag = false;
			fa_IsCheck.EveryTimeOnFadeIn();

			SceneManager.LoadScene("ForestScene");
		}

		if (Input.GetKeyDown(KeyCode.Escape)) {
            //SceneManager.LoadScene("BaseCamp");
        }

    }
}
