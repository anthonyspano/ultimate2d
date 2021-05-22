using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
	private GameObject temp;
	public UltimateBar ultBar;
	private float ultCharge;
	
	private void Awake()
	{
		temp = GameObject.Find("UltBar");
		ultBar = temp.GetComponent<UltimateBar>();
		ultCharge = ultBar.GetUlt();
	}
	
    private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			Debug.Log(other.gameObject);
			ultCharge += 20;
			ultBar.SetUlt(ultCharge);
		}
	}
}
