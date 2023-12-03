using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugColliderAnim : MonoBehaviour
{
    private Animator anim;

    float LastMoveY;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        var y = Input.GetAxis("Vertical");
        if(y > 0.2f)
            LastMoveY =  1;

        if(y < -0.2f)
            LastMoveY =  -1;

        anim.SetFloat("LastMoveY", LastMoveY);
        if(Input.GetKeyDown(KeyCode.K))
        {
            if(anim.GetFloat("LastMoveY") == -1)
                anim.Play("deleteme_attack");

            if(anim.GetFloat("LastMoveY") == 1)
                anim.Play("deleteme_attack_up");
        }
    }
}
