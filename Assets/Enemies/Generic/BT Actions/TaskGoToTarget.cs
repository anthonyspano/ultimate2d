using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using DebugTools;


// update speed var
// go to the target's tracked position and hit the ground
public class TaskGoToTarget : Node
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

    public TaskGoToTarget(Transform transform)
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
        if(MinotaurProperties.IsBusy)
        {
            state = NodeState.FAILURE;
            return state;
        }
        //Debug.Log("Going to target");

        _attackCounter += Time.deltaTime;
        
        // spawn attack box
        if (GameObject.Find("AttackBoxIndication(Clone)") == null)
        {
            var t = (Transform)GetData("target");
			_atkBox = Resources.Load("AttackBoxIndication") as GameObject;
            atkBox = Object.Instantiate(_atkBox.transform, t.position, Quaternion.identity);
            stopPoint = atkBox.position;
            
        }
        
        // move towards new position
        if (Vector3.Distance(_transform.position, stopPoint) > MinotaurProperties.AttackRange) // pos away from target
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, stopPoint, speed * Time.deltaTime);
            _animator.Play("Running");
        }
        // else  // attack
        // {
        //     _enemyManager.CanMove = false;
            
        //     if (_attackCounter >= _attackTime)
        //     {
		// 		// set bools for attacks
		// 		//Debug.Log(_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"));
        //         if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		// 		{
        //             _animator.Play("Attack");					
		// 		}
				
        //         ClearData("target");
        //         _attackCounter = 0f;
        //         Object.Destroy(atkBox.gameObject);
        //         state = NodeState.SUCCESS;
        //         return state;
                               
        //     }
        // }
        

        state = NodeState.RUNNING;
        return state;
    }
}
