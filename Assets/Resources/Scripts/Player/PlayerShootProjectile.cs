using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UltimateBar))]
[RequireComponent(typeof(PlayerAim))]
public class PlayerShootProjectile : LaunchProjectile
{
	private UltimateBar ultBar;
    public int ultChargeAmt;

    public float bulletSpeed;
    public Transform bulletPrefab;
    
    private PlayerAim myAim;
    public float crosshairSize;
    
    private void Start()
    {
	    myAim = GetComponent<PlayerAim>();
	    ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
    }

    private void Update()
    {
	    if (Input.GetButtonDown("Fire2"))
	    {
		    var pos = myAim.center;
		    Debug.Log("speed: " + bulletSpeed);
		    Fire(bulletPrefab, pos, pos - transform.XandY(), bulletSpeed);
	    }
	    
    }

    private void OnDrawGizmosSelected()
    {
	    //Gizmos.color = Color.red;
	    //Gizmos.DrawWireSphere(myAim.center, crosshairSize);
    }
    
}
