﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using DebugTools;

public class TaskRetreat : Node
{
    private Transform _transform;
    private Transform player;
    private Vector3 retreatPos;

    public TaskRetreat(Transform transform)
    {
        _transform = transform;
    }    
    
    public override NodeState Evaluate()
    {

        if(EnemyManager.Busy)
        {
            state = NodeState.FAILURE;
            return state;
        }

        if(!EnemyManager.Retreating)
        {
            EnemyManager.Retreating = true;		
            WindowStats.IsRetreating = EnemyManager.Retreating;
            // create a position that leads enemy away from player 
            player = (Transform)GetData("target");
            Debug.Log("Current: " + _transform.position);
            retreatPos = _transform.position - player.position;
            Debug.Log("Target position: " + (retreatPos));
            _transform.position = Vector3.MoveTowards(_transform.position, retreatPos, 8 * Time.deltaTime);
            //GameObject.Instantiate(Resources.Load("AttackBoxIndication"), retreatPos, Quaternion.identity);
        }
        else
        {
            _transform.position = Vector3.MoveTowards(_transform.position, retreatPos, 8 * Time.deltaTime);
            Debug.Log("retreating");
            Debug.Log("Distance: " + Vector2.Distance(_transform.position, retreatPos));
            if(Vector2.Distance(_transform.position, player.position) > 3f)
            {
                EnemyManager.Retreating = false;
                WindowStats.IsRetreating = EnemyManager.Retreating;
            }

        }

            state = NodeState.SUCCESS;
            return state;
    }
}
