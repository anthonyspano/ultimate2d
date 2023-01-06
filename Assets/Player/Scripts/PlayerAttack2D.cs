using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack2D : MonoBehaviour
{
    private Animator anim;
    public float cooldownRate;
    private float cooldownTimer;

    public float atkRadius;
    public LayerMask enemyLayer;
    
    void Start()
    {
        anim = GameObject.Find("StrikeSprite").GetComponent<Animator>();
        cooldownTimer = cooldownRate;
    }
    
    void Update()
    {
        if (PlayerInput.Slash() && IsReady())
        {
            StartCoroutine(Strike("strike"));
        }

        cooldownTimer -= Time.deltaTime;
    }
    
    public IEnumerator Strike(string stateName)
    {
        // cooldown
        cooldownTimer = cooldownRate;
        
        yield return new WaitForSeconds(0.2f);

        // play anim
        anim.Play(stateName);
        
        // hitbox
        //var center = transform.XandY() + lastMove.normalized * range;
        var hits = Physics2D.OverlapCircleAll(transform.position + Vector3.right, atkRadius, enemyLayer);
        foreach (var col in hits)
        {
            // remember to check tags and layer!!
            //Debug.Log(col.gameObject.tag);
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<HealthBar>().healthSystem.Damage(20);
            }
            
        }
        



    }
    
    
    
    private bool IsReady() => cooldownTimer <= 0;
}
