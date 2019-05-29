using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aries : MonoBehaviour
{
    GameObject g_HPGauge;　//呼び出し
    GameObject g_HP; //呼び出し
    GameObject AriesHeal; 

    private AriesHeal g_AriesHeal;

	private AnimeEnd ae_Effect;
	[SerializeField]
	private Animator an_Effect;

    public float HealAmount = 1f; //回復量
    public float Limit = 0;
    private float f_Timer = 0.0f;           //タイマー
    private const float cf_Wait = 0.0f;     //硬直(溜め？)時間
    private const float cf_Active = 3.0f;   //発動時間

    private bool b_ActiveFlg;               //発動フラグ(true:発動、false:硬直)

    /*=============================================*/
    // 初期化
    /*=============================================*/
    void Start()
    {
        g_HPGauge = GameObject.Find("HPGauge");
        g_HP = GameObject.Find("HP");
        AriesHeal = GameObject.Find("AriesHeal");
        AriesHeal.SetActive(false);
		//g_AriesHeal = AriesHeal.GetComponent<AriesHeal>;
		ae_Effect = gameObject.GetComponent<AnimeEnd>();
        f_Timer = 0.0f;
    }
    /*=============================================*/
    // アクティブ切り替え時処理
    /*=============================================*/
    private void OnEnable()
    {
		an_Effect.Play("Heal");
        b_ActiveFlg = false;
        f_Timer = 0.0f;
    }

    /*=============================================*/
    // 更新
    /*=============================================*/
    void Update()
    {
        f_Timer += Time.deltaTime;
        //能力発動条件
        if ((f_Timer > cf_Wait) && (Limit <= 0) && b_ActiveFlg == false)
        {
            b_ActiveFlg = true;
            Limit++;
        }

        //能力終了条件
        if((f_Timer > cf_Active) && b_ActiveFlg == true)
        {
            b_ActiveFlg = false;
            f_Timer = 0.0f;
        }

        //発動処理
        if (b_ActiveFlg == true)
        {
            AriesHeal.SetActive(true);
            AriesHeal.GetComponent<Image>().enabled = true;
            AriesHeal.GetComponent<AriesHeal>().enabled = true;

        }
        else
        {

        }                   
    //    Debug.Log(f_Timer);
        //Debug.Log(b_ActiveFlg);
    }
}        