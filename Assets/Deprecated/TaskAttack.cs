using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DebugTools;


public class TaskAttack : Node
{
    private Animator _animator;
    
    private Transform _lastTarget;
    private Transform _transform;
    private EnemyManager _enemyManager;
    private PlayerManager _playerManager = PlayerManager.Instance;

    // attack counter
    private float _attackTime = MinotaurAttr.atkSpd;
    private float _attackCounter = MinotaurAttr.atkSpd+1;

    public TaskAttack(Transform transform)
    {
        _animator = transform.GetComponent<Animator>();
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        MinotaurProperties.IsBusy = true;
        Debug.Log("Attacking");
        Transform target = (Transform)GetData("target");
        if (target != _lastTarget)
        {
            _lastTarget = target;
        }

        _animator.Play("Attack");

        var axeHitBox = GameObject.Find("AxeHitBox");
        var hits = Physics2D.OverlapCircleAll(axeHitBox.transform.position, 1.25f, LayerMask.GetMask("Player")); // put proper radius
        foreach (var col in hits)
        {
            // do damage to player
            PlayerManager.Instance.pHealth.Damage(20);

        }
            
        // check if player is dead
        if (_playerManager.pHealth.GetHealth() <= 0)
        {
            ClearData("target");

            _animator.Play("Idle");
    
        }

        state = NodeState.RUNNING;
        return state;
    }
    
    
}
