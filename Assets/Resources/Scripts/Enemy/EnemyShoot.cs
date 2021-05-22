using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyManager;

public class EnemyShoot : MonoBehaviour
{
	private Rigidbody2D rb2D;
	public Transform bulletPrefab;
	private Vector2 playerPos;
	public float bulletSpeed; // 40

	public float time;
	private float timer;
	

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rb2D = GetComponent<Rigidbody2D>();

        timer = time;
    }

    private void FixedUpdate()
    {
	    if (timer > 0)
			timer -= Time.deltaTime;
		else
		{
			timer = time;
			Fire();
		}
		
		
    }
    
	
	public void Fire()	
	{
		// locate player
		playerPos = GameObject.Find("Player").transform.position;
		// spawn bullet
		Transform bulletClone = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
		// gain invulnerability from bullet
		Physics2D.IgnoreCollision(bulletClone.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());		
		// add force
		Rigidbody2D bulletRB = bulletClone.GetComponent<Rigidbody2D>();
		Vector2 traj = playerPos - (Vector2)transform.position; 
		bulletRB.AddForce(traj * bulletSpeed);

	}
	
}
