using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAim))]
public class PlayerAttack : MonoBehaviour
{
    // temp
    private UltimateAim ultAim;
    
    // hitbox
    public float radius;
    public LayerMask enemyLayer;
    public float range;
    private Vector2 lastMove;

    // debug
    private Color hitboxColor;

    // ultimate
    private UltimateBar ultBar;
    [Tooltip("This charges the ultimate bar")]
    public int ultChargeAmt;

    // animation
    private Animator anim;
    
    [SerializeField] public float cooldownRate;
    private float cooldownTimer;
    //[SerializeField] public float CooldownTimer { get; set; }
    //[SerializeField] public float CooldownRate { get; set; }

    // Player Aim
    private PlayerAim myAim;

    public int bossDamage;

    private void Start()
    {
        // hitbox
        lastMove = new Vector2(0,1);
        myAim = GetComponent<PlayerAim>();
        ultAim = GetComponent<UltimateAim>();
        //ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>(); // not implemented in scene
        anim = GameObject.Find("StrikeSprite").GetComponent<Animator>();
        cooldownTimer = cooldownRate;
    }

    private void Update()
    {
        // for hitbox
        //if (x != 0 || y != 0) lastMove = new Vector2(x,y);
        //if (CooldownTimer <= 0 && (Input.GetKeyDown(PlayerInput.k_slash)
        //                        || Input.GetKeyDown(PlayerInput.c_slash))) 
        if (PlayerInput.Slash() && IsReady())
        {
            StartCoroutine(Strike("strike", 20));
        }
        

        cooldownTimer -= Time.deltaTime;
    }

    private bool IsReady() => cooldownTimer <= 0;

    // 0.2 seconds will work for now
    public IEnumerator Strike(string stateName, int ultChargeAmt)
    {
        yield return new WaitForSeconds(0.2f);

        // play anim
        anim.Play(stateName);
        
        // hitbox
        //var center = transform.XandY() + lastMove.normalized * range;
        var hits = Physics2D.OverlapCircleAll(myAim.center, radius, enemyLayer);
        foreach (var col in hits)
        {
            // remember to check tags and layer!!
            //Debug.Log(col.gameObject.tag);
            // if (col.gameObject.CompareTag("Enemy"))
            // {
            //     col.gameObject.GetComponent<EnemyManager>().hSystem.Damage(20);
            //     //ultBar.AddUlt(ultChargeAmt);
            //     hitboxColor = Color.green;
            // }

            if (col.gameObject.CompareTag("Boss"))
            {
                col.gameObject.GetComponent<EnemyTakeDamage>().healthSystem.Damage(bossDamage);
            }
        }
        
        // cooldown
        cooldownTimer = cooldownRate;


    }
    
    public void EndAnim()
    {
        anim.Play("Neutral");
    }


    private void OnDrawGizmosSelected()
    {
        //var center = transform.XandY();
        //center += lastMove.normalized * range;
        // Gizmos.color = hitboxColor;
        // Gizmos.DrawWireSphere(myAim.center, radius);
        // hitboxColor = Color.red;
    }


}
