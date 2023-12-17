using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
public class BossTakeDamage : MonoBehaviour
{
    // health
    public HealthSystem healthSystem;

    public int maxHealth;

    public int ultAddedOnHit;

    private Animator anim;
    private EnemyManager em;
    private void Awake() 
    {
        anim = transform.parent.GetComponent<Animator>();
        
        // health
        healthSystem = new HealthSystem(maxHealth, 0f);
        healthSystem.OnHealthChanged += OnDamage;
        em = transform.parent.GetComponent<EnemyManager>();
    }

	private void OnDamage(object sender, System.EventArgs e) 
	{
    	if(healthSystem.GetHealth() <= 0)
		{
			// Death sequence
			//anim.SetBool("isDead", true);
            StartCoroutine("Death");
		}
        else 
        {
            StartCoroutine(FlashRed());
            if(healthSystem.GetHealthPercent() < .50f)
            {
                em.timeToReact = true;
            }
        }

	}

    IEnumerator Death()
    {
        // disable further movements
        // enemy manager
        transform.parent.GetComponent<BossBattleSystem>().CanMove = false;
        transform.parent.GetComponent<BossBattleSystem>().Dead = true;
        

        // play death anim
        anim.Play("enemy_death", 0);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length); 


        Destroy(transform.parent.gameObject);
    }

    public IEnumerator FlashRed()
    {
        var repeatTimes = 3;
        var timer = 0.1f; // just seems like a good number
        var sr = transform.parent.GetComponent<SpriteRenderer>();
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
        var myCollider = col.GetContact(0);
        if(col.GetContact(0).collider.transform.CompareTag("PlayerAttack"))
        {
            healthSystem.Damage(PlayerManager.Instance.Attack);
            PlayerManager.Instance.ultBar.AddUlt(PlayerManager.Instance.ultAddedOnHit); // consider source
        }
    }
}

}