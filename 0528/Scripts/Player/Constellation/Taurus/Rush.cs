using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rush : MonoBehaviour
{
	private Animator an_Rush;

    // Start is called before the first frame update
    void Start()
    {
		an_Rush = GetComponent<Animator>();
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
}
