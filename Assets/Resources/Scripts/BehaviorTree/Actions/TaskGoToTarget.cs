﻿using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskGoToTarget : Node
{
    private Transform _transform;

    public TaskGoToTarget(Transform transform)
    {
        _transform = transform;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        EnemyManagerTemp.Flipped = (target.position.x > _transform.position.x) ? true : false;
        
        if (Vector3.Distance(_transform.position, target.position) > 0.01f)
        {
            _transform.position = Vector3.MoveTowards(
                _transform.position, target.position, GuardBT.speed * Time.deltaTime);
        }

        state = NodeState.RUNNING;
        return state;
    }
}
