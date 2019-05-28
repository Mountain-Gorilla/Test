using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sagittarius : MonoBehaviour
{
	//private int n_KeyDownCode = 0;

	private GameObject g_Arrow;

    // Start is called before the first frame update
    void Start()
    {
		g_Arrow = GameObject.Find("Arrow");
		g_Arrow.SetActive(true);
    }

	void OnEnable()
	{
		g_Arrow.SetActive(true);
	}

	void OnDisable()
	{
		g_Arrow.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {

    }
}
