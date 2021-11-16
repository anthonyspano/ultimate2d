using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndAnimation : MonoBehaviour
{
    private Animator anim;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void EndAnim()
    {
        anim.Play("Neutral");
    }
}
