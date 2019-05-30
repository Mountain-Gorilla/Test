using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
    //プライベート化するか要検討

    public int n_State = 0;                 //状態（0:待機、1:移動、2:攻撃、3:怯み） 

    public bool b_ToPlayer = false;         //プレイヤーのいる方向(左:false右:true)
    public bool b_DamageFlag = false;       //ダメージを受けたか
    public bool b_Alive = true;             //生存フラグ
    public bool b_Find = false;             //プレイヤー発見フラグ
    public bool b_SpecialAttack = false;    //特殊攻撃フラグ
    public float f_Anger = 20.0f;           //怒り値（これを超えたら特殊攻撃？）
    public float f_Hp = 30.0f;              //HP
    bool b_DeadAnime = false;
    private Animator an_Motion;

    GameObject g_Player;
    GameObject g_Cam;
    GameObject g_Boss;

    // クリア時
    float f_Timer=0.0f;
    bool b_GameClear;


    public bool IsGameClear()
    {
        return g_Boss.GetComponent<BossManager>().IsGameClear();
    }
    /*========================================*/
    // 初期化
    /*========================================*/
    void Start()
    {
        an_Motion = GetComponent<Animator>();
        g_Cam = GameObject.Find("Main Camera");

        g_Player = GameObject.Find("Player");

        g_Boss = GameObject.Find("BossManager");
    }

    float m_AnimNum = 0.0f;
    /*========================================*/
    // 更新
    /*========================================*/
    void Update()
    {
        //プレイヤーのいる向き（左:false右;true）
        Vector2 v_dir = g_Player.transform.position - transform.position;
        if (v_dir.x > 0.0f) b_ToPlayer = true;  //右側
        if (v_dir.x < 0.0f) b_ToPlayer = false; //左側



        if (Input.GetKeyDown(KeyCode.P)) f_Hp = 0.0f;


        //
        if (f_Hp <= 0 && !b_DeadAnime){
            an_Motion.Play("Dead");
            b_DeadAnime = true;
            this.tag = "Enemy";
            return;
        }

        if(b_DeadAnime&&an_Motion.GetCurrentAnimatorStateInfo(0).normalizedTime>10.0f/11.0f){
            an_Motion.enabled = false;

            GameObject g_right_wall = GameObject.Find("BossRight");
            g_right_wall.GetComponent<Rigidbody2D>().simulated = false;

            GameObject g_wall = GameObject.Find("BossLeft");
            g_wall.GetComponent<Rigidbody2D>().simulated = false;
            g_Cam.GetComponent<Camera>().enabled = true;

            GetComponent<Rigidbody2D>().simulated = false;

            Destroy(this);
        }

    }


    public float GetHp() { return f_Hp; }
}
