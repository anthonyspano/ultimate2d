using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
                                                           
// update fovRange
public class CheckEnemyInPursuitRange : Node
{
    
    private Transform _transform;
    private Animator _animator;
	
    
    public CheckEnemyInPursuitRange(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        if (MinotaurProperties.IsBusy)
        {
            state = NodeState.FAILURE;
            return state;
        }
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, MinotaurProperties.SightRange, MinotaurProperties.PlayerMask);

        if (colliders.Length > 0)
        {
            // assuming the player is the only one on the enemyLayerMask
            parent.parent.SetData("target", colliders[0].transform);

            if (Vector2.Distance(_transform.position, colliders[0].transform.position) > MinotaurProperties.AttackRange)
            {
                _animator.Play("Running");
                
                state = NodeState.SUCCESS;
                return state;
            }	
        }
        
        state = NodeState.FAILURE;
        return state;
    }
}
