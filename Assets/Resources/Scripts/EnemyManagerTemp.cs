﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManagerTemp : MonoBehaviour
{
    public static int enemyLayerMask = 1 << 8;
    public static float fovRange = 20f;
    public static float attackRange = 12f;
    public static int damage = 20;

    private SpriteRenderer sr;

    public static bool Busy;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // player location
        sr.flipX = (PlayerManager.Instance.transform.position.x > transform.position.x) ? true : false;
    }

    private void OnDrawGizmos()
    {
        // sightRange hitbox
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, GuardBT.fovRange);
        
        // attackRange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
