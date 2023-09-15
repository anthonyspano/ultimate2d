using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
	public class EnemyShoot : LaunchProjectile
	{
		public float time;
		private float timer;
		public float bulletSpeed;
		public Transform bulletPrefab;

		private void Start()
		{
			Physics2D.queriesStartInColliders = false;
			timer = time;

		}

		private void FixedUpdate()
		{
			if (timer > 0)
				timer -= Time.deltaTime;
			else
			{
				timer = time;
				var playerPos = PlayerManager.player.transform.XandY();
				Fire(bulletPrefab, transform.position - Vector3.left, playerPos - transform.XandY(), bulletSpeed);
			}
			
			
		}

	}
}