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
    private static float retreatRange;
    private static bool canMove = true;

    private static float pursuitRange;

    public AudioClip attackSound;

    public static Transform AttackBox;

    public static float PursuitRange
    {
        get { return pursuitRange; }
        set { pursuitRange = value; }
    } 

    public static float RetreatRange
    {
        get { return retreatRange; }
        set { retreatRange = value; }
    }
    public static float AttackRange
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
    public static float FoVRange
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
    public static bool Retreating;

    public static int jumpSpeed = 15;


    public void CanMoveAgain()
    {
        CanMove = true;
    }




}
