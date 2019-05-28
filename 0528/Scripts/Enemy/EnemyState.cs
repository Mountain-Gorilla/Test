using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    //プライベート化するか要検討

    public int      n_State = 0;            //状態（0:待機、1:移動、2:攻撃、3:怯み） 
    public int      n_Condition = 0;        //状態異常（0:通常、1:毒、2:魅了）
    public bool     b_ToPlayer = false;     //プレイヤーのいる方向(左:false右:true)
    public bool     b_DamageFlag = false;   //ダメージを受けたか
    public bool     b_Alive = true;         //生存フラグ
    public bool     b_OnGround = false;     //接地フラグ
    public bool     b_Find = false;         //プレイヤー発見フラグ
    public bool     b_Attack = false;       //攻撃フラグ
    public float    f_StanSpan = 1.0f;      //スタン時間
    public float    f_Blow = 5.0f;          //吹き飛び値
    public float    f_Hp;                   //HP
    public float    f_MaxHp=10.0f;          //最大HP
    public float    f_SearchLength = 0.0f;  //プレイヤー探査範囲
    public float    f_AttackLength = 0.0f;  //プレイヤー攻撃範囲
    public float    f_MoveForce = 30.0f;    //移動力
    public float    f_MaxMoveSpeed = 2.0f;  //最大移動速度

    private float f_ConditionTimer = 0.0f;  //状態異常タイマー
    private Animator an_Motion;

    GameObject g_Player;

    /*========================================*/
    // 初期化
    /*========================================*/
    void Start()
    {
        //HPを設定
        f_Hp = f_MaxHp;

        an_Motion = GetComponent<Animator>();

        //プレイヤー取得
        g_Player = GameObject.Find("Player");
    }

    /*========================================*/
    // 更新
    /*========================================*/
    void Update()
    {
        //プレイヤーのいる向き（左:false右;true）
        Vector2 v_dir = g_Player.transform.position-transform.position;
        if (v_dir.x > 0.0f) b_ToPlayer = true;  //右側
        if (v_dir.x < 0.0f) b_ToPlayer = false; //左側
        

       

        //HP0でやられモーション変更
        if (f_Hp <= 0)
        {
            an_Motion.SetBool("Alive", false);
            b_Alive = false;
        }


    }

    void LateUpdate()
    {

        //状態異常処理
        //毒
        if (n_Condition == 1)
        {
            f_Hp -= 0.01f;
        }
        //魅了
        if (n_Condition == 2)
        {
            n_State = 0;
        }
        //状態異常タイマー
        if (n_Condition != 0)
        {
            f_ConditionTimer += Time.deltaTime;
            //3秒たったら
            if (f_ConditionTimer >= 3.0f)
            {
                //状態異常リセット
                n_Condition = 0;
                //タイマーもリセット
                f_ConditionTimer = 0.0f;
            }
        }

        //状態に応じてモーション変更
        switch (n_State)
        {
            //待機
            case 0:
                an_Motion.SetInteger("State", 0);
                break;
            //移動
            case 1:
                an_Motion.SetInteger("State", 1);
                break;
            //攻撃
            case 2:
                an_Motion.SetInteger("State", 2);
                break;
            //怯み
            case 3:
                an_Motion.SetInteger("State", 3);
                break;
        }

        //やられたか？
        if (!b_Alive)
        {
            //やられモーション一定まで終わったか
            if (an_Motion.GetCurrentAnimatorStateInfo(0).IsName("Die") &&
                an_Motion.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.75)
            {
                Destroy(gameObject);
            }
        }
    }
}

