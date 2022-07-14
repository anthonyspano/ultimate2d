using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BehaviorTree;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CheckEnemyInAttackRange : Node
{
    private static int _enemyLayerMask = EnemyManager.enemyLayerMask;

    private Transform _transform;
    private Animator _animator;

    public CheckEnemyInAttackRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (EnemyManager.Busy)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        if (Vector3.Distance(_transform.position, target.position) <= EnemyBT.attackRange)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            _animator.Play("Running");
        }

        state = NodeState.FAILURE;
        return state;
    }
    
}
