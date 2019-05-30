using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pop : MonoBehaviour
{
    private Animator an_Mortion;
    private bool b_Pop = false;

    void Start()
    {
        an_Mortion = GetComponent<Animator>();
    }

    int i_Cnt = 0;
    float f_animetime = 0;

    void LateUpdate()
    {
        if (b_Pop == false) return;
        f_animetime = an_Mortion.GetCurrentAnimatorStateInfo(0).normalizedTime;
  
        if (f_animetime > 1.0f && i_Cnt<5)
        {
            an_Mortion.Play("Moth", -1, 0);
            i_Cnt++;
            return;
        }
        //PopAnimeの終わり
        if(f_animetime>1.0f&&i_Cnt>=5)
        {
            an_Mortion.Play("Waiting");
            GetComponent<Pop>().enabled = false;
            b_Pop = false;
            GameObject g_manager = this.gameObject;
            g_manager.GetComponent<Scene>().End();
            this.GetComponent<Pop>().enabled = false;
            //Destroy(this);
            return;
        }

    }


    public void PopAnime()
    {
        if (b_Pop) return;
        an_Mortion.Play("Pop");
        b_Pop = true;
    }
}
