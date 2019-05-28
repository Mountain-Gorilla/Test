using UnityEngine;
using UnityEngine.UI;
// =================================
// 画面に赤みがかかるエフェクト(被弾時用)
// スプライトに赤色テクスチャを設定して使ってください
// =================================
public class FlushController : MonoBehaviour
{
	// カメラ情報
	GameObject g_Camera;

	// スプライト操作
	SpriteRenderer g_sr;
	// ※プレイヤー情報を取得してください※

	private bool b_HitFlag = false;
	
	public void OnFlag() { b_HitFlag = true; }
	public void DestoryFlag() { b_HitFlag = false; }

    void Start()
    {
		g_Camera = GameObject.Find("Main Camera");

		g_sr = GetComponent<SpriteRenderer>();
        g_sr.color = Color.clear;

		b_HitFlag = false;
    }

    void Update()
    {
        // ※取得したプレイヤー情報から被弾しているかどうか判定してください※
        // 暫定でクリック時の判定にしています
        if (b_HitFlag) 
        {
            // 被弾時赤くします　第1,4引数で赤みを変更できます
            g_sr.color = new Color(0.5f, 0f, 0f, 0.5f);
            // ※被弾判定を切ってください※

        }
        else
        {
            // 透明と赤色を線形補間で透明に近づけていきます
            g_sr.color = Color.Lerp(this.g_sr.GetComponent<SpriteRenderer>().color, Color.clear, Time.deltaTime);
        }

		Vector3 camera = g_Camera.transform.position;
		transform.position = new Vector3(camera.x, camera.y, 0.0f);
	}
}