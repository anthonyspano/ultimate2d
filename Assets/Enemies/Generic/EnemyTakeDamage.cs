using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamage : MonoBehaviour
{
    // health
    public HealthSystem healthSystem;
    //public HealthBar healthBar; // referenced with scene healthbar

    private Animator anim;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        
        // health
        healthSystem = new HealthSystem(200, 0f);
        //healthBar.Setup(healthSystem);
        // health - death event
        healthSystem.OnHealthChanged += OnDamage;
    }

	private void OnDamage(object sender, System.EventArgs e) 
	{
        Debug.Log("hit");
    	if(healthSystem.GetHealth() <= 0)
		{
			// Death sequence
			//anim.SetBool("isDead", true);
            Death();
		}
        else 
        {
            StartCoroutine(FlashRed());
        }

	}

    private void Death() 
    {
        // triggered after death animation
        // var scripts = gameObject.GetComponents(typeof(MonoBehaviour)); // scripts attached
        // foreach (MonoBehaviour s in scripts)
        // {
        //     s.enabled = false;
        //     Debug.Log(s);
        // }

        // anim.enabled = false;

        Destroy(gameObject);

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

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     if(col.transform.CompareTag("PlayerAttack"))
    //         healthSystem.Damage(70);
    // }
}
