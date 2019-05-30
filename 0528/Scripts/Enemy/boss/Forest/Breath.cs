using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
	private GameObject g_Boss;
	// 突進用
	private Direction g_Direction;

	private GameObject g_Player;
    private Vector3    v_Direct;

    public  float      f_Speed=0.3f;      //スピード

	private bool b_ActiveFlag;
	private int  n_IsReflection;

    [SerializeField]
    private float PopPosY;

    public bool IsActive() { return b_ActiveFlag; }

	void Awake()
	{
		g_Boss = GameObject.Find("Boss");
		g_Direction = g_Boss.GetComponent<Direction>();
        if (g_Direction.IsDirection() == 1) transform.localScale=new Vector3(-1.0f * transform.lossyScale.x, transform.lossyScale.y, transform.lossyScale.z);
        g_Player = GameObject.Find("Player");

		b_ActiveFlag = true;

		OnEnable();
	}

   void OnEnable()
	{
        Vector3 playerpos = g_Player.transform.position;
        Vector3 bosspos = g_Boss.transform.position;
        Vector3 breathpos = new Vector3(bosspos.x, bosspos.y + PopPosY, 0);
        v_Direct = playerpos - breathpos;
        Debug.Log(v_Direct);
        v_Direct = v_Direct.normalized;
        Debug.Log(v_Direct);


        transform.position = breathpos;

		b_ActiveFlag = true;
		n_IsReflection = 1;

    }

	void OnDisable()
	{
		b_ActiveFlag = false;
	}

	// Update is called once per frame
	void Update()
    {
        float MoveX = n_IsReflection * -g_Direction.IsDirection() * v_Direct.x * f_Speed;
        if (g_Direction.IsDirection() == 1) MoveX *= -1;
        transform.Translate(MoveX, n_IsReflection * v_Direct.y * f_Speed, 0.0f); 
    }

    private void LateUpdate()
    {
        if (HitPlayer)
        {
            Destroy(this.gameObject);
        }
    }

    bool HitPlayer = false;
	void OnTriggerEnter2D(Collider2D _collider)
	{

		if (_collider.gameObject.tag == "Reflection")
		{
			n_IsReflection = -1;
		}

        if (_collider.gameObject.tag == "Player")
        {
            HitPlayer = true;
        }

    }

    void OnTriggerExit2D(Collider2D _collider)
    {
        if (_collider.gameObject.tag == "TileMap")
        {
            Destroy(this.gameObject);
        }

    }
}
