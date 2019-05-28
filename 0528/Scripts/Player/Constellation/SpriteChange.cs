using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteChange : MonoBehaviour
{
	// 12宮の能力発動を管理
	private enum ConstellationState
	{
		Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra,
		Scorpio, Sagittarius, Capricorn, Aquarius, Pisces, Base
	}
	private int n_ConstellationState;    // 管理用変数

	private static int n_idMainTex = Shader.PropertyToID("_MainTex");  // テクスチャのID

	// 画像の変更用変数
	private SpriteRenderer        s_Render;     
	private MaterialPropertyBlock mpb_Block;

	// 主人公の画像
	[SerializeField]
	List<Texture> tex_Constellation = new List<Texture>();

	// テクスチャの変更用変数
	public Texture overrideTexture
	{
		get { return tex_Constellation[tex_Constellation.Count - 1]; }
		set {
			tex_Constellation[tex_Constellation.Count - 1] = value;
			if (mpb_Block == null) Init();
			
			mpb_Block.SetTexture(n_idMainTex, tex_Constellation[tex_Constellation.Count - 1]);
		}
	}

	/*=============================*/
	// 画像の共有
	/*=============================*/
	public void SharingState(int _state) { n_ConstellationState = _state; }


	// 画像(アイコン)を変更
	public void ChangeIcon()
	{
		overrideTexture = tex_Constellation[n_ConstellationState];
	}

	/*=============================*/
	// 初期化(Startより早く実行)
	/*=============================*/
	void Awake()
	{
		Init();
		n_ConstellationState = tex_Constellation.Count - 1;
		overrideTexture = tex_Constellation[n_ConstellationState];
	}

	/*=============================*/
	// 更新(Updateより遅く実行)
	/*=============================*/
	void LateUpdate()
	{
		s_Render.SetPropertyBlock(mpb_Block);
		overrideTexture = tex_Constellation[n_ConstellationState];
		//Debug.Log(n_ConstellationState);
	}

	/*=============================*/
	//  初期化をまとめたもの
	/*=============================*/
	void Init()
	{
		mpb_Block = new MaterialPropertyBlock();
		s_Render = GetComponent<SpriteRenderer>();
		s_Render.GetPropertyBlock(mpb_Block);
	}
}