using System;

public class Decorator : BTNode
{
	public BTNode Child { get; set; }
	public Decorator(BehaviorTree t, BTNode c) : base(t)
	{
		Child = c;
	}
}
