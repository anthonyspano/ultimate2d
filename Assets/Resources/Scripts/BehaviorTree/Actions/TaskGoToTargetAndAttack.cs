using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using DebugTools;



// go to the target's tracked position and hit the ground
public class TaskGoToTargetAndAttack : Node
{
    private Transform _transform;
    private Animator _animator;

    private EnemyManager _enemyManager;
    
    // attack counter
    private float _attackTime = MinotaurAttr.atkSpd;
    private float _attackCounter = MinotaurAttr.atkSpd+1;

    public TaskGoToTargetAndAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _enemyManager = transform.GetComponent<EnemyManager>();
    }

    public override NodeState Evaluate()
    {
        WindowStats.Value = _attackCounter;
        
        Transform target = (Transform)GetData("target");

        EnemyManager.Flipped = (target.position.x > _transform.position.x) ? true : false;

        _attackCounter += Time.deltaTime;
        
        if (Vector3.Distance(_transform.position, target.position) > 10f && _enemyManager.CanMove)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, EnemyBT.speed * Time.deltaTime);
            _animator.Play("Running");
        }
        else
        {
            // disable moving - bool canMove
            _enemyManager.CanMove = false;
            
            // attack()
            if (_attackCounter >= _attackTime)
            {
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("mino_atk1"))
                    _animator.Play("Attack");
                
                // check if player is dead
                if (PlayerManager.Instance.pHealth.GetHealth() <= 0)
                {
                    ClearData("target");
                    _animator.Play("Patrol");
                }
                else
                {
                    _attackCounter = 0f;
                }


            }
        }

        state = NodeState.RUNNING;
        return state;
    }
}
