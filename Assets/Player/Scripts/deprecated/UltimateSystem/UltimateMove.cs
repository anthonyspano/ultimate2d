using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UltimateMove : MonoBehaviour
{
    private UltimateBar ultBar;

    public LayerMask layerMask;

    private bool ultReady;

    //public int ultDamage; // 200

    public event EventHandler FireUltAnim;


    private void Start()
    {
        //ultBar.OnUltFull += ReadyUlt;


    }

    private void ReadyUlt(object sender, EventArgs e)
    {
        ultReady = true;

        // light up icon

    }
    
    private void Update()
    {
        // Debug Only
        if (Input.GetKeyDown(KeyCode.F))
        {
            ultBar.AddUlt(100);
        }

        if (ultReady && PlayerInput.Ultimate())
        {
            ultReady = false;
            UseUlt();
        }
    }
    
    private void UseUlt()
    {

        // play ult anim
        if(FireUltAnim != null) FireUltAnim(this, EventArgs.Empty);
        
        // empty ult bar
        ultBar.SetUlt(0);

        // disable icon


    }


}
