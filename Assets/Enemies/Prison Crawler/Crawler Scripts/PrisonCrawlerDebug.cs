using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonCrawlerDebug : MonoBehaviour
{
	[SerializeField]
	public float pursuitRange;
	public float attackRange;
	void OnDrawGizmos()
	{
		// Vector3 offset = new Vector3(0.5f, -0.7f, 0);

		// Gizmos.color = Color.yellow;
		// Gizmos.DrawWireSphere(transform.position + offset, pursuitRange);
		
		// Gizmos.color = Color.red;
		// Gizmos.DrawWireSphere(transform.position + offset, attackRange);
		
	}
}
