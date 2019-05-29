using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*=============================================*/
// 画像を表示、非表示を切り替える
/*=============================================*/

public class ActiveManager : MonoBehaviour
{
	
	// 12宮の能力発動を管理
	private enum ConstellationState
	{
		Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
		Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, None
	}

	// 能力発動キーの管理
	private enum AbilityKey
	{
		Z, X, C, Max, None = Max
	}

	private bool[]             b_Ability = new bool[(int)AbilityKey.Max];
	private int[]              n_Status = new int[(int)AbilityKey.Max];
	private int[]              n_RecoveryState = new int[(int)AbilityKey.Max];
	private const int          cn_ConstellationMax = 12;  // 12宮の最大個数
	private int                n_NowStatus;  // 現在の姿(能力変更まで変化なし)

	/*=============================*/
	// 現在取得している能力の管理
	/*=============================*/
	
	[SerializeField]
	List<GameObject>           g_Constellation = new List<GameObject>();

	// 能力の発動時間
	private float[]   f_Timer = new float[(int)AbilityKey.Max];
	private float[]   cf_Span = new float[cn_ConstellationMax];

	/*===========*/
	// 差し替え
	/*===========*/
	// 画像
	private GameObject         g_Player;
	private SpriteChange       g_SpriteChange;

	// アイコン管理
	private GameObject   g_IconDirector;
	private IconDirector id_Director;

	// 変身
	private GameObject   g_Transform;
	private TransformationDirector td_Script;

	// サウンド
	[SerializeField]
	List<AudioClip> lac_Sound = new List<AudioClip>();

	GameObject g_SE;
	AudioSource as_Source;

	// ロード時に実行
	void Start()
    {
		g_Player = GameObject.Find("Player");
		g_SpriteChange = g_Player.GetComponent<SpriteChange>();

		g_IconDirector = GameObject.Find("IconDirector");
		id_Director = g_IconDirector.GetComponent<IconDirector>();

		g_Transform = GameObject.Find("TransformDirector");
		td_Script = g_Transform.GetComponent<TransformationDirector>();

		g_SE = GameObject.Find("ConsteSE");
		as_Source = g_SE.GetComponent<AudioSource>();

		for (int i = 0; i < g_Constellation.Count; i++) {
			g_Constellation[i].SetActive(false);

			if (i >= (int)AbilityKey.Max) continue;
			f_Timer[i] = 0.0f;
			b_Ability[i] = false;
			n_Status[i] = (int)ConstellationState.None;
			n_RecoveryState[i] = n_Status[i];
		}

		n_NowStatus = (int)ConstellationState.None;

		SetSpan();
	}

	// タイマーの制限時間の設定
	void SetSpan()
	{
		cf_Span[(int)ConstellationState.Aries]       = 10.0f;
		cf_Span[(int)ConstellationState.Taurus]      = 3.0f;
		cf_Span[(int)ConstellationState.Gemini]      = 10.0f;
		cf_Span[(int)ConstellationState.Cancer]      = 3.0f;
		cf_Span[(int)ConstellationState.Leo]         = 0.8f;
		cf_Span[(int)ConstellationState.Virgo]       = 4.0f;
		cf_Span[(int)ConstellationState.Libra]       = 15.0f;
		cf_Span[(int)ConstellationState.Scorpio]     = 3.0f;
		cf_Span[(int)ConstellationState.Sagittarius] = 1.0f;
		cf_Span[(int)ConstellationState.Capricorn]   = 5.0f;
		cf_Span[(int)ConstellationState.Aquarius]    = 3.0f;
		cf_Span[(int)ConstellationState.Pisces]      = 0.35f;
	}

	// 能力発動中
	private void AbilityInvocating(int _index)
    {
        if (n_Status[_index] == (int)ConstellationState.None) return;

        f_Timer[_index] += Time.deltaTime;
		id_Director.Recovery(_index , cf_Span[n_Status[_index]] - f_Timer[_index], cf_Span[n_Status[_index]]);

		if (f_Timer[_index] > cf_Span[n_Status[_index]]) {
			g_Constellation[n_Status[_index]].SetActive(false);
			//id_Director.UseAbility();
			n_RecoveryState[_index] = n_Status[_index];
			n_Status[_index] = (int)ConstellationState.None;
			f_Timer[_index] = 0.0f;
		}
	}

	void AbilityInterval(int _index)
	{
		if (n_RecoveryState[_index] == (int)ConstellationState.None) return;

		f_Timer[_index] += Time.deltaTime;
		id_Director.Recovery(_index,f_Timer[_index], cf_Span[n_RecoveryState[_index]]);
		if (f_Timer[_index] > cf_Span[n_RecoveryState[_index]]) {
			n_RecoveryState[_index] = (int)ConstellationState.None;
			f_Timer[_index] = 0.0f;
			b_Ability[_index] = false;
			id_Director.DestroyFlag(_index);
		}
	}

    // 更新
    void Update ()
    {
        GameObject g_Boss = GameObject.Find("Boss");
        Scene s_BossScene = g_Boss.GetComponent<Scene>();        
        if (s_BossScene.GetStart()) return;
		//AbilitySelect();
		for (int i = 0; i < (int)AbilityKey.Max; i++){

			SetAbility(i);

			IsTransformation(i);

			AbilityInvocating(i);

			AbilityInterval(i);

			Debug.Log(n_Status[i]);
		}

        //Debug.Log(n_Status);
    }

	// 能力の選択
	bool IsTransformation(int _index)
	{
		if (!b_Ability[_index]) return false;

		if (td_Script.IsEndAnimation(g_Player.transform.position)) {
			n_Status[_index] = id_Director.GetAbility(_index);
			n_NowStatus = n_Status[_index];
			g_Constellation[n_Status[_index]].SetActive(true);
			g_SpriteChange.SharingState(n_Status[_index]);
			id_Director.SetAbility(_index, n_Status[_index]);
			return true;
		}
		return false;
	}

	// 発動時
	void Invocation(bool _now_state,int _index)
	{
		if (id_Director.GetAbility(_index) == (int)ConstellationState.None) {
			b_Ability[_index] = false;
			return;
		}

		if (!_now_state) {
			td_Script.StartTransform(_index);
			return;
		}

		n_Status[_index] = id_Director.GetAbility(_index);
		g_Constellation[n_Status[_index]].SetActive(true);
		g_SpriteChange.SharingState(n_Status[_index]);
		id_Director.SetAbility(_index, n_Status[_index]);
		as_Source.PlayOneShot(lac_Sound[n_Status[_index]]);
		
	}

	// 能力の発動
	void SetAbility(int _index)
	{
		if (n_Status[_index] != (int)ConstellationState.None || n_RecoveryState[_index] != (int)ConstellationState.None) return;
		
		// 能力の発動
		if ((Input.GetKey(KeyCode.Z) && _index == (int)AbilityKey.Z) ||
			(Input.GetKey(KeyCode.X) && _index == (int)AbilityKey.X) ||
			(Input.GetKey(KeyCode.C) && _index == (int)AbilityKey.C)) {

			b_Ability[_index] = true;
			Invocation(n_NowStatus == id_Director.GetAbility(_index),_index);
		}
	}

}
