using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public  float f_Hp;

    private const float cf_WalkForce = 30.0f;
    private const float cf_MaxWalkSpeed = 2.0f;
    private enum        Direction{ Left,Right};
    private int         n_LeftRightFlg = (int)Direction.Left;

    private Animator    an_Mortion;
    private Rigidbody2D g_Rigid2D;

    /*===============================================*/
    // 初期化
    /*===============================================*/
    void Start()
    {
        g_Rigid2D = GetComponent<Rigidbody2D>();
        an_Mortion = GetComponent<Animator>();

        n_LeftRightFlg = -1;
    }


    /*===============================================*/
    // 更新
    /*===============================================*/
    void Update()
    {
        //現在の移動速度
        float speed_x = Mathf.Abs(this.g_Rigid2D.velocity.x);

        //スピード制限
        if (speed_x < cf_MaxWalkSpeed){
            this.g_Rigid2D.AddForce(transform.right * n_LeftRightFlg * cf_WalkForce);
            an_Mortion.SetTrigger("WalkTrigger");
        }

        //if (speed_x == 0.0f) an_Mortion.SetTrigger("StayTrigger"); //止まっているときにやりたい
        if (n_LeftRightFlg != 0) transform.localScale = new Vector3(n_LeftRightFlg * -1.0f, 1.0f, 1);
    }
}
