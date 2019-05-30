using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class IconChange : MonoBehaviour
{
	// 12宮の能力発動を管理
	private enum ConstellationState
	{
		Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
		Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, None
	}
	private int n_NowStatus = (int)ConstellationState.Aries;      // 現在の能力
	private int n_NextState;                                      // 次の能力

	// 現在のアイコンの画像
	[SerializeField]
    private Image i_IconSprite;

	// 能力のアイコン画像
	[SerializeField]
	private List<Sprite> ls_ChangeIcon = new List<Sprite>();

	// アルファ値の設定
	[SerializeField]
	private float f_Alpha = 0.5f;

	//[SerializeField]
	//private int[] n_AbilityNum;

	[SerializeField]
	List<GameObject> g_Constellation = new List<GameObject>();
	
	// 能力の共有
	public void SharingState(int _share) { n_NextState = _share; }

	public Sprite GetSprite() { return ls_ChangeIcon[n_NextState]; }

	void Start()
	{
		n_NowStatus = (int)ConstellationState.None;
		n_NextState = n_NowStatus;
		i_IconSprite = this.GetComponent<Image>();
		i_IconSprite.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
	}

	// 選択する能力の変更
	public int ChangeIcon()
	{
		return n_NextState;
	}

	// 能力があるかどうか
	int CheckAbility(int _check_ability,bool _isplus)
	{
		// どちらに参照するか
		int check;
		if (_isplus) {
			check = _check_ability + 1;
			if (check > (int)ConstellationState.Pisces) check = (int)ConstellationState.Aries;
		}
		else {
			check = _check_ability - 1;
			if (check < (int)ConstellationState.Aries) check = (int)ConstellationState.Pisces;
		}

		// オブジェクトのスクリプトを取得
		AbilityCheck ability_script = g_Constellation[check].GetComponent<AbilityCheck>();

		if (ability_script.IsUseAbility() != check) check = CheckAbility(check, _isplus);

		return check;
		
		
	}

	// Update is called once per frame
	void Update()
	{
		// 参照先が一緒ならリターン
		if (n_NowStatus == n_NextState) return;

		// 画像切り替え
		n_NowStatus = n_NextState;
		i_IconSprite.sprite = ls_ChangeIcon[n_NowStatus];
		i_IconSprite.color = new Color(1.0f, 1.0f, 1.0f, f_Alpha);

		Debug.Log(n_NowStatus);
	}
}
