using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    float f_HP;

	public void  SetHP(float _hp) { f_HP = _hp; }
	public float GetHp() { return f_HP; }
	
	public void Increase(float _increase,float _max)
    {
        f_HP += _increase;
        if (f_HP > _max) f_HP = _max;
    }

    public void Decrease(float _decrease)
    {
        f_HP -= _decrease;
        if (f_HP <= 0) f_HP = 0;  
    }
}
