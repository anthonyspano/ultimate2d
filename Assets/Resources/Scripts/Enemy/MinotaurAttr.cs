using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make sure to reference atkBoxPrefab in the inspector!
public class MinotaurAttr : EnemyManager
{
    [SerializeField] 
    public static float fovRange = 19f;
    public static float attackRange = 12f;
    public static int damage = 20;
    public static float atkSpd = 1f;
    public static float moveSpeed = 8f;

    public Transform atkBoxPrefab;

    private void Start()
    {
        FoVRange = fovRange;
        AttackRange = attackRange;
        Damage = damage;
        AtkSpeed = atkSpd;
        MoveSpeed = moveSpeed;
    }

    public override Transform GetAtkBox() => atkBoxPrefab;
    
    private void OnDrawGizmos()
    {
        // sightRange hitbox
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fovRange);
        
        // attackRange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
