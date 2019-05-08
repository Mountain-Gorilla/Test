using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Selected : MonoBehaviour
{
    /*==============================================================*/
    // 初期化
    /*==============================================================*/
    void Start()
    {
        GameObject g_back = GameObject.Find("Back");
        g_back.GetComponent<Anime>().Change(0);
    }

    /*==============================================================*/
    //　更新
    /*==============================================================*/
    void Update()
    {
        //左クリック時
        if(Input.GetKeyDown(KeyCode.F)){
            SceneManager.LoadScene("ForestScene");

        }
    }


}
