using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconGauge : MonoBehaviour
{
	[SerializeField]
	GameObject g_Ability = default;

	// Start is called before the first frame update
	void Start()
    {
		
    }

	// ゲージ回復
    public void Recovery(float _recovery,float _max)
	{
		float recovery = _recovery / _max;
		if (recovery >= 1.0f) recovery = 1.0f;

		g_Ability.GetComponent<Image>().fillAmount = recovery;

	}

	// 能力発動時ゲージを０にする
	public void UseAbility()
	{
		g_Ability.GetComponent<Image>().fillAmount = 0.0f;
	}
}
