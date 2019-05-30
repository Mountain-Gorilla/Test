using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPDirector : MonoBehaviour
{
    GameObject g_HPGauge;

	// Use this for initialization
	void Start ()
    {
        g_HPGauge = GameObject.Find("HPGauge");    	
	}
	
	public void DecreaseHP(float _decrease,float _max)
    {
        g_HPGauge.GetComponent<Image>().fillAmount = (_decrease / _max);
		//Debug.Log(g_HPGauge.GetComponent<Image>().fillAmount);
    }

    public void IncreaseHP(float _increase, float _max)
    {
        g_HPGauge.GetComponent<Image>().fillAmount += (_increase / _max);
    }

	public void Reset()
	{
		g_HPGauge.GetComponent<Image>().fillAmount = 1.0f;
		Debug.Log(g_HPGauge.GetComponent<Image>().fillAmount);
	}

}
