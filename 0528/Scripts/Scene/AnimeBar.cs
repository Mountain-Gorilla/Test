using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimeBar : MonoBehaviour
{
    void Start()
    {
        
    }

    bool b_Inv = false;
    float f_Alpha = 1.0f;
    [SerializeField] private float INV_SPEED = 0.01f;

    void Update()
    {
        if (b_Inv == false) return;
        GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, f_Alpha);
        f_Alpha -= INV_SPEED;
    }

    public void Invisible()
    {
        b_Inv = true;
    }
}
