using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;

// to adjust settings for other enemy types, refer to another enemy manager
// override enemy manager to fit class
public class EnemyBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;
    
    public static float speed;
    public static float fovRange;
    public static float attackRange;

    protected override Node SetupTree()
    {
        speed = 8f;
        fovRange = MinotaurAttr.fovRange;
        attackRange = 7f;

        Node root = new Selector(new List<Node>
        {
            // new Sequence(new List<Node>
            // {
            //     new CheckEnemyInAttackRange(transform),
            //     new TaskAttack(transform),
            // }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInFOVRange(transform),
                new TaskGoToTargetAndAttack(transform),
            }),
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }

}
