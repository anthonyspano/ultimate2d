using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAttacks : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void StopAttack()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, EnemyManagerTemp.attackRange, EnemyManagerTemp.enemyLayerMask);
        if (player[0].name == "Player")
        {
            PlayerManager.Instance.pHealth.Damage(EnemyManagerTemp.damage);
        }
        _animator.Play("Idle");
    }
}
