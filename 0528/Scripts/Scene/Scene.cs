using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scene : MonoBehaviour
{
    GameObject g_Player;
    private Move g_Move;       // 移動

    GameObject g_TopBar;
    GameObject g_BottomBar;
    GameObject g_HpBar;
    GameObject g_Boss;

    GameObject g_Cam;
    void Start()
    {
        g_Player = GameObject.Find("Player");
        g_Move = new Move();

        g_Cam = GameObject.Find("Main Camera");

        g_TopBar = GameObject.Find("AnimeBarUp");
        g_BottomBar = GameObject.Find("AnimeBarDown");

        g_HpBar = GameObject.Find("HpBar");
        g_Boss = GameObject.Find("Boss");

    }

    private const float cf_MaxSpeed = 0.15f;       // 最大値
    private const float cf_AccelOnce = 0.15f;       // 一度の加速量(未使用)
    [SerializeField] private int CNT_MAX = 110;
    [SerializeField] private float BAR_MOVE = 0.015f;
    [SerializeField] private Vector3 Second_Pos;
    private float f_Dis = 0.0f;
    private int i_Cnt = 0;
    private bool b_Start = false;

    void Update()
    {
        if (!b_Start) return;

        if (i_Cnt > CNT_MAX)
        {
            g_Boss.GetComponent<Pop>().PopAnime();
            g_Player.GetComponent<Animator>().Play("Stay");
            GameObject g_wall = GameObject.Find("BossLeft");
            g_wall.GetComponent<Rigidbody2D>().simulated = true;

            GameObject g_Rightwall = GameObject.Find("BossRight");
            g_Rightwall.GetComponent<Rigidbody2D>().simulated = true;

            return;
        }
        else
        {
            g_Player.transform.Translate(g_Move.AccelerateRight(cf_AccelOnce, cf_MaxSpeed), 0.0f, 0.0f);
            Vector3 v_PlayerPos = g_Player.transform.position;
            
            g_Cam.transform.position = new Vector3(v_PlayerPos.x + f_Dis, v_PlayerPos.y + 3.0f, g_Cam.transform.position.z);

            g_TopBar.transform.position -= new Vector3(0.0f, BAR_MOVE, 0.0f);
            g_BottomBar.transform.position += new Vector3(0.0f, BAR_MOVE, 0.0f);

            i_Cnt++;
            f_Dis += 0.05f;
        }


    }

    //Boss登場シーン終わり
    public void End()
    {
        g_Player.GetComponent<Player>().enabled = true;
		g_HpBar.SetActive(true);
        g_BottomBar.GetComponent<AnimeBar>().Invisible();
        g_TopBar.GetComponent<AnimeBar>().Invisible();
        b_Start = false;

        this.gameObject.AddComponent<Scene>();
        transform.position = Second_Pos;

        Destroy(this);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Playerと接触した時
        if (col.tag == "Player")
        {
            g_Cam.GetComponent<Camera>().enabled = false;
			g_HpBar.SetActive(false);
            g_Player.GetComponent<Animator>().Play("Move");
            g_Player.GetComponent<Player>().enabled = false;
            b_Start = true;
        }
    }
    
}
