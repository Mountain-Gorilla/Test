using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private float       f_MoveForce;
    private float       f_MaxWalkSpeed;

    private EnemyState  es_State;
    private Animator    an_Motion;
    private Rigidbody2D ri_Rigid2D;

    /*===============================================*/
    // 初期化
    /*===============================================*/
    void Start()
    {
        ri_Rigid2D = GetComponent<Rigidbody2D>();
        an_Motion = GetComponent<Animator>();
        es_State   = GetComponent<EnemyState>();

        //必要なステータスを取得
        f_MoveForce = es_State.f_MoveForce;
        f_MaxWalkSpeed = es_State.f_MaxMoveSpeed;
    }


    /*===============================================*/
    // 更新
    /*===============================================*/
    void Update()
    {
        if (es_State.n_State != 1) return;
        if (an_Motion.GetCurrentAnimatorStateInfo(0).IsName("Attack")) { return; }

        //現在の移動速度
        float speed_x = Mathf.Abs(ri_Rigid2D.velocity.x);

        //bool値→int値に
        int n_dir = es_State.b_ToPlayer ? 1 : -1;

        //スピード制限
        if (speed_x < f_MaxWalkSpeed){
            ri_Rigid2D.AddForce(transform.right * n_dir * f_MoveForce);
        }

        //向きに応じて画像反転
        transform.localScale = new Vector3(n_dir * -1.0f, 1.0f, 1);
    }
}
