using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using DebugTools;


// update speed var
// go to the target's tracked position and hit the ground
public class TaskGoToTargetJumpAtTarget : Node
{
    private Transform _transform;
    private Animator _animator;

    private EnemyManager _enemyManager;
    //private MinotaurAttr _enemyManager;
	
	private GameObject _atkBox;
    private Transform atkBox;
    
    // attack counter
    private float _attackTime = 2f;
    private float _attackCounter = EnemyManager.atkSpeed+1;
	private float speed = 8f;
    
    // acquire target
    //private Transform target;
    private Vector2 stopPoint = Vector2.zero;

    public TaskGoToTargetJumpAtTarget(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _enemyManager = transform.GetComponent<EnemyManager>();
        
    }

    // spawn an object (where the player stood for that frame) to be hit
    // set busy to be true until that object is hit
    // repeat
    public override NodeState Evaluate()
    {
        Debug.Log("Going to target");
        // if(EnemyManager.Retreating)
        // {
        //     state = NodeState.FAILURE;
        //     return state;
        // }

        // if(!EnemyManager.Busy)
        // {
        //     EnemyManager.Busy = true;
        //     //WindowStats.IsBusy = EnemyManager.Busy;
        // }

        WindowStats.Value = _attackCounter; // static script for debugging
        _attackCounter += Time.deltaTime;
        
        // spawn attack box
        if (GameObject.Find("AttackBoxIndication(Clone)") == null)
        {
            var t = (Transform)GetData("target");
			_atkBox = Resources.Load("AttackBoxIndication") as GameObject;
            atkBox = Object.Instantiate(_atkBox.transform, t.position, Quaternion.identity);
            stopPoint = atkBox.position;
            
        }
        
        // flip based on player pos
        //EnemyManager.Flipped = (stopPoint.x < _transform.position.x) ? true : false;
        
        // move towards new position
        if (Vector3.Distance(_transform.position, stopPoint) > 7f && _enemyManager.CanMove) // pos away from target
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, stopPoint, speed * Time.deltaTime);
            _animator.Play("Running");
        }
        else  // attack
        {
            _enemyManager.CanMove = false;
            
            if (_attackCounter >= _attackTime)
            {
				float d = 0.25f;
				// set bools for attacks
				//Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
				{
                    _animator.Play("Attack");
					// jump to target (stopPoint)
					_transform.position = Vector2.Lerp(_transform.position, stopPoint, d);
					
				}
				
				d += 0.25f;
				
				if (Vector2.Distance(_transform.position , stopPoint) > 0.01f)
                {
					_transform.position = Vector2.Lerp(_transform.position, stopPoint, 0.25f);
                    state = NodeState.RUNNING;
                    return state;
                }
                else
                {
					//ClearData("target");
					//EnemyManager.Busy = false;
                    WindowStats.IsBusy = EnemyManager.Busy;
                    _attackCounter = 0f;
                    Object.Destroy(atkBox.gameObject);
                    state = NodeState.SUCCESS;
                    return state;
                    
                }
                
                
            }
        }
        

        state = NodeState.RUNNING;
        return state;
    }
}
