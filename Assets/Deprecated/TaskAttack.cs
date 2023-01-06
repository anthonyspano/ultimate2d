/* using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DebugTools;


public class TaskAttack : Node
{
    private Animator _animator;
    
    private Transform _lastTarget;
    private Transform _transform;
    private EnemyManager _enemyManager;
    private PlayerManager _playerManager = PlayerManager.Instance;

    // attack counter
    private float _attackTime = MinotaurAttr.atkSpd;
    private float _attackCounter = MinotaurAttr.atkSpd+1;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        WindowStats.Value = _attackCounter;
        EnemyManager.Busy = true;
        
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lastTarget = target;
        }

        _attackCounter += Time.deltaTime;
        if (_attackCounter >= _attackTime)
        {
            _animator.Play("Attack");
             
             // check if player is dead
             if (_playerManager.pHealth.GetHealth() <= 0)
             {
                 ClearData("target");

                 _animator.Play("Patrol");
            
             }
             else
             {
                 _attackCounter = 0f;
                 EnemyManager.Busy = false;
             }
        }

        state = NodeState.RUNNING;
        return state;
    }
    
    
}
 */