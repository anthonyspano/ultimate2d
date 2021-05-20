using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNode : MonoBehaviour
{
	public enum Result { Running, Failure, Success };
	
	public BehaviorTree Tree { get; set; }
	
	public BTNode(BehaviorTree t)
	{
		Tree = t;
	}

	public virtual Result Execute()
	{
		return Result.Failure;
	}



}
