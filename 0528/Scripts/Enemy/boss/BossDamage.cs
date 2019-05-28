using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamage : MonoBehaviour
{
    GameObject g_Player;                                //プレイヤー情報取得用   

    private BossState bs_State;                        //ステータス

    private bool b_Flag = false;                        //ダメージを受けたか
    private float f_Anger;                               //怒り値

    // Start is called before the first frame update
    void Start()
    {
        bs_State = gameObject.GetComponent<BossState>();

        f_Anger = 0.0f;
        b_Flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        //怒り値一定以上で特殊攻撃
        if (f_Anger >= bs_State.f_Anger)
        {
            bs_State.b_SpecialAttack = true;
        }
    }

    void OnTriggerEnter2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag == "PlayerAttack")
        { 
            //ステートを怯みに
            bs_State.n_State = 3;
            //HP減少
            bs_State.f_Hp = bs_State.f_Hp - 1.0f;
            //怒り値減少
            f_Anger += 1.0f;

        }
    }
}
