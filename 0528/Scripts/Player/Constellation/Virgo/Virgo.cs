using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virgo : MonoBehaviour
{
	[SerializeField]
	Animator an_Charm;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	void OnEnable()
	{
		an_Charm.Play("Charm");
	}
}
