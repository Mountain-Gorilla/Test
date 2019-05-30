﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HueController : MonoBehaviour
{
    // マテリアル操作用のユニークID　文字列を渡すより処理が早くなります
    public int propID;
    // スプライトの明るさ　0.5が真ん中、大きくなるほど明るくなります
    public float brightness = 0.5f;

	bool b_Up;

	// Start is called before the first frame update
	void Start()
    {
        // ユニークID取得
        this.propID = Shader.PropertyToID("_Brightness");
		b_Up = true;
    }

    // Update is called once per frame
    void Update()
    {
		float once = 0.01f;

		if (b_Up)
		{
			brightness = 1;
			//if (brightness >= 1.0f) b_Up = false;
			b_Up = false;
		}

		else
		{
			brightness -= once;
			//if (brightness <= 0.5f) b_Up = true;
		}

		// 確認用　左右キーで切り替えます
		//if (Input.GetKey(KeyCode.RightArrow))
  //      {
  //          brightness += 0.01f;
  //      }
  //      if(Input.GetKey(KeyCode.LeftArrow))
  //      {
  //          brightness -= 0.01f;
  //      }
        if (brightness > 1.0f) brightness = 1.0f;
        if (brightness < 0.0f) brightness = 0.0f;

        // マテリアルにパラメータをセットします
        Renderer r = GetComponent<Renderer>();
        r.material.SetFloat(propID, brightness);

    }
}
