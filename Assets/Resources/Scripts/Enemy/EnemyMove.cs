using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyController;

public class EnemyMove : MonoBehaviour
{
	Vector2 playerPos;
	float mDD; // 0.01f
	bool isInRange;
	
	//enum State {Moving, Standing, Shooting};
	CurrentState eState;
	
	public float radius;
	
	private EnemyController eController;
	
	
	void Start() 
	{
		mDD = 0.00001f;
		eController = GetComponent<EnemyController>();
		
	}
	
	
	public void Move() 
	{
		// determine player's position
		GameObject player = GameObject.Find("Player");
		playerPos = player.transform.position;
		
		// accomplish being in range of player
		if (!InRange(player))
		{
			transform.position = Vector2.MoveTowards(transform.position, playerPos, mDD);
			//eState = CurrentState.Moving;
			//eController.setState(eState);
		}
		else
		{		
			//Debug.Log("Shooting");
			eState = CurrentState.Shooting;
			//eController.setState(eState);
		}
		
		
	}
	
	public IEnumerator FinishMove()
	{
		yield return null;
		
		// determine player's position
		GameObject player = GameObject.Find("Player");
		playerPos = player.transform.position;
		
		// accomplish being in range of player
		while (!InRange(player))
		{
			transform.position = Vector2.MoveTowards(transform.position, playerPos, mDD);	
		}
		
		//eState = CurrentState.Shooting;
		//eController.setState(eState);
	}
	
	// void OnDrawGizmos() 
	// {
		// Gizmos.color = Color.red;
		// Gizmos.DrawWireSphere(transform.position, radius);
	// }
	
	
	bool InRange(GameObject target)
    {
        float dX = transform.position.x - target.transform.position.x;
        float dY = transform.position.y - target.transform.position.y;

        if (dX < radius && dY < radius)
            return true;

        else
            return false;
    }
}
