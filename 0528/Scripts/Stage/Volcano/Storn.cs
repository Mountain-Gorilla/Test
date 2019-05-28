using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storn : MonoBehaviour {
    public GameObject arrowPrefab;
    public float span = 0.5f;　　　//石の発生速度
    float delta = 0;　　　　　　　 //時間を数える
    public int coordinate;         //石の落下範囲の始点
    private  int range=0;          //石の落下範囲
 /*=============================================*/
 // 初期化
 /*=============================================*/
    void Start()
    {
        range = 0;
    }
    /*=============================================*/
    // 更新
    /*=============================================*/
    void Update () {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0;
            GameObject go = Instantiate(arrowPrefab) as GameObject;
            int px = Random.Range(coordinate, range=coordinate+80);
            go.transform.position = new Vector3(px, 0, 0);
        }
    }
}
