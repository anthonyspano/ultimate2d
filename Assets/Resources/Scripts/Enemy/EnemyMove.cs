using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMove : MonoBehaviour
{
    private Animator anim;
    [HideInInspector] public GameObject player;

    public float sightRange;
    public float atkRange;
    public float runSpeed;

    private bool once;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = PlayerManager.player;
    }

    void Update()
    {
        var playerPos = player.transform.position;
        var distX = Math.Abs(transform.position.x - playerPos.x);
        var distY = Math.Abs(transform.position.y - playerPos.y);
        
        anim.SetBool("inSight", inSight(distX,distY));
        anim.SetBool("inRange", inRange(distX,distY));

        // sight range 
        if (inSight(distX, distY) && !inRange(distX, distY))
        {  
            anim.SetBool("inSight", true);
            // probably move towards playerpos but set value to distance traveled in one frame
            // towards: (playerPos - transform.position).normalize
            var tPos = transform.position + ((playerPos - transform.position).normalized);
            transform.position = Vector2.MoveTowards(transform.position, tPos, runSpeed * Time.deltaTime);
        }

        
        // attack range
        if (inRange(distX, distY) && once)
        {
            once = false;
            anim.SetBool("inRange", true); // begins the "mino_atk1" animation
        }
    }
    private bool inSight(float dx, float dy)
    {
        return (dx < sightRange && dy < sightRange);
    }

    private bool inRange(float dx, float dy)
    {
        return (dx < atkRange && dy < atkRange);
    }

    private void OnDrawGizmos()
    {
        // atkRange hitbox
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, atkRange);

        // sightRange hitbox
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
