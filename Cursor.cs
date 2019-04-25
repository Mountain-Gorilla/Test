using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cursor : MonoBehaviour
{
    public Sprite[] Constellation;
    int CONSTELLATION_MAX = 12;

    public static int[] a = new int[3];
    static int Get(int _i)
    {
        return a[_i];
    }
    private enum ConstellationState
    {
        Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
        Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, Sortie,None
    }
    int[] i_test;
    private enum Command
    {
        Z,X,C,None
    }

    Transform Trans;
    int NowCursor = 0;
    void Start()
    {
        Trans = GetComponent<Transform>();
        //i_test;
    }

    void Update()
    {
        Check();
        if(PushKey(KeyCode.LeftArrow)){
            NowCursor -= 1;
        }
        if(PushKey(KeyCode.RightArrow)){
            NowCursor += 1;
        }
        if(PushKey(KeyCode.DownArrow)){
            NowCursor += 4;
        }
        if(PushKey(KeyCode.UpArrow)){
            NowCursor -= 4;
        }

        if(NowCursor>=CONSTELLATION_MAX){
            NowCursor -= CONSTELLATION_MAX;
        }
        if(NowCursor<0){
            NowCursor += CONSTELLATION_MAX;
        }



        //数字に合うEnumの名前を持ってくる
        //その名前の場所に移動させる
        string s_now_num = Enum.GetName(typeof(ConstellationState), NowCursor); 
        GameObject g_test = GameObject.Find(s_now_num);
        Trans.position = g_test.GetComponent<Transform>().position;

        Pos();

        if (PushKey(KeyCode.Return)){
            SetSprite();
        }
    }

    public float Wight = 0;
    public float Height = 0;
    private void Pos()
    {
        if (NowCursor == 12)
        {
            Trans.localScale = new Vector3(Wight, Height, 0);
        }
    }


    void Scene()
    {
        for (int i_Cnt = 0; i_Cnt < 3; i_Cnt++)
        {

            string st_now_command = Enum.GetName(typeof(Command), i_Cnt);
            GameObject go_Command = Find(st_now_command);
            Sprite sprite_command = GetSprite(go_Command);
            if (sprite_command == Sprite_None) return;
        }
        //ZXC三つとも何かが入っている

    }

    //空のスプライト
    private Sprite Sprite_None;
    private void SetSprite()
    {
        if (!Search()) return;
        for (int i_Cnt = 0; i_Cnt < 3; i_Cnt++){
            string st_now_command = Enum.GetName(typeof(Command), i_Cnt);
            GameObject go_Command = Find(st_now_command);
            SpriteRenderer sprite_ren = go_Command.GetComponent<SpriteRenderer>();
            Sprite sprite_command = GetSprite(go_Command);
            if (sprite_command != Sprite_None) continue;
            //追加
            sprite_ren.sprite = Constellation[NowCursor];
            AnimeChange(sprite_ren.sprite);
            StandChange(sprite_ren.sprite);
            return;
        }


    }

    void StandChange(Sprite _sprite)
    {
        Sprite s_now_sprite = _sprite;

        GameObject g_stand = GameObject.Find("Stand");

        string s_for_work = s_now_sprite.ToString();
        s_for_work = s_for_work.Substring(14, 1);
        g_stand.GetComponent<Anime>().Change(int.Parse(s_for_work));
    }

    private void AnimeChange(Sprite _sprite)
    {
        Sprite s_now_sprite = _sprite;
        string s_for_work = s_now_sprite.ToString();
        //数値の切り取り
        s_for_work = s_for_work.Substring(14, 1);

        GameObject create_obj = GameObject.Find("Anime");
        create_obj.GetComponent<SpriteChange>().SharingState(int.Parse(s_for_work));
        create_obj.GetComponent<Animator>().Play("Move");
    }

    private bool Search()
    {
        for(int i=0;i<3;i++){
            //ZXCを探す
            string s_now_num = Enum.GetName(typeof(Command), i);
            GameObject g_test = GameObject.Find(s_now_num);
            if (GetSprite(g_test) != Constellation[NowCursor]) continue;
            //かぶっている画像を無しに
            g_test.GetComponent<SpriteRenderer>().sprite = Sprite_None;
            return false;
        }
        return true;
    }

    void Check()
    {
        for (int i = 0; i < 3; i++)
        {
            //ZXCを探す
            string s_now_num = Enum.GetName(typeof(Command), i);
            GameObject obj = Find(s_now_num);
            if (GetSprite(obj) != Sprite_None) continue;
            CONSTELLATION_MAX = 12;
            return;
        }
        CONSTELLATION_MAX = 13;
        NowCursor = 12;
    }

    GameObject Find(string _name)
    {
        return GameObject.Find(_name);
    }

    Sprite GetSprite(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private bool PushKey(KeyCode _key)
    {
        if (Input.GetKeyDown(_key)) {
            return true;
        }
        return false;
    }

    

}
