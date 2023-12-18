using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UltimateBar))]
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
	    //ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
    }

    private void Update()
    {
	    if (PlayerInput.Shoot())
	    {
            var pos = transform.position + PlayerManager.Instance.LastMove.normalized;
		    Fire(bulletPrefab, pos, pos - transform.position, bulletSpeed);
	    }
	    
    }

    
}
