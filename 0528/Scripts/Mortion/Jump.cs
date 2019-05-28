using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    private float f_Power = 0.0f;  // ジャンプ力
    private float f_Max = 0.0f;    // 値の最大値
    private bool  b_Flag = false;  // フラグ

    /*=====================*/
    // ゲッター
    /*=====================*/
    public bool IsFlag() { return b_Flag; }

    /*========================*/
    // 初期化
    /*========================*/

    void Start()
    {
        f_Power = 0.0f;
        f_Max = 0.0f;
        b_Flag = false;
    }

    public void Reset()
    {
        f_Power = 0.0f;
        b_Flag = false;
    }

    /*=============================================================*/
    // ジャンプ力を入れる
    // 引数 : ジャンプ力(値は0未満、そうでなければ強制的に0未満に)
    /*=============================================================*/

    public void Invocation(float _power)
    {
        if (b_Flag) return;
        b_Flag = true;

        f_Power = _power;
        f_Max = -f_Power;
        if (_power <= 0) f_Power *= -1.0f;
    }

    /*=========================================================*/
    // ジャンプ中
    // 引数   : 現在の移動量に追加する一度の移動量
    // 戻り値 : 計算後の移動量
    /*=========================================================*/

    public float InJump(float _once)
    {
        if (!b_Flag) return 0.0f;

        if (_once < 0) _once *= -1.0f;
        f_Power -= _once;

        if (f_Power < f_Max) {
            Reset();
            return 0.0f;
        }

        return f_Power;
    }

    /*============*/
    // デバック用
    /*============*/
    public void ValueDraw()
    {
       // Debug.Log(f_Power);
        Debug.Log(b_Flag);
    }

}
