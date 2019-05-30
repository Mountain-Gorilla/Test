using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiDirector : MonoBehaviour
{
	[SerializeField]
	GameObject g_Gemini = default;

	[SerializeField]
	List<GameObject> lg_Attack = new List<GameObject>();

	[SerializeField]
	NowAbility na_NowAbilityNumber = default;

	const int cn_NoneAttack = 12;
	int n_NextAttackAbility = cn_NoneAttack;
	int n_NowAttackAbility = cn_NoneAttack;

	// 発動する攻撃の配列番号を取得
	public void SharingAttackNumber() { n_NextAttackAbility = na_NowAbilityNumber.SharingGeminiAttackNum(); }

	// 攻撃終了
	public void EndAttack() { n_NextAttackAbility = cn_NoneAttack; }

    // Start is called before the first frame update
    void Awake()
    {
		g_Gemini.SetActive(false);
    }

	void OnEnable()
	{
		g_Gemini.SetActive(true);

		n_NextAttackAbility = cn_NoneAttack;
		n_NowAttackAbility = cn_NoneAttack;

		for (int i = 0; i < lg_Attack.Count; i++) lg_Attack[i].SetActive(false);
	}

	void OnDisable()
	{
		g_Gemini.SetActive(false);

		n_NextAttackAbility = cn_NoneAttack;
		n_NowAttackAbility = cn_NoneAttack;

		for (int i = 0; i < lg_Attack.Count; i++) lg_Attack[i].SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {
		if (n_NowAttackAbility == n_NextAttackAbility) return;

		// 発動終了時
		if (n_NextAttackAbility == cn_NoneAttack) {
			lg_Attack[n_NowAttackAbility].SetActive(false);
			n_NowAttackAbility = n_NextAttackAbility;
			return;
		}

		// 能力発動
		n_NowAttackAbility = n_NextAttackAbility;
		Debug.Log(n_NextAttackAbility);
		lg_Attack[n_NowAttackAbility].SetActive(true);
		
    }
}
