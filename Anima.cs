using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Collections;

public class Anima : MonoBehaviour
{

    // アニメーション
    private Animator an_Mortion;

    /*======================*/
    //  初期化
    /*======================*/
    void Start()
    {
        an_Mortion = GetComponent<Animator>();
        //an_Mortion.Play("Move");

    }
    public void Test()
    {
        an_Mortion.Play("Jump");
/*
        switch (n_MortionState)
        {

            case (int)Mortion.Jump:
                an_Mortion.Play("Jump");
                break;

            case (int)Mortion.Fall:
                an_Mortion.Play("Fall");
                break;

            case (int)Mortion.Move:
                break;

            case (int)Mortion.Stay:
                an_Mortion.Play("Stay");
                break;
        }
        */
    }

}
