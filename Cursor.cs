using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cursor : MonoBehaviour
{
    public Sprite[] Constellation;

    private enum ConstellationState
    {
        Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
        Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, None
    }

    private enum Command
    {
        Z,X,C,None
    }

    Transform Trans;
    int NowCursor = 0;
    void Start()
    {
        Trans = GetComponent<Transform>();
    }

    void Update()
    {
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

        if(NowCursor>=12){
            NowCursor -= 12;
        }
        if(NowCursor<0){
            NowCursor += 12;
        }

        //数字に合うEnumの名前を持ってくる
        //その名前の場所に移動させる
        string s_now_num = Enum.GetName(typeof(ConstellationState), NowCursor); 
        GameObject g_test = GameObject.Find(s_now_num);
        Trans.position = g_test.GetComponent<Transform>().position;


        if (PushKey(KeyCode.Return)){
            SetSprite();
        }
    }

    private Sprite Sprite_None;


    private void SetSprite()
    {
        if (!Search()) return;
        for (int i_Cnt = 0; i_Cnt < 3; i_Cnt++){

            string st_now_command = Enum.GetName(typeof(Command), i_Cnt);
            GameObject go_Command = GameObject.Find(st_now_command);
            SpriteRenderer sprite_ren = go_Command.GetComponent<SpriteRenderer>();

            if (sprite_ren.sprite != Sprite_None) continue;
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
            Sprite spr_now = g_test.GetComponent<SpriteRenderer>().sprite;
            if (spr_now != Constellation[NowCursor]) continue;
            //画像を無しに
            g_test.GetComponent<SpriteRenderer>().sprite = Sprite_None;
            return false;
        }
        return true;
    }
    


    private bool PushKey(KeyCode _key)
    {
        if (Input.GetKeyDown(_key)) {
            return true;
        }
        return false;
    }

}
