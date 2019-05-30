using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeActive : MonoBehaviour
{
	public Fade fade;
	[SerializeField]
	FadeImage fi_Image = default;

	[SerializeField]
	bool b_FadeIn = default;

	[SerializeField]
	bool b_FadeOut = default;


	[SerializeField]
	private float f_FadeSpeed = 0.9f;

	private bool b_FadeFinish = false;
	
	public void EveryTimeOnFadeIn() { b_FadeIn = true; }

	public bool IsFadeFinish() { return b_FadeFinish; }

	private void Update()
	{
		if (b_FadeIn && fi_Image.Range >= 1.0f) {
			b_FadeFinish = true;
			b_FadeOut = true;
			b_FadeIn = false;
		}

		else if (b_FadeOut && fi_Image.Range <= 0.0f) {
			b_FadeFinish = true;
			b_FadeOut = false;
			b_FadeIn = true;
		}
		else { b_FadeFinish = false; }
	}

	public void FadeIn()
	{
		fade.FadeIn(f_FadeSpeed);
	}

	public void FadeOut()
	{
		fade.FadeOut(f_FadeSpeed);
	}

}
