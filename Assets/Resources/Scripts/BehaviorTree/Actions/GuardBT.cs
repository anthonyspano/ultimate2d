using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree;
public class GuardBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;

    protected override Node SetupTree()
    {
        Node root = new TaskPatrol(transform, waypoints);
        return root;
    }

}
