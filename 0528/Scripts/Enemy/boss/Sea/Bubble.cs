using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    Animator Ani_Bubble;
    private void Start()
    {
        Ani_Bubble = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Ani_Bubble.GetCurrentAnimatorStateInfo(0).normalizedTime>1.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
