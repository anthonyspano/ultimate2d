using System;
using System.Collections.Generic;

public class BTSequencer : BTComposite
{
	private int currentNode = 0;
	
	public BTSequencer(BehaviorTree t, BTNode [] children) : base(t, children)
	{
		
	}
	
	// if all children return success (AND gate)
	public override Result Execute()
	{
		if (currentNode < Children.Count)
		{
			Result result = Children[currentNode].Execute();
			
			if(result == Result.Running)
				return Result.Running;
			else if (result == Result.Failure)
			{
				currentNode = 0;
				return Result.Failure;
			}
			else
			{
				currentNode++;
				if (currentNode < Children.Count)
					return Result.Running;
				else
				{
					currentNode = 0;
					return Result.Success;
					
				}
				
			}
			
		}
		
		return Result.Success;
		
	}
	
}
