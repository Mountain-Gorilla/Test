using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
	[SerializeField]
	private Animator an_Rush = default;

	AnimeEnd ae_Check;

	// Start is called before the first frame update
	void Start()
    {
		ae_Check = this.gameObject.GetComponent<AnimeEnd>();
		OnEnable();
	}

	void OnEnable()
	{
		an_Rush.Play("Rush");
	}

	// Update is called once per frame
	void Update()
    {
        
    }

	public bool IsAnimeEnd()
	{
		if (ae_Check.IsAnimeEnd()) return true;
		return false;
	}
}
