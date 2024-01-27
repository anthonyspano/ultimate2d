using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
	public UltimateBar ultBar;

	private void Start()
	{
		ultBar = PlayerManager.Instance.ultBar;
	}
	
    private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("shadow hit 1");
		if(other.gameObject.CompareTag("HitBox"))
		{
			Debug.Log("shadow hit 2");
			ultBar.AddUlt(20);
		}
	}


}
