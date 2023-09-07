using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
public class EnemyTakeDamage : MonoBehaviour
{
    // health
    public HealthSystem healthSystem;
    //public HealthBar healthBar; // referenced with scene healthbar

    public int maxHealth;

    public int ultAddedOnHit;

    private Animator anim;
    private void Start() 
    {
        anim = GetComponent<Animator>();
        
        // health
        healthSystem = new HealthSystem(maxHealth, 0f);
        //healthBar.Setup(healthSystem);
        // health - death event
        healthSystem.OnHealthChanged += OnDamage;
    }

	private void OnDamage(object sender, System.EventArgs e) 
	{
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
        var scripts = gameObject.GetComponents(typeof(MonoBehaviour)); // scripts attached
        foreach (MonoBehaviour s in scripts)
        {
            s.enabled = false;
            //Debug.Log(s);
        }

        //anim.enabled = false;

        StartCoroutine("EnemyDying");

    }

    IEnumerator EnemyDying()
    {
        // disable further movements
        gameObject.GetComponent<BattleSystem>().enabled = false;

        // play death anim
        //anim.Play("Death", 0);
        //yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length); 

        var sr = GetComponent<SpriteRenderer>();

        sr.enabled = true;
        var i = 0;
        float flickerTime = 0.33f;
        while (i < 3)
        {
            yield return new WaitForSeconds(flickerTime);
            sr.enabled = false;

            yield return new WaitForSeconds(flickerTime);
            sr.enabled = false;

            i++;
        }



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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            healthSystem.Damage(PlayerManager.Instance.Attack);
            PlayerManager.Instance.ultBar.AddUlt(PlayerManager.Instance.ultAddedOnHit); // consider source
        }
    }
}

}