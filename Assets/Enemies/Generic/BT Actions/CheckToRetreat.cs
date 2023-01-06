using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using DebugTools;

// checks to see if the Player is too close
public class CheckToRetreat : Node
{
	private Transform _transform;
	private float _tooClose;

	private int _enemyLayerMask = EnemyManager.enemyLayerMask;

	public CheckToRetreat(Transform transform, float tooClose)
	{
		_transform = transform;
		_tooClose = tooClose;	
	}

	public override NodeState Evaluate()
	{
		if (EnemyManager.Busy)
        {
            state = NodeState.SUCCESS;
            return state;
        }
        

		Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, _tooClose, _enemyLayerMask);

		if (colliders.Length > 0)
		{
			// assuming the player is the only one on the enemyLayerMask
			// retreat if too close
			if(Vector2.Distance(_transform.position, colliders[0].transform.position) <= _tooClose)
			{			
				state = NodeState.SUCCESS;
				return state;
			}
			
		}
			
		state = NodeState.FAILURE;
		return state;
        
    }
	

	
}
