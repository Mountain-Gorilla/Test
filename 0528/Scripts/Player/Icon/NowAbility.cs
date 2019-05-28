using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NowAbility : MonoBehaviour
{
	// 現在のアイコンの画像
	[SerializeField]
	private Image i_IconSprite;

	[SerializeField]
	List<AbilityCheck>  lac_Ability = new List<AbilityCheck>();

	[SerializeField]
	List<Sprite>      ls_Ability = new List<Sprite>();

	[SerializeField]
	List<int>         ln_AbilityNumber = new List<int>();

	private int       n_NowSetAbility;
	private int       n_NextAbility;
	private const int cn_None = 12;

	public void SharingAbility(int _ability) { n_NextAbility = _ability; }

	public int GetNowAbility() { return n_NowSetAbility; }
	public Sprite GetNowSprite() { return ls_Ability[n_NowSetAbility]; }

	// Start is called before the first frame update
	void Start()
    {
		n_NowSetAbility = cn_None;
		n_NextAbility = cn_None;

		i_IconSprite = this.GetComponent<Image>();
		i_IconSprite.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	}

    // Update is called once per frame
    void Update()
    {
		if (n_NextAbility == n_NowSetAbility) return;

		// 予期していない値が入らないように
		int ability_next_num = IsUseIcon(n_NextAbility);
		if (ability_next_num == cn_None) return;

		// 今使える能力をステージに落とす
		int now_ability = IsUseIcon(n_NowSetAbility);
		if (now_ability != cn_None) lac_Ability[now_ability].AbilityRelease();

		n_NowSetAbility = n_NextAbility;
		i_IconSprite.sprite = ls_Ability[ability_next_num];
		i_IconSprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

	// 万が一、予期していない値が入った時用
	int IsUseIcon(int _check)
	{
		for(int i = 0;i < ls_Ability.Count; i++) {
			if (ln_AbilityNumber[i] == _check) return i;
		}
		return cn_None;
	}
}
