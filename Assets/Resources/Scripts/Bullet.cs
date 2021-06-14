using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float lifeExpectency;
    
	void Update()
	{
		lifeExpectency -= Time.deltaTime;
		
		if(lifeExpectency <= 0)
			Destroy(gameObject);
		
	}
	
	void OnCollisionEnter2D(Collision2D col)
	{
		Destroy(gameObject);
		
	}


}
