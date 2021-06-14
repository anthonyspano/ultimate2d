using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    private Animator anim;

    private UltimateMove _ultMove;

    public AnimationClip clip;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        
        _ultMove = PlayerManager.player.GetComponent<UltimateMove>();
        _ultMove.FireUltAnim += DoAnim;
    }

    private void DoAnim(object sender, EventArgs e)
    {
        anim.Play("blast");
    }
    
    
}
