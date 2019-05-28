using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGround : MonoBehaviour
{
    private GameObject g_Parent;
    private EnemyState es_State;

    //===========================
    // 初期化
    //===========================
    void Start()
    {
        g_Parent = transform.root.gameObject;
        es_State = g_Parent.GetComponent<EnemyState>();
    }

    //===========================
    // 更新
    //===========================
    void Update()
    {
        
    }

    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "TileMap"){
            es_State.b_OnGround = true;
            //Debug.Log("OK");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "TileMap"){
            es_State.b_OnGround = false;
           // Debug.Log("JUMP");
        }
    }
}
