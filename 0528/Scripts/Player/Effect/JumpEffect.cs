using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
	[SerializeField]
	Animator an_Effect;

	[SerializeField]
	GameObject g_Player;

	AnimeEnd ae_Check;

	// Start is called before the first frame update
	void Start()
    {
		
    }

	void OnEnable()
	{
		transform.position = g_Player.transform.position;
		ae_Check = this.gameObject.GetComponent<AnimeEnd>();
		an_Effect.Play("Jump_Effect");
	}

   public bool IsAnimeEnd()
	{
		if (ae_Check.IsAnimeEnd()) return true;
		return false;
	}
}
