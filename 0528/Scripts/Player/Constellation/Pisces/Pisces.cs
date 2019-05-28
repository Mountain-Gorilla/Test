using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pisces : MonoBehaviour
{
    GameObject g_Player;
    private Move g_Move;
    private int framecount = 0;     //フレームカウント
    private float f_Timer = 0.0f;           //タイマー
    private const float cf_Wait = 0.05f;     //硬直(溜め？)時間
    private const float cf_Active = 0.3f;   //発動時間
    private float cf_MaxSpeed = 0.25f;        //加速度最大
    private float cf_AccelOnce = 0.125f;       // 一度の加速量
    private bool b_ActiveFlg;               //発動フラグ(true:発動、false:硬直)
    private bool InvicbleFlg; //無敵フラグ
    public bool RightStep; //右回避フラグ
    public bool LeftStep; //左回避フラグ

    void Start()
    {
        g_Player = GameObject.Find("Player");
        f_Timer = 0.0f;
        b_ActiveFlg = false;
        g_Move = new Move();
        InvicbleFlg = true;
        LeftStep = false;
        RightStep = false;
    }
    /*=============================================*/
    // アクティブ切り替え時処理
    /*=============================================*/
    private void OnEnable()
    {
        b_ActiveFlg = false;
        f_Timer = 0.0f;
        LeftStep = false;
        RightStep = false;
        InvicbleFlg = true;
        framecount = 0;

		g_Player.gameObject.layer = 11;
    }
    /*=============================================*/
    // 更新
    /*=============================================*/
    void Update()
    {
        f_Timer += Time.deltaTime;
        //能力発動条件
        if ((f_Timer > cf_Wait) && b_ActiveFlg == false)
        {
            b_ActiveFlg = true;
        }
        //能力終了条件
        if ((f_Timer >= cf_Active) && b_ActiveFlg == true)
        {
            b_ActiveFlg = false;
            f_Timer = 0.0f;
        }
        //発動処理
        if (b_ActiveFlg == true)
        {
            if (InvicbleFlg == true)//フラグ判定
            {
                if (g_Player.GetComponent<Player>().IsDirection() == -1)//左を向いているとき
                {
                    LeftStep = true;//左回避フラグ呼び出し
                    InvicbleFlg = false;
                }
                if (g_Player.GetComponent<Player>().IsDirection() == 1)//右を向いているとき
                {
                    RightStep = true;//右回避フラグ呼びだし
                    InvicbleFlg = false;
                }
            }
        }
        //回避処理
        if (LeftStep == true)//左回避処理
        {
            g_Player.transform.Translate(g_Move.AccelerateLeft(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
            framecount++;
            if (framecount < 15){
                g_Player.layer = 10;
            }else { 
                g_Player.layer = 8;
            }
        }
        if (RightStep == true)//右回避処理
        {
            g_Player.transform.Translate(g_Move.AccelerateRight(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
            framecount++;
            if (framecount < 15){
                g_Player.layer = 10;
            }
            else{
                g_Player.layer = 8;
            }
        }
    }
}
/*導入のさい、Unity側で設定する項目2つがございます
 ①HierarchyのPlayerのInspectorの項目でLayer設定を作成します。layer10を設定する必要があります。
 ②UnityのEditでProject Settingを選択します。physics 2Dを選択し、一番の下のLayerのあたり判定設定があります。
 Enemyと無敵判定用layer10の項目を外します。
     */