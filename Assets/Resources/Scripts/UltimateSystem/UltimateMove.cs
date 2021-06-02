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

        if (ultReady && Input.GetKeyDown(KeyCode.Mouse2))
        {
            ultReady = false;
            UseUlt();
        }
    }
    
    private void UseUlt()
    {
        // play ult anim
        if(FireUltAnim != null) FireUltAnim(this, EventArgs.Empty);

        // shoot raycasts to left and right to apply damage
        RaycastHit2D rightUlt = Physics2D.Raycast(transform.position, Vector2.right, 20f, layerMask);
        RaycastHit2D leftUlt = Physics2D.Raycast(transform.position, Vector2.left, 20f, layerMask);
        
        // damage enemy hit
        if (leftUlt.collider) 
            leftUlt.collider.gameObject.GetComponent<EnemyManager>().hBar.healthSystem.Damage(ultDamage);
        if (rightUlt.collider)    
            rightUlt.collider.gameObject.GetComponent<EnemyManager>().hBar.healthSystem.Damage(ultDamage);
        
        // empty ult bar
        ultBar.SetUlt(0);
    }


}
