using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class CheckEnemyNotInSight : Node
{
    private Transform _transform;

    public CheckEnemyNotInSight(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, EnemyManager.RetreatRange, EnemyManager.enemyLayerMask);
        //Debug.Log(EnemyManager.RetreatRange);
        if(colliders.Length > 0)
        {
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;

    }

}
