using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taurus : MonoBehaviour
{
    private GameObject  g_Player;           //プレイヤー
    private Player g_Script;
	private Animator an_Rush;

	private GameObject g_Rush;

    private float f_Timer = 0.0f;           //タイマー
    private const float cf_Wait = 1.0f;     //硬直(溜め？)時間
    private const float cf_Active = 2.0f;   //発動時間

    private const float cf_Move = 0.5f;     //移動量

    private bool b_ActiveFlg;               //発動フラグ(true:発動、false:硬直)

	private bool b_WallJudge;                // 壁との当たり判定

    /*=============================================*/
    // 初期化
    /*=============================================*/
    void Start()
    {
        g_Player = GameObject.Find("Player");
		g_Script = g_Player.GetComponent<Player>();
		an_Rush = g_Player.GetComponent<Animator>();

		g_Rush = GameObject.Find("Rush");
		g_Rush.SetActive(false);

		b_ActiveFlg = false;
		b_WallJudge = false;
        f_Timer = 0.0f;
    }

    /*=============================================*/
    // アクティブ切り替え時処理
    /*=============================================*/
    void OnEnable()
    {
		g_Rush.SetActive(true);
		b_WallJudge = false;
        b_ActiveFlg = false;
        f_Timer = 0.0f;
    }

	void OnDisable()
	{
		an_Rush.Play("Stay");
		g_Rush.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D _wall)
	{
		if(_wall.gameObject.tag == "TileMap"){
			b_WallJudge = true;
		}
	}

	void OnTriggerExit2D(Collider2D _wall)
	{
		if (_wall.gameObject.tag == "TileMap") {
			b_WallJudge = false;
		}
	}

    /*=============================================*/
    // 更新
    /*=============================================*/
    void Update()
    {
        f_Timer += Time.deltaTime;

		if (b_WallJudge) {
			Debug.Log("当たっている");
			return;
		}

		//硬直終了時
		if ((f_Timer > cf_Wait) && b_ActiveFlg == false) {
            b_ActiveFlg = true;
            f_Timer = 0.0f;
        }

        ////能力発動終了時
        if ((f_Timer > cf_Active) && b_ActiveFlg == true) {
			g_Rush.SetActive(true);
			b_ActiveFlg = false;
            f_Timer = 0.0f;
        }

        //発動時処理
        if (b_ActiveFlg){
            g_Player.transform.Translate(g_Script.IsDirection() * cf_Move, 0, 0);
			an_Rush.Play("Rush");
        }
        //硬直時処理
        else{

        }

        //Debug.Log(f_Timer);
        //Debug.Log(b_ActiveFlg);
        
    }
}
