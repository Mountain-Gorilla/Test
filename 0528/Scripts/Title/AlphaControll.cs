using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaControll : MonoBehaviour
{
	[SerializeField]
	SpriteRenderer sr_Alpha = default;

	[SerializeField]
	float f_AlphaOnce = 0.01f;

	float f_Alpha = 1.0f;
	bool b_Up;

    // Start is called before the first frame update
    void Start()
    {
		f_Alpha = 1.0f;
		b_Up = false;
    }

    // Update is called once per frame
    void Update()
    {		
		if (b_Up) {
			f_Alpha += f_AlphaOnce;
			if (f_Alpha >= 1.0f) b_Up = false;
		}

		else
		{
			f_Alpha -= f_AlphaOnce;
			if (f_Alpha <= 0.5f) b_Up = true;
		}

		sr_Alpha.color = new Color(1.0f, 1.0f, 1.0f, f_Alpha);
    }
}
