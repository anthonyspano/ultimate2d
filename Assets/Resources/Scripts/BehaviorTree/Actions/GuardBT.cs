using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class GuardBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;
    
    public static float speed = 8f;
    public static float fovRange = EnemyManagerTemp.fovRange;
    public static float attackRange = EnemyManagerTemp.attackRange;

    protected override Node SetupTree()
    {
        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
                new CheckEnemyInAttackRange(transform),
                new TaskAttack(transform),
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTarget(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }

}
