using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState : MonoBehaviour
{
	//プライベート化するか要検討

	public int n_State = 0;                 //状態（0:待機、1:移動、2:攻撃、3:怯み） 

	public bool b_ToPlayer = false;         //プレイヤーのいる方向(左:false右:true)
	public bool b_DamageFlag = false;       //ダメージを受けたか
	public bool b_Alive = true;             //生存フラグ
	public bool b_Find = false;             //プレイヤー発見フラグ
	public bool b_SpecialAttack = false;    //特殊攻撃フラグ
	public float f_Anger = 20.0f;           //怒り値（これを超えたら特殊攻撃？）
	public float f_Hp = 30.0f;              //HP
	bool b_DeadAnime = false;

	bool b_CrabPop = false;
	bool b_GameClear;

	// クリア時
	float f_Timer;

    private Animator an_Motion;

    GameObject g_Player;
    GameObject g_Cam;

	[SerializeField] RuntimeAnimatorController[] BossAnimeList = default;
    [SerializeField] MonoBehaviour[] BossList;
    int i_AnimeNum = 0;

	public bool IsGameClear()
	{
		f_Timer += Time.deltaTime;
		if (f_Timer <= 1.0f) return false;

		return b_GameClear;
	}

    /*========================================*/
    // 初期化
    /*========================================*/
    void Start()
    {
        an_Motion = GetComponent<Animator>();
        g_Cam = GameObject.Find("Main Camera");

        g_Player = GameObject.Find("Player");
		b_CrabPop = false;
		b_GameClear = false;

		f_Timer = 0.0f;
	}

    /*========================================*/
    // 更新
    /*========================================*/
    void Update()
    {
        //プレイヤーのいる向き（左:false右;true）
        Vector2 v_dir = g_Player.transform.position - transform.position;
        if (v_dir.x > 0.0f) b_ToPlayer = true;  //右側
        if (v_dir.x < 0.0f) b_ToPlayer = false; //左側

        if (Input.GetKeyDown(KeyCode.P)) f_Hp = 0;

        if (g_Cam.transform.position.x - 20.0f > transform.position.x&&f_Hp<=0.0f)
        {
            i_AnimeNum++;
            an_Motion.enabled = true;
            //かにに変更
            //an_Motion.runtimeAnimatorController = (RuntimeAnimatorController)RuntimeAnimatorController.Instantiate(Resources.Load("Sprite/Enemy/Sea/Boss/Animation/Boss"));
            an_Motion.runtimeAnimatorController = BossAnimeList[i_AnimeNum];
            //Destroy(this.GetComponent<ForestBoss>());
            an_Motion.Play("Waiting");
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<CircleCollider2D>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
			GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.5f, 0.5f, 1.0f);
			transform.localScale = new Vector3(1.5f, 1.5f, 1.0f);
            f_Hp = 30;//仮かに

            this.gameObject.AddComponent<Pop>();

			b_CrabPop = true;

            GameObject g_Mane = GameObject.Find("Mana");
            transform.position = new Vector3(g_Mane.transform.position.x + 25.0f, g_Mane.transform.position.y, 0);
        }

        //
        if (f_Hp <= 0 && !b_DeadAnime){
            if (GetComponent<ForestBoss>().enabled == false) return;
            GetComponent<ForestBoss>().enabled = false;
            an_Motion.Play("Dead");
            b_DeadAnime = true;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

			if (b_CrabPop) b_GameClear = true;

			return;
        }

        if(b_DeadAnime && an_Motion.GetCurrentAnimatorStateInfo(0).normalizedTime > 10.0f/11.0f){
            an_Motion.enabled = false;
            GameObject g_right_wall = GameObject.Find("BossRight");
            g_right_wall.GetComponent<Rigidbody2D>().simulated = false;
            GameObject g_wall = GameObject.Find("BossLeft");
            g_wall.GetComponent<Rigidbody2D>().simulated = false;

            b_DeadAnime = false;
            g_Cam.GetComponent<Camera>().enabled = true;
        }

    }


    public float GetHp() { return f_Hp; }
}
