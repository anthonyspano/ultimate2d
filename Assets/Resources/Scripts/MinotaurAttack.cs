using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttack : MonoBehaviour
{
    public int assignedPlayerDamage;

    // hitbox
    public float hitboxSize;
    public LayerMask playerLayer;
    private Vector3 hitboxPos;
    

    private void Update()
    {
        hitboxPos = GameObject.Find("AxeHitBox").transform.position; 
        
    }

    private void Attack() 
    {
        // triggered from animation
        var hits = Physics2D.OverlapCircleAll(hitboxPos, hitboxSize, playerLayer);
        foreach (var col in hits)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Player hit!");
                col.gameObject.GetComponent<PlayerManager>().pHealth.Damage(assignedPlayerDamage);
            }
        }

    }


    // private void OnDrawGizmos()
    // {
    //     // axe hitbox
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(hitboxPos, hitboxSize);
    // }
      
}
