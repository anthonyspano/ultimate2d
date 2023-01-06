using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonCrawlerDebug : MonoBehaviour
{
	[SerializeField]
	public int pursuitRange;
	public int retreatRange;
	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere(transform.position + new Vector3(0.5f, -0.7f, 0), pursuitRange);
		
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position + new Vector3(0.5f, -0.7f, 0), retreatRange);
		
	}
}
