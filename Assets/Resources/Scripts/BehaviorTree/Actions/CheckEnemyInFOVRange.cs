using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{
    private static int _enemyLayerMask = EnemyManager.enemyLayerMask;
    
    private Transform _transform;
    private Animator _animator;
    
    public CheckEnemyInFOVRange(Transform transform)
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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, EnemyBT.fovRange , _enemyLayerMask);

            if (colliders.Length > 0)
            {
                parent.parent.SetData("target", colliders[0].transform);
                _animator.Play("Running");
                
                state = NodeState.SUCCESS;
                return state;
            }
            
            state = NodeState.FAILURE;
            return state;
        }

        state = NodeState.SUCCESS;
        return state;
    }
}
