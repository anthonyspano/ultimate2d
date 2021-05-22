using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyManager;

public class EnemyMove : MonoBehaviour
{
	private Vector2 playerPos;
	private float maxDistDelta; // 0.01f

	public float radius;

	private void Start() 
	{
		maxDistDelta = 0.01f;

	}


	public void Move()
	{
		// determine player's position
		GameObject player = GameObject.Find("Player");
		playerPos = player.transform.position;

		// accomplish being in range of player
		if (!InRange(player))
		{
			transform.position = Vector2.MoveTowards(transform.position, playerPos, maxDistDelta);
		}


	}

	public bool InRange(GameObject target)
    {
        float dX = transform.position.x - target.transform.position.x;
        float dY = transform.position.y - target.transform.position.y;

        return (dX < radius && dY < radius);
    }
	
	// void OnDrawGizmos() 
	// {
	// Gizmos.color = Color.red;
	// Gizmos.DrawWireSphere(transform.position, radius);
	// }
}
