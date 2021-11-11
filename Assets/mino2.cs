using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// print when the code steps through each point and make sure correct

public class mino2 : MonoBehaviour
{
    private Animator anim;
    private GameObject player;

    private float distX;
    private float distY;

    [SerializeField]
    private float sightRange;
    [SerializeField]
    private float atkRange;
    [SerializeField]
    private float runSpeed;

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
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");

        // health
        healthSystem = new HealthSystem(200, 0f);
        healthBar.Setup(healthSystem);
        // health - death event
        healthSystem.OnHealthChanged += OnDeath;
    }

    private void OnDeath(object sender, System.EventArgs e)
    {
        if(healthSystem.GetHealth() <= 0) 
        {
            // Death sequence   
        }
    }

    private void Update()
    {
        hitboxPos = GameObject.Find("AxeHitBox").transform.position; 
        var playerPos = player.transform.position;
        distX = Math.Abs(transform.position.x - playerPos.x);
        distY = Math.Abs(transform.position.y - playerPos.y);
        
        // sight range 
        if (inSight(distX, distY) && !inRange(distX, distY))
        {  
            anim.SetBool("inSight", true);
            // probably move towards playerpos but set value to distance traveled in one frame
            // towards: (playerPos - transform.position).normalize
            tPos = transform.position + ((playerPos - transform.position).normalized);
            transform.position = Vector2.MoveTowards(transform.position, tPos, runSpeed * Time.deltaTime);
        }

        if(inRange(distX, distY) || !isSwinging)
        {

            //Debug.Log("attacking player");

        }

        
        // attack range
        if (inRange(distX, distY) && once)
        {
            once = false;
            Debug.Log("once");
            //isSwinging = true;
            anim.SetBool("inRange", true);
            //swing
            StartCoroutine("Attack");
        }
        
        
    }

    private IEnumerator Attack()
    {
        //isSwinging = false;
        yield return new WaitForSeconds(1f);
        // trigger hitbox
        var hits = Physics2D.OverlapCircleAll(hitboxPos, hitboxSize, playerLayer);
        foreach (var col in hits)
        {
            if(col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Player hit!");
                col.gameObject.GetComponent<PlayerManager>().pHealth.Damage(20);
            }
        }

        once = true;
    }


    private bool inSight(float dx, float dy)
    {
        return (dx < sightRange && dy < sightRange);
    }

    private bool inRange(float dx, float dy)
    {
        return (dx < atkRange && dy < atkRange);
    }



    
    
    private void OnDrawGizmos()
    {
        // attack range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, atkRange);
        // sight range
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        // axe hitbox
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitboxPos, hitboxSize);
    
    }
    
    
    
}
