using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private GameObject      g_Parent;
    private Rigidbody2D     ri_Physic;
    private EnemyState      es_State;                   //ステータス

    private float           f_Blow;                     //吹き飛び値
    private float           f_StanProg;                 //スタン経過時間


	// ロード時に実行
	void Start ()
    {
        //親オブジェクトのステータス取得
        g_Parent = transform.root.gameObject;
        ri_Physic = g_Parent.GetComponent<Rigidbody2D>();
        es_State = g_Parent.GetComponent<EnemyState>();

        f_Blow = 0.0f;
        f_StanProg = 0.0f;
	}
	
	// 更新
	void Update ()
    {

        if (es_State.n_State != 3)return;

        f_StanProg += Time.deltaTime;

        //吹き飛び値一定以下で怯み終了
        if (f_StanProg > es_State.f_StanSpan && es_State.b_OnGround == true){

            //スタン経過リセット
            f_StanProg = 0.0f;

            //ステートを待機状態に
            es_State.n_State = 0;

            //ヒットボックス判定をON
            this.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D _collider)
    {

        //プレイヤーの通常攻撃
        if (_collider.gameObject.tag == "PlayerAttack"){
            //吹き飛び値設定
            f_Blow = es_State.f_Blow;

            //慣性をリセット
            ri_Physic.velocity = Vector2.zero;

            //攻撃を受けた方向に飛ぶ
            int n_dir_blow = es_State.b_ToPlayer ? -1 : 1;
            ri_Physic.AddForce(new Vector2(f_Blow * n_dir_blow, f_Blow),ForceMode2D.Impulse);

            //攻撃を受けた方向に向く
            int n_dir_turn = es_State.b_ToPlayer ? -1 : 1;
            transform.localScale = new Vector3(n_dir_turn * -1.0f, 1.0f, 1);

            //HP減少
            es_State.f_Hp = es_State.f_Hp - 1.0f; ;

            //ステートを怯みに
            es_State.n_State = 3;

            //ヒットボックス判定をOFF
            this.GetComponent<BoxCollider2D>().enabled = false;

        }

        //毒攻撃
        if (_collider.gameObject.tag == "Scorpio"){
            if (es_State.n_Condition == 0){
                //毒状態に
                es_State.n_Condition = 1;
            }
        }

        //魅了攻撃
        if (_collider.gameObject.tag == "Virgo"){
            if (es_State.n_Condition == 0){
                //魅了状態に
                es_State.n_Condition = 2;
            }
        }
    }
 }
