using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
                                                           
// update fovRange
public class CheckEnemyInPursuitRange : Node
{
    private static int _enemyLayerMask = EnemyManager.enemyLayerMask;
    
    private Transform _transform;
    private Animator _animator;
	
	private float _pursuitRange = 16;
	private float _tooClose;
    
    public CheckEnemyInPursuitRange(Transform transform, float tooClose)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
		_tooClose = tooClose;
    }

    public override NodeState Evaluate()
    {
        if (EnemyManager.Busy)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _pursuitRange , _enemyLayerMask);

        if (colliders.Length > 0)
        {
            // assuming the player is the only one on the enemyLayerMask
            parent.parent.SetData("target", colliders[0].transform);

            if (Vector2.Distance(_transform.position, colliders[0].transform.position) > _tooClose)
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
