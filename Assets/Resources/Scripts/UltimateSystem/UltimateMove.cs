using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* melee class: lightning hits sword and applies damage horizontally
 *
 */

public class UltimateMove : MonoBehaviour
{
    private UltimateBar ultBar;

    public LayerMask layerMask;

    private bool ultReady;

    public int ultDamage;

    public event EventHandler FireUltAnim;

    private void Start()
    {
        ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
        ultBar.OnUltFull += ReadyUlt;


    }

    private void ReadyUlt(object sender, EventArgs e)
    {
        ultReady = true;
    }
    
    private void Update()
    {
        // Debug Only
        if (Input.GetKeyDown(KeyCode.F))
        {
            ultBar.AddUlt(100);
        }

        if (ultReady && Input.GetKeyDown(KeyCode.U))
        {
            ultReady = false;
            UseUlt();
        }
    }
    
    private void UseUlt()
    {
        // hitbox in direction of aim
        
        
        // play ult anim
        if(FireUltAnim != null) FireUltAnim(this, EventArgs.Empty);
        
        
        // empty ult bar
        ultBar.SetUlt(0);
    }


}
