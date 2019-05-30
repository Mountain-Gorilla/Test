using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AriesHeal : MonoBehaviour
{
	[SerializeField]
	GameObject g_HPGauge = default; //呼び出し

	[SerializeField]
	HP g_HP = default;　//呼び出し
    public float HealAmount = 1f; //ゲージ回復量
    public float HPHeal;//HP回復量
   
    /*=============================================*/
    // 初期化
    /*=============================================*/
    void Start()
    {

    }

	void OnEnable()
	{
		g_HPGauge.GetComponent<Image>().fillAmount = 1.0f; //ゲージ回復処理
		g_HP.Increase(600.0f, 600.0f);//HP回復処理
	}

	/*=============================================*/
	// 更新
	/*=============================================*/
	void Update()
    {
		
		//if (f_Timer <= 10)//10秒間ずっと回す
  //      {
  //          f_Timer += Time.deltaTime;
  //          g_HPGauge.GetComponent<Image>().fillAmount = 1.0f;　//ゲージ回復処理
  //          g_HP.Increase(500.0f,500.0f);//HP回復処理
  //          Debug.Log(f_Timer);
  //      }else
  //      {
  //          f_Timer = 0;　//初期化
  //          this.gameObject.SetActive(false);
  //      }
    }
}
