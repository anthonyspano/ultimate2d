using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// animation event class for ultimate
public class TestAnim : MonoBehaviour
{
    private Animator anim;

    private UltimateMove ultMove;

    public float animLength;
    private BoxCollider2D bc;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        bc = GetComponent<BoxCollider2D>();

        ultMove = PlayerManager.player.GetComponent<UltimateMove>();
        ultMove.FireUltAnim += UltAnim;
    }

    private void UltAnim(object sender, EventArgs e)
    {
        StartCoroutine(HitboxLife());
        anim.Play("blast");
    }

    private IEnumerator HitboxLife()
    {
        yield return null;
        bc.enabled = true;
        // ref clip in animator state
        var clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        yield return new WaitForSeconds(clipInfo.Length);
        bc.enabled = false;
    }
    
    
}
