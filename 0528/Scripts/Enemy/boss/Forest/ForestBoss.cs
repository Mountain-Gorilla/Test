﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    private Animator an_Mortion;

    // モーション管理
    private enum Mortion
    {
        Waiting, Attack1, Attack2, Max
    }
    private int n_Mortion;
    private bool b_AttackMortion = false;      // 攻撃モーションかどうか

    // 攻撃間のインターバル
    private float f_Timer = 0.0f;
    private const float cf_Span = 3.0f;


    // ブレス攻撃
    [SerializeField]
    private GameObject g_Breath;


    // 突進用
    private Direction g_Direction;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        an_Mortion = GetComponent<Animator>();
        n_Mortion = (int)Mortion.Waiting;

        b_AttackMortion = false;

        g_Direction = GetComponent<Direction>();
    }

    // Update is called once per frame
    void Update()
    {

        BossState g_Boss = this.GetComponent<BossState>();
        if (g_Boss == null) { return; }
        if (g_Boss.GetHp() == 0) return;
        if (this.GetComponent<Pop>().enabled) return;

        if (HitCount == 1)
        {
            this.tag = "Enemy";
        }

        f_Timer += Time.deltaTime;

        AnimationCheck();

        AttackSelection();

    }

    void AttackSelection()
    {
        if (f_Timer < cf_Span || b_AttackMortion) return;

        n_Mortion = Random.Range((int)Mortion.Attack1, (int)Mortion.Max);
        switch (n_Mortion)
        {
            case (int)Mortion.Attack1:
                an_Mortion.Play("Attack1");
                break;

            case (int)Mortion.Attack2:
                this.tag = "EnemyAttack";
                an_Mortion.Play("Attack2");
                transform.Translate(g_Direction.IsDirection() * 7.2f, 0.0f, 0.0f);
                break;
        }

        HitCount = 0;
        b_AttackMortion = true;
    }

    private bool b_BreathPop = false;
    void Breath()
    {
        if (an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7 && an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.8 &&
            n_Mortion == (int)Mortion.Attack1 && !b_BreathPop)
        {
            b_BreathPop = true;
            GameObject g_NewBreath=Instantiate(g_Breath);
            g_NewBreath.GetComponent<SpriteRenderer>().enabled = true;
            g_NewBreath.GetComponent<Breath>().enabled = true;
            g_NewBreath.GetComponent<CircleCollider2D>().enabled = true;
            return;
        }

    }

    void AnimationCheck()
    {
        if (!b_AttackMortion) return;

        Breath();

        if (an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            b_AttackMortion = false;

            if (n_Mortion == (int)Mortion.Attack2)
            {
                transform.Translate(g_Direction.IsDirection() * 7.1f, 0.0f, 0.0f);
                transform.localScale = new Vector3(g_Direction.IsDirection() * 1.8f, 1.5f, 1.0f);
                g_Direction.ChangeDirection();
            }
            an_Mortion.Play("Waiting");
            n_Mortion = (int)Mortion.Waiting;
            this.tag = "Enemy";
            b_BreathPop = false;
            f_Timer = 0;
        }
    }

    int HitCount = 0;
    void OnTriggerEnter2D(Collider2D col)
    {

        //Playerと接触した時
        if (col.tag == "Player" && this.tag == "EnemyAttack" && HitCount == 0)
        {
            HitCount++;
        }

        if (col.tag == "Reflection")
        {
            this.tag = "Enemy";
        }
    }
}
