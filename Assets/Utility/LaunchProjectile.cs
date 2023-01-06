using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public void Fire(Transform bulletPrefab, Vector2 pos, Vector2 dir, float bulletSpeed)	
    {
        // spawn bullet
        Transform bulletClone = Instantiate(bulletPrefab, pos, Quaternion.identity);
        // gain invulnerability from bullet
        Physics2D.IgnoreCollision(bulletClone.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());		
        // add force
        Rigidbody2D bulletRB = bulletClone.GetComponent<Rigidbody2D>();
        bulletRB.AddForce(dir * bulletSpeed);

    }    
}
