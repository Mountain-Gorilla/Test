using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquarius : MonoBehaviour
{
	[SerializeField]
	Animator an_Effect;
	SpriteRenderer sr_Sprite;

	AnimeEnd ae_Check;

	// Start is called before the first frame update
	void Awake()
	{
		ae_Check = this.gameObject.GetComponent<AnimeEnd>();
		sr_Sprite = this.gameObject.GetComponent<SpriteRenderer>();
	}

	void OnEnable()
	{
		an_Effect.Play("Shield");
	}

	void Update()
	{

	}

	public bool IsAnimeEnd()
	{
		if (ae_Check.IsAnimeEnd()) return true;
		return false;
	}
}
