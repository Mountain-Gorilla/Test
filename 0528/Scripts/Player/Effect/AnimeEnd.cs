using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeEnd : MonoBehaviour
{
	[SerializeField]
	Animator an_Anime;

	public bool IsAnimeEnd()
	{
		if (an_Anime.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) return true;
		return false;
	}
}
