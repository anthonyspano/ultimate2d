using System.Collections;
using System.Collections.Generic;

using BehaviorTree;

// to adjust settings for other enemy types, refer to another enemy manager
// override enemy manager to fit class
public class CrawlerTree : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;
    
    public static float speed;
    public static float fovRange;
    public static float attackRange;
    public static float tooClose;

    protected override Node SetupTree()
    {
        speed = 8f;
        attackRange = 7f;
		tooClose = 7;

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
				new CheckEnemyInPursuitRange(transform, tooClose),
                new TaskGoToTargetJumpAtTarget(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckToRetreat(transform, tooClose),
                new TaskRetreat(transform)
            }),
            new TaskPatrol(transform, waypoints),
        });
        return root;
    }

}