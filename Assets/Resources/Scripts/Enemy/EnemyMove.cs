using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyManager;

public class EnemyMove : MonoBehaviour
{
	private Vector2 playerPos;
	private float maxDistDelta = 0.01f; // 0.01f

	public float radius;
	

	public void Move()
	{
		// determine player's position
		playerPos = PlayerManager.player.transform.position;

		// accomplish being in range of player
		if (!InRange(playerPos))
		{
			transform.position = Vector2.MoveTowards(transform.position, playerPos, maxDistDelta);
		}


	}

	public bool InRange(Vector2 target)
    {
        float dX = transform.position.x - target.x;
        float dY = transform.position.y - target.y;

        return (dX < radius && dY < radius);
    }
	
	// void OnDrawGizmos() 
	// {
	// Gizmos.color = Color.red;
	// Gizmos.DrawWireSphere(transform.position, radius);
	// }
}
