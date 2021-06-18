﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerAim))]
public class PlayerAttack : MonoBehaviour, ICoolDown
{
    // temp
    private UltimateAim ultAim;
    
    // hitbox
    public float radius;
    public LayerMask enemyLayer;

    // debug
    private Color hitboxColor;

    // ultimate
    private UltimateBar ultBar;
    [Tooltip("This charges the ultimate bar")]
    public int ultChargeAmt;

    // animation
    private Animator anim;
    
    // interface
    [Header("Interface")]
    [SerializeField] private float cooldownRate;
    public float CooldownTimer { get; set; }
    public float CooldownRate { get; set; }

    // Player Aim
    private PlayerAim myAim;

    private void Start()
    {
        myAim = GetComponent<PlayerAim>();
        ultAim = GetComponent<UltimateAim>();
        ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
        anim = GameObject.Find("StrikeSprite").GetComponent<Animator>();
        Debug.Log(anim);
        CooldownTimer = 0;
    }

    private void Update()
    {
        if (CooldownTimer <= 0 && Input.GetButtonDown("Fire1"))
        {
            Strike("strike", 20);
        }
        

        CooldownTimer -= Time.deltaTime;
    }

    // pass in anim name based on where called
    // param for ultcharge based on where called
    public void Strike(string stateName, int ultChargeAmt)
    {
        // cooldown
        CooldownTimer = cooldownRate;
        
        // play anim
        anim.Play("strike");
        
        // spawn hitbox
        var hits = Physics2D.OverlapCircleAll(myAim.center, radius, enemyLayer);
        foreach (var col in hits)
        {
            if (col.gameObject.CompareTag("Enemy"))
            {
                col.gameObject.GetComponent<EnemyManager>().hSystem.Damage(20);
                ultBar.AddUlt(ultChargeAmt);
                hitboxColor = Color.green;
            }
        }


    }
    
    public void EndAnim()
    {
        anim.Play("Neutral");
    }

    // temporarily for ultimate aim
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = hitboxColor;
        Gizmos.DrawWireSphere(myAim.center, radius);
        hitboxColor = Color.red;
    }
}
