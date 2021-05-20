using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowController : MonoBehaviour
{
	private GameObject temp;
	public UltimateBar ultBar;
	private int ultCharge = 0;
	
	void Awake()
	{
		Debug.Log(GameObject.Find("UltBar"));
		temp = GameObject.Find("UltBar");
		ultBar = temp.GetComponent<UltimateBar>();
	}
	
    void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			ultCharge += 50;
			ultBar.SetUlt(ultCharge);
		}
	}
}
