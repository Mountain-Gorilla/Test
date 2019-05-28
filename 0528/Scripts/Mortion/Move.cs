using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    
    private float       f_Move = 0.0f;      // 移動量

    // デバック用
    public float GetMove() { return f_Move; }

    void Start()
    {
        f_Move = 0.0f;
    }

    /*========================*/
    // 移動量の初期化
    /*========================*/
    public void Reset() { f_Move = 0.0f; }


    /*====================================================================*/
    // 加速計算後の移動量(右)
    // 引数   : 現在の移動量に追加する一度の移動量、加速度の最大値、
    // 戻り値 : 計算後の移動量
    /*====================================================================*/
    public float AccelerateRight(float _once,float _max)
    {
        f_Move += _once;
        if (f_Move > _max) f_Move = _max;

        return f_Move;
    }

    /*====================================================================*/
    // 加速計算後の移動量(左)
    // 引数   : 現在の移動量に追加する一度の移動量、加速度の最大値、
    // 戻り値 : 計算後の移動量
    /*====================================================================*/
    public float AccelerateLeft(float _once, float _max)
    {
        f_Move -= _once;
        if (f_Move < -_max) f_Move = -_max;

        return f_Move;
    }

    /*=============================*/
    //  減速計算(右)
    //  引数   : 減速する速度
    //  戻り値 : 計算後の移動量  
    /*=============================*/
    public float DecelerateRight(float _once)
    {
        f_Move -= _once;
        if (f_Move <= 0.0f) f_Move = 0.0f;

        return f_Move;
    }

    /*=============================*/
    //  減速計算(左)
    //  引数   : 減速する速度
    //  戻り値 : 計算後の移動量
    /*=============================*/
    public float DecelerateLeft(float _once)
    {
        f_Move += _once;
        if (f_Move >= 0.0f) f_Move = 0.0f;

        return f_Move;
    }

    /*============*/
    // デバック用
    /*============*/
    public void ValueDraw()
    {
        Debug.Log(f_Move);
    }

}
