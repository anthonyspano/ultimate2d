using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
	public UltimateBar ultBar;

	private void Awake()
	{
		ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
	}
	
    private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			ultBar.AddUlt(50);
		}
	}
}
