using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconDirector : MonoBehaviour
{
	// アイコン
	[SerializeField]
	List<GameObject> lg_Icon = new List<GameObject>();

	[SerializeField]
	List<IconGauge>  lig_Gauge = new List<IconGauge>();

	[SerializeField]
	List<NowAbility> lna_Ability = new List<NowAbility>();

	[SerializeField]
	List<IconHalf>   lih_Half = new List<IconHalf>();

	bool[]   lb_Ability = new bool[3];

	// Start is called before the first frame update
	void Start()
    {
		for (int i = 0; i < lg_Icon.Count; i++) {
			lb_Ability[i] = false;
		}
	}
	
	public int GetAbility(int _index)
	{
		return lna_Ability[_index].GetNowAbility();
	}

	public void Recovery(int _index,float _recovery,float _max)
	{
		if (!lb_Ability[_index]) return;
		lig_Gauge[_index].Recovery(_recovery, _max);
	}

	public void DestroyFlag(int _index) { lb_Ability[_index] = false; }

    public void SetAbility(int _index ,int _status)
	{
		lb_Ability[_index] = true;
		
		// アイコン
		//lih_Half[_index].SetSprite(lna_Ability[_index].GetNowSprite());
	}
}
