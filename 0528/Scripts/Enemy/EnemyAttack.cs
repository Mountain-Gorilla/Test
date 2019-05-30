using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private Rigidbody2D ri_Pysic;
    private EnemyState es_EnemyState;

	/*===============================*/
	// 初期化
	/*===============================*/
	void Start()
    {
        ri_Pysic = this.GetComponent<Rigidbody2D>();
        es_EnemyState = this.GetComponent<EnemyState>();
    }

    /*===============================*/
    // 更新
    /*===============================*/
    void Update()
    {
        //プレイヤーを見つけたら攻撃開始
        if (!(es_EnemyState.n_State==2)) return;
        
    }
}
