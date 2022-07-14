using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttackLanding : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AttackLands()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, MinotaurAttr.attackRange, EnemyManager.enemyLayerMask);
        if(player.Length > 0)
            if (player[0].name == "Player")
            {
                PlayerManager.Instance.pHealth.Damage(MinotaurAttr.damage);
            }
        
        _animator.Play("Idle");
        
        // Start event where rocks fall from ceiling

        
        // allow minotaur to move
        GetComponent<EnemyManager>().CanMove = true;
    }
}
