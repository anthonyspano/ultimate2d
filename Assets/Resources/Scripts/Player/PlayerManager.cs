using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{	
	// health
	public Transform pfHealthBar;	
	private HealthSystem pHealth;
	public float positionOffset;
	
	// ultimate
	public int maxUlt = 100;
	public UltimateBar ultBar;

    private void Start()
    {
	    // health
        pHealth = new HealthSystem(100, 1f);
        var healthBarTransform = Instantiate(pfHealthBar, 
								     new Vector3(transform.position.x, transform.position.y + positionOffset), 
								     Quaternion.identity, 
								     transform);
		var healthBar = healthBarTransform.GetComponent<HealthBar>();
		healthBar.Setup(pHealth);
		
		// ult amt start of scene
		ultBar.SetUlt(0);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
            pHealth.Damage(20);
    }


}
