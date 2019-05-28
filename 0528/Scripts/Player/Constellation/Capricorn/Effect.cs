using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
	[SerializeField]
	Animator an_Effect;

	AnimeEnd ae_Check;

	// Start is called before the first frame update
	void Start()
	{
		ae_Check = this.gameObject.GetComponent<AnimeEnd>();
	}

	void OnEnable()
	{
		an_Effect.Play("Capricorn");
	}

	public bool IsAnimeEnd()
	{
		if (ae_Check.IsAnimeEnd()) return true;
		return false;
	}
}
