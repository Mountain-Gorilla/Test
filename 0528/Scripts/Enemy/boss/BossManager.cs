using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField] private GameObject[] BossList;
    int i_BossNum = 0;
    GameObject Boss;

    GameObject g_Cam;
    void Start()
    {
        Boss = Instantiate(BossList[i_BossNum]);
        Boss.name = ("Boss");
        i_BossNum++;

        g_Cam = GameObject.Find("Main Camera");

    }

    void Update()
    {

        if (g_Cam.transform.position.x - 20.0f > Boss.transform.position.x)
        {
            Destroy(Boss);
            Boss = Instantiate(BossList[i_BossNum]);
            Boss.name = ("Boss");
            i_BossNum++;
        }

    }

}


