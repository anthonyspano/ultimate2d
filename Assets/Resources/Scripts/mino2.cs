using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// change OnHealthChanged to flash red and delete sr in start and var

public class mino2 : MonoBehaviour
{
    private Animator anim;
    [HideInInspector] public GameObject player;

    private float distX;
    private float distY;

    [SerializeField]
    private float sightRange;
    [SerializeField]
    private float atkRange;
    [SerializeField]
    private float runSpeed;

    public int assignedPlayerDamage;

    // attacking flow control
    private bool isSwinging;
    private bool once = true;

    //testing
    private Vector2 tPos;

    // health
    public HealthSystem healthSystem;
    public HealthBar healthBar; // referenced with scene healthbar

    // hitbox
    public float hitboxSize;
    public LayerMask playerLayer;
    private Vector3 hitboxPos;


    // remove later
    SpriteRenderer sr;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = PlayerManager.player;

        // health
        healthSystem = new HealthSystem(200, 0f);
        healthBar.Setup(healthSystem);
        // health - death event
        healthSystem.OnHealthChanged += OnDamage;

        sr = GetComponent<SpriteRenderer>();
    }

	private void OnDamage(object sender, System.EventArgs e) 
	{
		StartCoroutine(FlashRed());
    
    	if(healthSystem.GetHealth() <= 0)
		{
			// Death sequence
			anim.SetBool("isDead", true);
		}

	}

	private IEnumerator FlashRed()
	{
		var timer = 0.33f;
		sr.color = Color.red;
		yield return new WaitForSeconds(timer);
		sr.color = Color.white;
		// yield return new WaitForSeconds(timer);
		// sr.color = Color.red;
		// yield return new WaitForSeconds(timer);
		// sr.color = Color.white;

	}

    private void Update()
    {
        
        hitboxPos = GameObject.Find("AxeHitBox").transform.position; 
        var playerPos = player.transform.position;
        distX = Math.Abs(transform.position.x - playerPos.x);
        distY = Math.Abs(transform.position.y - playerPos.y);
        
        anim.SetBool("inSight", inSight(distX,distY));
        anim.SetBool("inRange", inRange(distX,distY));
        
        // sight range 
        if (inSight(distX, distY) && !inRange(distX, distY))
        {  
            anim.SetBool("inSight", true);
            // probably move towards playerpos but set value to distance traveled in one frame
            // towards: (playerPos - transform.position).normalize
            tPos = transform.position + ((playerPos - transform.position).normalized);
            transform.position = Vector2.MoveTowards(transform.position, tPos, runSpeed * Time.deltaTime);
        }

        
        // attack range
        if (inRange(distX, distY) && once)
        {
            once = false;
            anim.SetBool("inRange", true); // begins the "mino_atk1" animation
        }
        
        
    }


    private bool inSight(float dx, float dy)
    {
        return (dx < sightRange && dy < sightRange);
    }

    private bool inRange(float dx, float dy)
    {
        return (dx < atkRange && dy < atkRange);
    }


    private void Attack() 
    {
        // triggered from animation
        var hits = Physics2D.OverlapCircleAll(hitboxPos, hitboxSize, playerLayer);
        foreach (var col in hits)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                //Debug.Log("Player hit!");
                col.gameObject.GetComponent<PlayerManager>().pHealth.Damage(assignedPlayerDamage);
            }
        }

    }

    private void Death() 
    {
        // triggered after death animation
        anim.enabled = false;
        gameObject.GetComponent<mino2>().enabled = false;

    }
    
    
    // private void OnDrawGizmos()
    // {
    //     // attack range
    //     Gizmos.color = Color.green;
    //     Gizmos.DrawWireSphere(transform.position, atkRange);
    //     // sight range
    //     Gizmos.color = Color.white;
    //     Gizmos.DrawWireSphere(transform.position, sightRange);

    //     // axe hitbox
    //     Gizmos.color = Color.red;
    //     Gizmos.DrawWireSphere(hitboxPos, hitboxSize);
    
    // }
    

    
    
}
