using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// change OnHealthChanged to flash red

public class PlayerManager : MonoBehaviour
{	
	// singleton
	private static PlayerManager _instance;
	public static PlayerManager Instance
	{
		get { return _instance; }
	}

	public static GameObject player;

	// health
	public Transform pfHealthBar;	
	public HealthSystem pHealth;
	public float positionOffset;
	
	// ultimate
	public int maxUlt = 100;
	public UltimateBar ultBar;
	public float invulnAfterHit;

	// animator
	private Animator anim;

	// for damage
	SpriteRenderer sr;

	// player properties
	[SerializeField] private int maxHealth;
	public float range;

	// etc
	private int wrongWayCount = 0;
	public GameObject wrongWayPanel;

	private void Start()
    {
	    if(player == null)
			player = GameObject.Find("Player");
	    
	    // health
        pHealth = new HealthSystem(maxHealth, invulnAfterHit);
        var healthBar = GameObject.Find("PlayerHealthBar").GetComponent<HealthBar>();
		healthBar.Setup(pHealth);
		
		// ult amt start of scene
		//ultBar.SetUlt(0);

		// death event
		pHealth.OnHealthChanged += OnDamage;
		anim = transform.GetComponent<Animator>();

		// damage
		sr = GetComponent<SpriteRenderer>();

		// wrong way dialogue box
		//wrongWayPanel = GameObject.Find("WrongWayDialogue");
    }
	
	private void Awake()
	{
		// singleton
		if (_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Enemy"))
		{
			Instance.pHealth.Damage(20);
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		if(col.transform.CompareTag("Door"))
		{
			var doorExit = col.transform.GetChild(0);
			Debug.Log(doorExit.gameObject.name);
			Debug.Log(doorExit.position);
			transform.position = doorExit.position;
		}


		if(col.transform.CompareTag("RockShadow"))
		{
			wrongWayCount++;
			// 24.5f
			transform.position = new Vector3(-16.93f, transform.position.y, transform.position.z);
			if(wrongWayCount > 2)
				wrongWayPanel.gameObject.SetActive(true);
				//Debug.Log("Maybe I'm going the wrong way...");

		}
	}

	private void OnDamage(object sender, System.EventArgs e) 
	{
		if(pHealth.GetHealth() <= 0)
		{
			// Death sequence
			anim.SetBool("isDead", true);
		}

		else
		{
			StartCoroutine(FlashRed());
		}
		
		

	}

	private IEnumerator FlashRed()
	{
		var timer = 0.28f;
		sr.color = Color.red;
		yield return new WaitForSeconds(timer);
		sr.color = Color.white;
		yield return new WaitForSeconds(timer);
		sr.color = Color.red;
		yield return new WaitForSeconds(timer);
		sr.color = Color.white;

	}

	private void Death() 
    {
        // disable minotaur
		//var boss = GameObject.Find("minotaur_boss");
		//var moveScript = boss.GetComponent<EnemyMove>();
		//moveScript.enabled = false;
		//var bossAnim = boss.GetComponent<Animator>();
		//bossAnim.enabled = false;
		//moveScript.player = GameObject.Find("DeadPlayerPlaceholder");

		// triggered after death animation
        anim.enabled = false;
        gameObject.GetComponent<PlayerMove>().enabled = false;

		
		var reticle = transform.Find("Reticle");
		reticle.gameObject.SetActive(false);
		
    }

}
