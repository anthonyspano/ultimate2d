using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// make sure to reference atkBoxPrefab in the inspector!
public class MinotaurAttr : EnemyManager
{
    public static float fovRange = 19f;
    public static float attackRange = 12f;
    public static float hitRange = 5.5f; 
    public static int damage = 30;
    public static float atkSpd = 3f; // 1.2f
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

    //public override Transform GetAtkBox() => atkBoxPrefab;

    public Vector2 GetHitPos()
    {
        // return a Vector2 with x +- 3 depending on flipped
        float xAdjust = 5;
        return (Flipped()) ? transform.XandY() + (Vector2.right * xAdjust) + Vector2.down * 3 : transform.XandY() + Vector2.left * xAdjust + Vector2.down * 3;
    }
    
    private void OnDrawGizmos()
    {
        // sightRange hitbox
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, fovRange);
        
        // attackRange
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        
        // hitRange
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(GetHitPos(), hitRange);
    }

    public bool Flipped()
    {
        if(transform.position.x - PlayerManager.Instance.transform.position.x < 0)
        {
            return false;
        }

        return true;
    }
}
