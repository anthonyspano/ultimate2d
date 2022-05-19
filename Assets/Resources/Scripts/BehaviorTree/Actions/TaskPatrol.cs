using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator _animator;
    private Transform[] _waypoints;

    private int _currentWaypointIndex = 0;

    private float _waitTime = 2.2f;
    private float _waitCounter = 0f;
    private bool _waiting = false;
    private float _speed;


    public TaskPatrol(Transform transform, Transform[] waypoints) 
    {
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {
        if(_waiting)
        {
            _waitCounter += Time.deltaTime;
            if(_waitCounter >= _waitTime)
            {
                _waiting = false;
                _animator.SetBool("Walking", true);
            }
        }
        else
        {
            Transform wp = _waypoints[_currentWaypointIndex];
            if(Vector2.Distance(_transform.position, wp.position) < 0.1f)
            {
                _animator.SetBool("Walking", false);
                _transform.position = wp.XandY();
                _waitCounter = 0f;
                _waiting = true;

                _currentWaypointIndex = (_currentWaypointIndex + 1) % _waypoints.Length;
            }
            else
            {
                _transform.position = Vector2.MoveTowards(_transform.position, wp.position, 8f * Time.deltaTime);
            }

        }

        state = NodeState.RUNNING;
        return state;
    }
}
