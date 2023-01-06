using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static int enemyLayerMask = 1 << 8;
    private static float fovRange;
    private static float damage;
    public static float atkSpeed;
    private static float moveSpeed;
    private static float atkRange;
    private static bool canMove = true;

    private Transform atkBox;

/*     public virtual Transform GetAtkBox()
    {
        Debug.Log("this is my no no spot!");
        return atkBox;
    } */

    protected float AttackRange
    {
        get { return atkRange; }
        set { atkRange = value; }
    }
    
    protected float Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    protected float AtkSpeed
    {
        get { return atkSpeed; }
        set { atkSpeed = value; }
    }
    protected float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }
    protected float FoVRange
    {
        get { return fovRange; }
        set { fovRange = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    private SpriteRenderer sr;

    public static bool Busy = false;
    public static bool Flipped;
    public static bool Retreating;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // player location
        if (sr) sr.flipX = Flipped;
    }

    public void CanMoveAgain()
    {
        CanMove = true;
    }

}
