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

    public bool isReady;
    [SerializeField] private float cooldownMax;
    private float cooldown;

    // flipping sprite
    Vector3 facingRight;
    Vector3 facingLeft;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = PlayerManager.player;
        // animation/ai control
        SetReady(1);
        cooldown = cooldownMax;

        // flipping sprite
        facingLeft = transform.localScale;
        facingRight = Vector3.Scale(transform.localScale, new Vector3(-1,1,1));
    }

    void Update()
    {
        // managing cooldown timer for isReady
        if(!isReady)
        {
            cooldown -= Time.deltaTime;
            if(cooldown <= 0) 
            {
                SetReady(1);
                cooldown = cooldownMax;
            }
        }

        var playerPos = player.transform.position;
        var distX = Math.Abs(transform.position.x - playerPos.x);
        var distY = Math.Abs(transform.position.y - playerPos.y);
        
        anim.SetBool("inSight", inSight(distX,distY));
        // begins the "mino_atk1" animation automatically if ready and in range
        anim.SetBool("inRange", inRange(distX,distY)); 

        // conditions for moving
        if (inSight(distX, distY) && !inRange(distX, distY) && isReady)
        {  
            // flip minotaur relative to player
            if(transform.position.x < playerPos.x)
                transform.localScale = facingRight; 
            if(transform.position.x > playerPos.x)
               transform.localScale = facingLeft;
            // probably move towards playerpos but set value to distance traveled in one frame
            var tPos = transform.position + ((playerPos - transform.position).normalized);
            transform.position = Vector2.MoveTowards(transform.position, tPos, runSpeed * Time.deltaTime);

        }

    }

    public void SetReady(int b)
    {
        isReady = Convert.ToBoolean(b);
        anim.SetBool("isReady", isReady);
    }

    private bool inSight(float dx, float dy)
    {
        return (dx < sightRange && dy < sightRange);
    }

    private bool inRange(float dx, float dy)
    {
        return (dx < atkRange && dy < atkRange);
    }

    // private void OnDrawGizmos()
    // {
    //     // atkRange hitbox
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(transform.position, atkRange);
    //
    //     // sightRange hitbox
    //     Gizmos.color = Color.yellow;
    //     Gizmos.DrawWireSphere(transform.position, sightRange);
    // }
}
