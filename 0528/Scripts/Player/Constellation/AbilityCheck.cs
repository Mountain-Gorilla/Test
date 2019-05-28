using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCheck : MonoBehaviour
{
	private const int cn_NoAbility = 12;
	public int   n_ConAbilityNum;
	private bool b_GetAbility;

	private GameObject g_IconDirector;
	private IconDirector id_IconChange;

	private GameObject g_Player;
	private Player g_PlayerScript;

	// 自身のコンポーネントを参照
	private Rigidbody2D rb_Body;

	[SerializeField]
	private Collider2D col_CircleTrigger;

	[SerializeField]
	private GameObject g_BoxCollider;
	private IconGroundJudge igj_Script;

	private SpriteRenderer g_sr;     // スプライト操作

	// UIオブジェクトへの参照
	[SerializeField]
	GameObject g_IconUI;

	NowAbility na_IconUI;

	// 能力が使えるかどうか
	public int IsUseAbility()
	{
		if (!b_GetAbility) return cn_NoAbility;
		return n_ConAbilityNum;
	}

	// 能力解除
	public void AbilityRelease()
	{
		rb_Body.bodyType = RigidbodyType2D.Dynamic;
		col_CircleTrigger.isTrigger = false;

		igj_Script.DestroyFlag();

		transform.position = g_Player.transform.position;
		rb_Body.AddForce(new Vector2(10.0f * -g_PlayerScript.IsDirection(), 10.0f), ForceMode2D.Impulse);

		b_GetAbility = false;
		g_sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}

    // Start is called before the first frame update
    void Start()
    {
		g_IconDirector = GameObject.Find("IconDirector");
		id_IconChange = g_IconDirector.GetComponent<IconDirector>();

		g_Player = GameObject.Find("Player");
		g_PlayerScript = g_Player.GetComponent<Player>();

		rb_Body = GetComponent<Rigidbody2D>();
		col_CircleTrigger = GetComponent<Collider2D>();
		g_sr = GetComponent<SpriteRenderer>();

		igj_Script = g_BoxCollider.GetComponent<IconGroundJudge>();

		na_IconUI = g_IconUI.GetComponent<NowAbility>();

		b_GetAbility = false;
    }

	void Update()
	{
		if (igj_Script.IsGround()) {
			rb_Body.velocity = new Vector2(0.0f, 0.0f);
			rb_Body.bodyType = RigidbodyType2D.Kinematic;
			igj_Script.DestroyFlag();
		}
	}

	// 能力取得
	void OnTriggerEnter2D(Collider2D _collider)
	{
		if (b_GetAbility) return;

		if(_collider.gameObject.tag == "Player") {
			b_GetAbility = true;
			na_IconUI.SharingAbility(n_ConAbilityNum);
			g_sr.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);

		}
	}

	void OnCollisionEnter2D(Collision2D _collision)
	{
		if(_collision.gameObject.tag == "TileMap") {
			rb_Body.velocity = new Vector2(0.0f, 0.0f);
			col_CircleTrigger.isTrigger = true;
		}
	}
}
