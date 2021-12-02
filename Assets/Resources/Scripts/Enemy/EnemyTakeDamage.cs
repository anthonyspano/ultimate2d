using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    // health
    public HealthSystem healthSystem;
    public HealthBar healthBar; // referenced with scene healthbar

    private Animator anim;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        
        // health
        healthSystem = new HealthSystem(200, 0f);
        healthBar.Setup(healthSystem);
        // health - death event
        healthSystem.OnHealthChanged += OnDamage;
    }

	private void OnDamage(object sender, System.EventArgs e) 
	{
    	if(healthSystem.GetHealth() <= 0)
		{
			// Death sequence
			anim.SetBool("isDead", true);
		}
        else 
        {
            StartCoroutine(FlashRed());
        }

	}

    private void Death() 
    {
        // triggered after death animation
        anim.enabled = false;
        var scripts = gameObject.GetComponents(typeof(MonoBehaviour)); // scripts attached
        foreach (MonoBehaviour s in scripts)
        {
            s.enabled = false;
        }

    }

    public IEnumerator FlashRed()
    {
        var repeatTimes = 3;
        var timer = 0.1f; // just seems like a good number
        var sr = GetComponent<SpriteRenderer>();
		for(int i = 0; i < repeatTimes; i++)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(timer);
            sr.color = Color.white;
            yield return new WaitForSeconds(timer);
        }
    }
}
