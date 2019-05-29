﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    enum Boss
    {
        Snake,
        Club
    };


    [SerializeField]
    private int i_LastBossNum = 2;
    [SerializeField]
        private GameObject[] BossList;
    int i_BossNum = 0;
    GameObject g_Boss;

    GameObject g_Cam;
    void Start()
    {
        g_Boss = Instantiate(BossList[i_BossNum]);
        g_Boss.name = ("Boss");
        g_Cam = GameObject.Find("Main Camera");

    }

    void Update()
    {

        if (g_Cam.transform.position.x - 20.0f > g_Boss.transform.position.x)
        {
            Destroy(g_Boss);
            i_BossNum++;
            if (i_BossNum >= i_LastBossNum) return;
            g_Boss = Instantiate(BossList[i_BossNum]);
            g_Boss.name = ("Boss");
        }
    }

    // クリア時
    float f_Timer = 0.0f;


    public bool IsGameClear()
    {
        f_Timer += Time.deltaTime;
        if (f_Timer <= 1.0f) return false;
        if (i_BossNum < i_LastBossNum) return false;
        return true;
    }

}


