using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestBoss : MonoBehaviour
{
    private Animator an_Mortion;

	// モーション管理
    private enum Mortion{
        Waiting,Attack1,Attack2,Max
    }
    private int n_Mortion;
	private bool b_AttackMortion = false;      // 攻撃モーションかどうか

	// 攻撃間のインターバル
    private float              f_Timer = 0.0f;
	private const float        cf_Span = 3.0f;


	// ブレス攻撃
	private GameObject g_Breath;
	private Breath g_BreathScript;

	// 突進用
	private Direction g_Direction;

	void Awake()
	{
		g_Breath = GameObject.Find("Breath");
		g_BreathScript = g_Breath.GetComponent<Breath>();
		g_Breath.SetActive(false);
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
        f_Timer += Time.deltaTime;

		AnimationCheck();

		AttackSelection();

        BossState g_Boss = GetComponent<BossState>();
        if(g_Boss.GetHp()<=0.0f)
        {
			//g_Direction.ReSetDirection();
            //Destroy(this);
        }
    }

    void AttackSelection()
    {
		if (f_Timer < cf_Span || b_AttackMortion) return;

		n_Mortion = Random.Range((int)Mortion.Attack1, (int)Mortion.Max);
		//n_Mortion = 2;

		switch (n_Mortion)
		{
			case (int)Mortion.Attack1:
				an_Mortion.Play("Attack1");
				break;

			case (int)Mortion.Attack2:
				an_Mortion.Play("Attack2");
				transform.Translate(g_Direction.IsDirection() * 5.3f, 0.0f, 0.0f);
				gameObject.tag = "EnemyAttack";
				break;
		}

		b_AttackMortion = true;
    }

	void Breath()
	{
		if(an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7 && an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime <= 0.8 && 
			n_Mortion == (int)Mortion.Attack1 && !g_Breath.activeSelf) {

			g_Breath.SetActive(true);
			return;
		}

		if(g_Breath.activeSelf && !g_BreathScript.IsActive()) { g_Breath.SetActive(false); }

		Debug.Log(g_BreathScript.IsActive());
	}

	void AnimationCheck()
	{
		if (!b_AttackMortion) return;

		Breath();

		if (an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime > 1) {
			b_AttackMortion = false;
			//if (n_Mortion == (int)Mortion.Attack1) g_Breath.SetActive(false);

			if (n_Mortion == (int)Mortion.Attack2) {
				transform.Translate(g_Direction.IsDirection() * 5.3f, 0.0f, 0.0f);
				transform.localScale = new Vector3(g_Direction.IsDirection() * 1.5f, 1.5f, 1.0f);
				g_Direction.ChangeDirection();
				gameObject.tag = "Enemy";
			}
			an_Mortion.Play("Waiting");
			n_Mortion = (int)Mortion.Waiting;
			f_Timer = 0;
		}
	}


}
