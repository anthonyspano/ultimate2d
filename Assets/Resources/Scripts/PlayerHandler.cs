using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{	
	// health
	public Transform pfHealthBar;	
	private HealthSystem pHealth;
	
	// ultimate
	public int maxUlt = 100;
	public int ultCharge = 0;
	public UltimateBar ultBar;

    private void Start()
    {
        pHealth = new HealthSystem(100, 1f);
		// added
		Transform healthBarTransform = Instantiate(pfHealthBar, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity, transform);
		HealthBar healthBar = healthBarTransform.GetComponent<HealthBar>(); 
		// end
        healthBar.Setup(pHealth);
		
		// ultimate bar
		ultBar.SetUlt(ultCharge);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            pHealth.Damage(20);
    }


}
