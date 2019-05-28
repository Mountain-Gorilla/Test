using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StornController : MonoBehaviour {
    GameObject player;
    public float rotationalSpeed;
	// Use this for initialization
	void Start () {
        this.player = GameObject.Find("player");
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, -0.5f, 0); //石の一回の落下速度
        rotationalSpeed += 1.0f;　　　
        if (transform.position.y < -100.0f)　//石の落下範囲
        {
            Destroy(gameObject);
        }

        Vector2 p1 = transform.position;
        Vector2 dir = p1;
        float d = dir.magnitude;
        float r1 = 0.5f;
        float r2 = 1.0f;
        if(d<r1 + r2)
        {
            Destroy(gameObject);
        }
	}
}
//Unity授業用の教科書であった屋が落ちてくるゲームの物を参考にしました。