using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //敵
    public GameObject g_Enemy1;
   
    // 敵の状態を管理
    private enum EnemyState
    {
        None,Enemy1
    }
    //private int n_Status = 0;

    // ロード時に実行
    void Start()
    {
        g_Enemy1.SetActive(true);
    }



    // 更新
    void Update()
    {
        

    }
}
