using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour
{

    Sprite[] Sprite_List;
    
    Vector3 v_Back_Pos      = new Vector3(100.0f, 110.0f, 0.0f);
    Vector3 v_Back_Size     = new Vector3(0.2f, 0.2f, 0.2f);
    Vector3 v_Sprite_Size   = new Vector3(0.7f, 0.7f, 0.7f);

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
        if(Input.GetMouseButtonDown(1)){
            Delete();
        }
    }

    /*==============================================================*/
    // オブジェクトを消す(ZXCの順で
    /*==============================================================*/
    private Sprite None;
    void Delete()
    {

        if (Find("Z")){
            GameObject Delete_Object = GameObject.Find("Z");
            Delete_Object.GetComponent<SpriteRenderer>().sprite = None;
        }
        else if (Find("X")){
            GameObject Delete_Object = GameObject.Find("X");
            Delete_Object.GetComponent<SpriteRenderer>().sprite = None;
        }
        else if (Find("C")){
            GameObject Delete_Object = GameObject.Find("C");
            Delete_Object.GetComponent<SpriteRenderer>().sprite = None;
        }
    }

    bool Find(string _object)
    {
        GameObject g_Test=GameObject.Find(_object);
        if(g_Test.GetComponent<SpriteRenderer>().sprite!=None)
        {
            return true;
        }
        return false;
    }
    
    /*==============================================================*/
    // スプリクトのロード
    // 引数   : フォルダの名前
    /*==============================================================*/
    void Load(string fileName)
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>(fileName);
        Sprite_List = sprites;
    }

}
