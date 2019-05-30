using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{

    /*==============================================================*/
    //初期化
    /*==============================================================*/
    void Start()
    {
    }

    //ダブルクリック用
    bool  b_PushFlg     = false;
    float f_Click_Cnt   = 0.0f;

    /*==============================================================*/
    //更新
    /*==============================================================*/
    void Update()
    {
        if(b_PushFlg){
            f_Click_Cnt++;
        }

        if (f_Click_Cnt > 10){
            b_PushFlg   = false;
            f_Click_Cnt = 0.0f;
        }

    }


    /*==============================================================*/
    //左クリック時
    /*==============================================================*/
    void OnMouseDown()
    {

        if (b_PushFlg == false)
        {
            b_PushFlg   = true;
            return;
        }
        /*
        b_PushFlg = false;
        f_Click_Cnt = 0.0f;
        */
        //ゲームオブジェクトを見つけてくる
        if (Find("Z")){
            //Zがある            
            Search("X", "Z");
        }else{
            CreateObject("Z", "");
        }

        CreateStand();
        CreateAnime(this.GetComponent<SpriteRenderer>().sprite);
    }

    private enum ConstellationState
    {
        Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
        Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, None
    }

    /*******************************/
    // enumの番号の名前に変える
    // 引数 : 呼び出したい番号
    // 戻り値 : 変換した文字
    /*******************************/
    string ToString(int _num)
    {
        string name;        
        name = Enum.GetName(typeof(ConstellationState), _num);
        return name;        
    }


    public void CreateAnime(Sprite _sprite)
    {
        Sprite s_now_sprite = _sprite;
        string s_for_work = s_now_sprite.ToString();
        //数値の切り取り
        s_for_work = s_for_work.Substring(14, 1);

        GameObject create_obj = GameObject.Find("Anime");
        create_obj.GetComponent<SpriteChange>().SharingState(int.Parse(s_for_work));
        create_obj.GetComponent<Animator>().Play("Move");
    }

    /*==============================================================*/
    // ZXCの選択 
    // 引数   : 探したいオブジェクト、既存オブジェクト 
    /*==============================================================*/
    void Search(string _fand_name,string _existing_name)
    {
        if (Find(_fand_name)){
            //無限ループにため
            if (_fand_name == "C") return;
            Search("C", _fand_name);

        } else{
            CreateObject(_fand_name, _existing_name);
        }
    }

    /*==============================================================*/
    // オブジェクトを作る 
    // 引数   : 作りたいオブジェクト名、座標を求めるオブジェクト名
    /*==============================================================*/
    void CreateObject(string _create_name,string _existing_name)
    {
        GameObject create_obj = GameObject.Find(_create_name);

        Sprite s_now_sprite = this.GetComponent<SpriteRenderer>().sprite;
        string s_for_work = s_now_sprite.ToString();
        s_for_work = s_for_work.Substring(14, 1);

        if (!Image_Data_Check(s_now_sprite)) return;
        create_obj.GetComponent<SpriteRenderer>().sprite = s_now_sprite;

        
    }


    /*==============================================================*/
    //立ち絵の作成
    /*==============================================================*/
    void CreateStand()
    {
        Sprite s_now_sprite = this.GetComponent<SpriteRenderer>().sprite;

        GameObject g_stand = GameObject.Find("Stand");

        string s_for_work = s_now_sprite.ToString();
        s_for_work = s_for_work.Substring(14, 1);
        g_stand.GetComponent<Anime>().Change(int.Parse(s_for_work));
    }

    /*==============================================================*/
    // 画像データがすでに選ばれているか調べる 
    // 引数   : 調べたい画像名
    // 戻り値 : 存在する場合　false :ない場合　true
    /*==============================================================*/
    bool Image_Data_Check(object _Test)
    {
        if (Find("Z")) {
            GameObject g_test = GameObject.Find("Z");
            object o_test = g_test.GetComponent<SpriteRenderer>().sprite;
            if (o_test == _Test) return false;
        }

        if (Find("X")){
            GameObject g_test = GameObject.Find("X");
            object o_test = g_test.GetComponent<SpriteRenderer>().sprite;
            if (o_test == _Test) return false;
        }

        if (Find("C")){
            GameObject g_test = GameObject.Find("C");
            object o_test = g_test.GetComponent<SpriteRenderer>().sprite;
            if (o_test == _Test) return false;
        }

        return true;
    }

	/*==============================================================*/
	// オブジェクトがあるかどうか 
	// 引数   : 探したいオブジェク
	/*==============================================================*/
	private Sprite None = default;
    bool Find(string _Object)
    {
        GameObject g_Test = GameObject.Find(_Object);
        if (g_Test.GetComponent<SpriteRenderer>().sprite != None)
        {
            //何か画像選択中
            return true;
        }
        return false;
    }


   
}
