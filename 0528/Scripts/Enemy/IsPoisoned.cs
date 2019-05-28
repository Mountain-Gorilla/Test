using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsPoisoned : MonoBehaviour
{
    EnemyState es_EnemyState;
    SpriteRenderer sp_SpriteRenderer;

    // 初期化
    void Start()
    {
        es_EnemyState = transform.root.parent.GetComponent<EnemyState>();
        sp_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 更新
    void Update()
    {
        //毒状態の時アクティブ
        if (es_EnemyState.n_Condition == 1)
        {
            sp_SpriteRenderer.enabled = true;
        }
        else
        {
            sp_SpriteRenderer.enabled = false;
        }
    }
}
