using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeActive : MonoBehaviour
{
	public Fade fade;
	[SerializeField]
	FadeImage fi_Image;

	[SerializeField]
	bool b_UpDown;

	[SerializeField]
	private float f_FadeSpeed = 0.9f;

	private bool b_FadeFinish = false;
	

	public bool IsFadeFinish() { return b_FadeFinish; }

	private void Update()
	{		
		if (b_UpDown && fi_Image.Range >= 1.0f) b_FadeFinish = true;

		if (!b_UpDown && fi_Image.Range <= 0.0f) b_FadeFinish = true;
	}

	public void FadeIn()
	{
		fade.FadeIn(f_FadeSpeed);

		//return true;
	}

	public void FadeOut()
	{
		fade.FadeOut(f_FadeSpeed);
	}

}
