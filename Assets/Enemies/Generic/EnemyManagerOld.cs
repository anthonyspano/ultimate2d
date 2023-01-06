using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyManagerOld : MonoBehaviour
{
	public Transform pfHealthBar;
	
	public HealthSystem hSystem;
	[HideInInspector]
	public HealthBar hBar;


	private void Start()
    {
		// Health
        hSystem = new HealthSystem(100, 0f);
        var healthBarTransform = Instantiate(pfHealthBar, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity, transform);
		hBar = healthBarTransform.GetComponent<HealthBar>();
		hBar.Setup(hSystem);
		// subscribe to health change event
		hSystem.OnHealthChanged += OnDeath;

    }

	private void OnDeath(object sender, System.EventArgs e)
	{
		if(hSystem.GetHealth() <= 0)
			Destroy(gameObject);
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
		
        if (collision.gameObject.CompareTag("Player"))
        {
            hSystem.Damage(20);
        }
    }
}
