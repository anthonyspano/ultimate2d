using System.Collections;
using System.Collections.Generic;

using BehaviorTree;

// to adjust settings for other enemy types, refer to another enemy manager
// override enemy manager to fit class
public class CrawlerTree : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;
    
    // see docs for tree

    protected override Node SetupTree()
    {

        Node root = new Selector(new List<Node>
        {
            new Sequence(new List<Node>
            {
				new CheckEnemyInPursuitRange(transform),
                new TaskGoToTargetJumpAtTarget(transform)
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyNotInSight(transform),
                new TaskPatrol(transform, waypoints)
            }) 

            /*
            new Sequence(new List<Node>
            {
                new CheckIfCovered(false),
                new TaskRetrieveCover()
                
            }),
            new Sequence(new List<Node>
            {
                new CheckIfCovered(true),
                new CheckEnemyInPursuitRange(transform),
                
            }),
            new Sequence(new List<Node>
            {
                new CheckEnemyInPursuitRange(transform), // check if enemy is attackable
                new TaskGoToTargetJumpAtTarget(transform)
            }),
            new Sequence(new List<Node>
            {

            })
            */
            
        });
        return root;
    }

}