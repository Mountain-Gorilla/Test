using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class IconHalf : MonoBehaviour
{
	// 発動時本体から受け取る情報を格納
	[SerializeField]
	private GameObject g_IconHalf = default;
	private Image      i_Sprite;

	void Start()
	{
		i_Sprite = g_IconHalf.GetComponent<Image>();
	}

	public void SetSprite(Sprite _sprite)
	{
		i_Sprite.sprite = _sprite;
		i_Sprite.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
	}
}
