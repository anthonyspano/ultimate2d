using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyManager;

public class EnemyShoot : LaunchProjectile
{
	public float time;
	private float timer;
	public float bulletSpeed;
	public Transform bulletPrefab;

	private GameObject player;

    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
        timer = time;

        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
	    if (timer > 0)
			timer -= Time.deltaTime;
		else
		{
			timer = time;
			var playerPos = player.transform.XandY();
			Fire(bulletPrefab, transform.position - Vector3.left, playerPos - transform.XandY(), bulletSpeed);
		}
		
		
    }

}
