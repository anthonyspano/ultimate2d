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
    //private MinotaurAttr _enemyManager;
    
    // attack counter
    private float _attackTime = MinotaurAttr.atkSpd;
    private float _attackCounter = MinotaurAttr.atkSpd+1;
    
    // acquire target
    //private Transform target;
    private Vector2 stopPoint = Vector2.zero;

    public TaskGoToTargetAndAttack(Transform transform)
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _enemyManager = transform.GetComponent<MinotaurAttr>();
        
    }

    // spawn an object (where the player stood for that frame) to be hit
    // set busy to be true until that object is hit
    // repeat
    public override NodeState Evaluate()
    {
        // target = player's transform each new frame
        EnemyManager.Busy = true;
        WindowStats.Value = _attackCounter;
        _attackCounter += Time.deltaTime;
        
        // spawn attack box
        if (GameObject.Find("AttackBoxIndication(Clone)") == null)
        {
            var t = (Transform)GetData("target");
            Transform atkBox = Object.Instantiate(_enemyManager.GetAtkBox(), t.position, Quaternion.identity);
            stopPoint = atkBox.position;
        }
        
        // flip based on player pos
        EnemyManager.Flipped = (stopPoint.x > _transform.position.x) ? true : false;
        
        // move towards new position
        if (Vector3.Distance(_transform.position, stopPoint) > 12f && _enemyManager.CanMove)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, stopPoint, EnemyBT.speed * Time.deltaTime);
            _animator.Play("Running");
        }
        else  // attack
        {
            _enemyManager.CanMove = false;
            
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
