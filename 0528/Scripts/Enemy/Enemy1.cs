using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // 当たり判定する能力
    private GameObject g_Leo;
	//Rigidbody2D right2D;

	// 12宮の能力発動を管理
	private enum ConstellationState
	{
		Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
		Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, Base
	}

	private GameObject g_ActManager;
    private ActiveManager g_Script;

    private bool  b_Flag = false;
    private float f_Damage = 0.0f;


	// ロード時に実行
	void Start ()
    {
        g_Leo = GameObject.Find("Leo");
        g_ActManager = GameObject.Find("ActiveManager");
        g_Script = g_ActManager.GetComponent<ActiveManager>();

        f_Damage = 0.0f;
        b_Flag = false;
	}
	
	// 更新
	void Update ()
    {
        //HitJudge();

        if (!b_Flag) return;

        f_Damage -= 0.1f;
        transform.Translate(f_Damage, 0.0f, 0.0f);

        if (f_Damage < 0.0f) gameObject.SetActive(false);
    }

    /*void HitJudge()
    {
        if (g_Script.Status() != (int)ConstellationState.Leo || b_Flag) return;

        Vector2 v_Enemy = transform.position;
        Vector2 v_Leo = g_Leo.transform.position;
        Vector2 v_Dir = v_Enemy - v_Leo;

        float f_Dir = v_Dir.magnitude;
        float f_RadArrow = 1.0f;
        float f_RadPlayer = 1.0f;

        if (f_Dir < f_RadArrow + f_RadPlayer) {
            f_Damage = 1.0f;
            b_Flag = true;
        }
    }
	*/

    //void OnCollisionEnter2D(Collision2D _collision)
    //{
    //    // レイヤー名を取得
    //    string layerName = LayerMask.LayerToName(_collision.gameObject.layer);

    //    if (layerName == "Leo") {
    //        Destroy(gameObject);
    //    }
    //}
}
