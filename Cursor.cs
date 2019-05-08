using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Cursor : MonoBehaviour
{

    public　Sprite[]    Constellation;

    Vector3             RIGHT       = new Vector3(0.5f, 0, 0);
    Vector3             LEFT        = new Vector3(-0.5f, 0, 0);

    Vector3             MAX_SCALE   = new Vector3(1.5f, 1.5f, 1.0f);
    Vector3             MIN_SCALE   = new Vector3(1.0f, 1.0f, 1.0f);

    //空のスプライト
    Sprite              Sprite_None = null;

    Transform           Trans;
    GameObject          g_Other;                //Cursorの左側

    int                 NowCursor           = 0;
    int                 CONSTELLATION_MAX   = 12;
    const int           BUTTON_HOLDTIME = 40;
    public static int[] Ability = new int[3];
    /*========================================*/
    //プレイヤーに渡す選択情報
    //引数     :ほしい配列の番号
    //戻り値   :能力にあった数値
    /*========================================*/
    public static int GetAbility(int _num){ return Ability[_num]; }

    private enum ConstellationState
    {
        Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
        Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, Sortie,None
    }

    private enum Command
    {
        Z,X,C,None
    }

    void Start()
    {
        Trans = GetComponent<Transform>();
        g_Other = Find("Other");
    }

    void Update()
    {
        Check();      

        Move();

        if (PushKey(KeyCode.Return)|| PushKey(KeyCode.Space)){
            SetSprite();
        }

        for(int i_cnt=0; i_cnt < 3; i_cnt++) {
            SetAbility(i_cnt);

        }
    }

    void HoldDecision()
    {
        if(!Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow)&&
           !Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            b_PushButton = false;
            i_HoldTime = 0;
        }
            
    }

    private void Move()
    {
        HoldDecision();

        if (PushKey(KeyCode.LeftArrow))
        {
            if (NowCursor % 4 == 0 && CONSTELLATION_MAX==13)
            {
                NowCursor = 12;
            }
            else
            {
                NowCursor -= 1;
            }
        }
        if (PushKey(KeyCode.RightArrow))
        {
            NowCursor += 1;
        }
        if (PushKey(KeyCode.DownArrow))
        {

            NowCursor += 4;
        }
        if (PushKey(KeyCode.UpArrow))
        {
            NowCursor -= 4;
        }


        if (NowCursor >= CONSTELLATION_MAX)
        {
            NowCursor -= CONSTELLATION_MAX;
        }
        if (NowCursor < 0)
        {
            NowCursor += CONSTELLATION_MAX;
        }



        //数字に合うEnumの名前を持ってくる
        //その名前の場所に移動させる
        string s_ability = Enum.GetName(typeof(ConstellationState), NowCursor);
        GameObject g_ability = GameObject.Find(s_ability);
        Vector3 v_icon = g_ability.GetComponent<Transform>().position;

        if(NowCursor==12)
        {
            Trans.position = v_icon + RIGHT * 4.0f;
            g_Other.transform.position = v_icon += LEFT * 4.0f;

            Trans.localScale = MAX_SCALE;
            g_Other.transform.localScale = MAX_SCALE;
        }
        else
        {
            Trans.position = v_icon + RIGHT;
            g_Other.transform.position = v_icon += LEFT;

            Trans.localScale = MIN_SCALE;
            g_Other.transform.localScale = MIN_SCALE;

        }

    }

    void SetAbility(int _command)
    {
        if (NowCursor == CONSTELLATION_MAX - 1) return;


        string st_ability = Enum.GetName(typeof(Command), _command);

        if (Input.GetKeyDown(st_ability.ToLower()))
        {
            Cancel();
            GameObject g_ability = Find(st_ability);
            SpriteRenderer sr_command = GetSpriteRender(g_ability);
            sr_command.sprite = Constellation[NowCursor];
            AnimeChange();
            StandChange();
            Ability[_command] = NowCursor;

        }
    }

    bool Scene()
    {
        if (NowCursor != 12) return false;

        for (int i_Cnt = 0; i_Cnt < 3; i_Cnt++)
        {
            string st_ability = Enum.GetName(typeof(Command), i_Cnt);
            GameObject g_ability = Find(st_ability);
            SpriteRenderer sr_ability = GetSpriteRender(g_ability);
            if (sr_ability.sprite == Sprite_None) return false;
        }
        //このオブジェクトのアクティブを切る
        gameObject.SetActive(false);

        //ZXC全部はいている
        SceneManager.LoadScene("ForestScene");
        return true;
    }

    private void SetSprite()
    {
        if (Scene()) return;
        if (Cancel()) return;

        for (int i_Cnt = 0; i_Cnt < 3; i_Cnt++){
            //ZXC
            string st_command = Enum.GetName(typeof(Command), i_Cnt);
            GameObject g_command = Find(st_command);
            SpriteRenderer sr_command = GetSpriteRender(g_command);
            //ZXCのどれかに何も入っていない時
            if (sr_command.sprite != Sprite_None) continue;
            
            //追加
            sr_command.sprite = Constellation[NowCursor];
            AnimeChange();
            StandChange();
            Ability[i_Cnt] = NowCursor;
            return;
        }


    }

    void StandChange()
    {
        GameObject g_stand = GameObject.Find("Stand");
        g_stand.GetComponent<Anime>().Change(NowCursor);
    }

    void AnimeChange()
    {
        GameObject create_obj = GameObject.Find("Anime");
        create_obj.GetComponent<SpriteChange>().SharingState(NowCursor);
        create_obj.GetComponent<Animator>().Play("Move");
    }

    bool Cancel()
    {
        for(int i=0;i<3;i++){
            //ZXCを探す
            string s_now_num = Enum.GetName(typeof(Command), i);
            GameObject g_now_obj = GameObject.Find(s_now_num);
            if (GetSpriteRender(g_now_obj).sprite != Constellation[NowCursor]) continue;
            //かぶっている画像を無しに
            g_now_obj.GetComponent<SpriteRenderer>().sprite = Sprite_None;
            b_Check = false;
            return true;
        }
        return false;
    }

    bool b_Check = false;
    void Check()
    {
        for (int i = 0; i < 3; i++){
            //ZXCを探す
            string s_ability = Enum.GetName(typeof(Command), i);
            GameObject g_ability = Find(s_ability);
            if (GetSpriteRender(g_ability).sprite != Sprite_None) continue;
            CONSTELLATION_MAX = 12;
            return;
        }

        CONSTELLATION_MAX = 13;
        if (!b_Check){
            NowCursor = 12;
            b_Check = true;
        }
    }

    GameObject Find(string _name){ return GameObject.Find(_name); }

    SpriteRenderer GetSpriteRender(GameObject gameObject)
    {
        return gameObject.GetComponent<SpriteRenderer>();
    }

    bool    b_PushButton=false;
    int     i_HoldTime = 0;
    KeyCode Key_HoleCode;
    private bool PushKey(KeyCode _key)
    {
        if (Input.GetKeyDown(_key)) {
            b_PushButton = true;
            Key_HoleCode = _key;
            return true;
        }
        if (Key_HoleCode != _key) return false;
        if (b_PushButton){
            i_HoldTime++;
        }
        if (b_PushButton && i_HoldTime > BUTTON_HOLDTIME) return true;
        return false;
    }

    

}
