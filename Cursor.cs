using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Cursor : MonoBehaviour
{
    private enum ConstellationState
    {
        Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
        Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, None
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

        if(NowCursor>=12)
        {
            NowCursor -= 12;
        }
        if(NowCursor<0)
        {
            NowCursor += 12;
        }

        Debug.Log(NowCursor);
        Debug.Log(Enum.GetName(typeof(ConstellationState),NowCursor));
        string s_now_num = Enum.GetName(typeof(ConstellationState), NowCursor);

        GameObject g_test = GameObject.Find(s_now_num);
        Trans.position = g_test.GetComponent<Transform>().position;

        if (PushKey(KeyCode.KeypadEnter)){
            GameObject Z = GameObject.Find("Z");
            Z.GetComponent<SpriteRenderer>().sprite = g_test.GetComponent<SpriteRenderer>().sprite;
        }
    }

    private bool PushKey(KeyCode _key)
    {
        if (Input.GetKeyDown(_key)) {
            return true;
        }
        return false;
    }

}
