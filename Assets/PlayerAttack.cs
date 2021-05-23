using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float x;
    private float y;
    
    // hitbox
    public float radius;
    public float range;
    private Vector2 lastMove;
    public LayerMask enemyLayer;
    
    // debug
    private Color hitboxColor;

    private UltimateBar ultBar;
    public int ultChargeAmt;

    private void Start()
    {
        lastMove = new Vector2(0, 1);
        ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
    }

    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (x != 0 || y != 0)
            lastMove = new Vector2(x, y);
        
        if (Input.GetButtonDown("Fire1"))
        {
            // spawn hitbox
            var center = transform.XandY() + lastMove.normalized * range;
            var hits = Physics2D.OverlapCircleAll(center, radius, enemyLayer);
            foreach (var col in hits)
            {
                Debug.Log(col.gameObject.tag);
                if (col.gameObject.CompareTag("Enemy"))
                {
                    col.gameObject.GetComponent<EnemyManager>().hSystem.Damage(20);
                    ultBar.AddUlt(ultChargeAmt);
                    hitboxColor = Color.green;
                }
            }
            
        }
        
        
    }

    
    private void OnDrawGizmos()
    {
        if (lastMove.x != 0 || lastMove.y != 0)
        {
            var center = transform.XandY();
            center += lastMove.normalized * range;
            Gizmos.color = hitboxColor;
            Gizmos.DrawWireSphere(center, radius);
        }
        hitboxColor = Color.red;
    }
}
