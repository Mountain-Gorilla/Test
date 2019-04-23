using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Anime : MonoBehaviour
{
    private SpriteRenderer NowSprite;
    public Sprite[] Constellation;
    private Sprite None;

    void Awake()
    {
        NowSprite = GetComponent<SpriteRenderer>();
    }

    public void Change(int _Test)
    {
        //Debug.Log(NowSprite);
        NowSprite.sprite = Constellation[_Test];
    }

    public void Erase()
    {
        NowSprite.sprite = None;
    }

    public Sprite GetSprite()
    {
        return NowSprite.sprite;
    }
}
