using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;


public class TaskAttack : Node
{
    private Animator _animator;
    
    private Transform _lastTarget;
    private Transform _transform;
    private EnemyManager _enemyManager;
    private PlayerManager _playerManager = PlayerManager.Instance;

    // attack counter
    private float _attackTime = 3f;
    private float _attackCounter = 0f;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        //if(!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //    _animator.SetBool("isIdle", true);
        
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
             // attack once
             _animator.Play("Attack");
            
             // assign damage if hit
             // Collider2D[] player = Physics2D.OverlapCircleAll(_transform.position, EnemyManagerTemp.attackRange, EnemyManagerTemp.enemyLayerMask);
             // if (player[0].name == "Player")
             // {
             //     _playerManager.pHealth.Damage(EnemyManagerTemp.damage);
             // }
            
             // check if player is dead
             if (_playerManager.pHealth.GetHealth() <= 0)
             {
                 ClearData("target");

                 _animator.Play("Patrol");
            
             }
             else
             {
                 _attackCounter = 0f;
             }
        }

        state = NodeState.RUNNING;
        return state;
    }
    
    
}
