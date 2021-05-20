using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static EnemyController;

public class Shoot : MonoBehaviour
{
    Rigidbody2D rb2D;
    private Camera cam;
    public int validTarget;     // for player - layermask of enemy
	public Transform bulletPrefab;
	private Vector2 direction;
	Vector2 playerPos;
	public float bulletSpeed; // 40
	public Transform bulletSpawn;
	
	public float time;
	float timer;
	
	// enemy controller
	CurrentState eState;
	EnemyController eController;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rb2D = GetComponent<Rigidbody2D>();
        //cam = Camera.main; // moved to fire()
		
		timer = time;
		
		// enemy controller
		eController = GetComponent<EnemyController>();
	}

    private void FixedUpdate()
    {
		if (gameObject.tag == "Player")
		{
			// 1 - right click
			if (Input.GetMouseButtonDown(1))
			{
				Fire();
			}
		}
		
		// enemy trigger
		if (timer > 0)
			timer -= Time.deltaTime;
		else
		{
			timer = time;
			if (gameObject.tag != "Player")
				FireProjectile();
			
		}
		
		
    }

    void Fire()		// for player
    {
		cam = Camera.main;
        // Get mouse position in game world
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);

        // get distance for raycast d = sqrt(pow((x2 - x1), 2), pow((y2 - y1), 2))
        float x1 = gameObject.transform.position.x;
        float y1 = gameObject.transform.position.y;
        float x2 = mouseWorldPoint.x;
        float y2 = mouseWorldPoint.y;

        float distance = Mathf.Sqrt((Mathf.Pow((x2 - x1), 2) + Mathf.Pow((y2 - y1), 2)));

        
        Vector3 click = mouseWorldPoint - transform.position;
        Debug.DrawLine(transform.position, mouseWorldPoint, Color.red);
        // cast from player pos to mouse pos 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, click, distance, ~validTarget);


        // on hit - iterates 4 times
        if (hit.collider != null)
        {
            Debug.Log("Hit");

            // TBI: Health Implementation
            hit.collider.gameObject.GetComponentInChildren<EnemyController>().hBar.healthSystem.Damage(20);
            Debug.Log(hit.collider.gameObject.tag);
            

        }

    }
	
	public void FireProjectile()		// for enemy
	{
		// reduce duplicates of initiating shooting protocol
		//eState = CurrentState.Standing;
		//eController.setState(eState);
		
		// locate player
		playerPos = GameObject.Find("Player").transform.position;
		// spawn bullet
		Transform bulletClone = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
		// gain invulnerability from bullet
		Physics2D.IgnoreCollision(bulletClone.GetComponent<BoxCollider2D>(), GetComponent<BoxCollider2D>());		
		// add force
		Rigidbody2D bulletRB = bulletClone.GetComponent<Rigidbody2D>();
		Vector3 t = transform.position;
		Vector2 traj = playerPos - (Vector2)transform.position; 
		bulletRB.AddForce(traj * bulletSpeed);
		
		// let enemy transition to next state
		//eController.setState(CurrentState.Moving);
	}
	
}
