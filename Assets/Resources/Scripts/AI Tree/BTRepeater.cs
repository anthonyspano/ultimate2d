using System;
using UnityEngine;

public class BTRepeater : Decorator
{
	public BTRepeater(BehaviorTree t, BTNode c) : base(t,c)
	{
	}
	
	public override Result Execute()
	{
		//Debug.Log("Child returned: " + Child.Execute());
		return Result.Running;
	}
}
