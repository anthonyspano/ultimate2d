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
    private float coolDown = 0;

    public CheckEnemyInAttackRange(Transform transform)
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
     
        object t = GetData("target");
        if (t == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        Transform target = (Transform)t;
        //Debug.Log(coolDown);
        if (Vector3.Distance(_transform.position, target.position) <= MinotaurProperties.AttackRange && coolDown <= 0)
        {
            coolDown = MinotaurProperties.CoolDownRate;
            state = NodeState.SUCCESS;
            return state;
        }
        else
        {
            coolDown -= Time.deltaTime;
            _animator.Play("Running");
        }

        state = NodeState.FAILURE;
        return state;
    }
    
}
