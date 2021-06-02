using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    private Animator anim;

    private UltimateMove _ultMove;

    public string stateName;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        _ultMove = GameObject.Find("Player").GetComponent<UltimateMove>();
        _ultMove.FireUltAnim += DoAnim;
    }

    private void DoAnim(object sender, EventArgs e)
    {
        anim.Play(stateName);
    }

    public void EndAnim()
    {
        anim.Play("Neutral");
    }
    
}
