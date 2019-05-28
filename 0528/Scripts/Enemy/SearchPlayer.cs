using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchPlayer : MonoBehaviour {

    private GameObject g_Player;

    private EnemyState es_EnemyState;

    private float f_SearchLength = 0.0f;    //発見する距離
    private float f_AttackLength = 0.0f;    //攻撃できる距離
    private float f_MoveSpeed   = 0.05f;    // 追尾速度

    /*==========================*/
    // 初期化
    /*==========================*/
    void Start () {
        g_Player = GameObject.Find("Player");

        es_EnemyState = GetComponent<EnemyState>();

        f_SearchLength = es_EnemyState.f_SearchLength;
        f_AttackLength = es_EnemyState.f_AttackLength;
        f_MoveSpeed = es_EnemyState.f_MaxMoveSpeed;
    }

    /*===========================*/
    // 更新
    /*===========================*/
    void Update () {

        //怯み時はやらない
        if (es_EnemyState.n_State == 3) return;
        //魅了時もやらない
        if (es_EnemyState.n_Condition == 2) return;

        //プレイヤーの座標
        Vector3 PlayerPos = g_Player.transform.position;

        //敵の座標
        Vector3 Pos = transform.position;

        // プレイヤーと敵の間ベクトル
        Vector3 TmpVec = PlayerPos - Pos;

        // ベクトルの長さを求める
        float Length = TmpVec.magnitude;


        //状態初期化
        int state = 0;

        //発見範囲にいたら移動状態へ
        if (Length <= f_SearchLength) state = 1;
        //攻撃範囲にいたら攻撃状態へ
        if (Length <= f_AttackLength) state = 2;

        if (state != 0){
            es_EnemyState.n_State = state;
        }
	}
}
