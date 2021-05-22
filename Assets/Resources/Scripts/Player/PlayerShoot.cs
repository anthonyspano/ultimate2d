using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
	private Camera cam;
    public int enemyLayerMask;     // layer mask of enemy (9)


	// Start is called before the first frame update
    private void Start()
    {
        Physics2D.queriesStartInColliders = false;
    }

    private void FixedUpdate()
    {
	    if (Input.GetButtonDown("Fire2"))
	    {
		    // 1 - right click
		    Fire();
	    }
	    
    }

    void Fire()		// for player
    {
		cam = Camera.main;
        // Get mouse position in game world
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint((Vector2)Input.mousePosition);

        // get distance for raycast d = sqrt(pow((x2 - x1), 2), pow((y2 - y1), 2))
        float x1 = transform.position.x;
        float y1 = transform.position.y;
        float x2 = mouseWorldPoint.x;
        float y2 = mouseWorldPoint.y;

        float distance = Mathf.Sqrt((Mathf.Pow((x2 - x1), 2) + Mathf.Pow((y2 - y1), 2)));

        
        Vector3 click = mouseWorldPoint - transform.position;
        Debug.DrawLine(transform.position, mouseWorldPoint, Color.red);
        // cast from player pos to mouse pos 
        RaycastHit2D hit = Physics2D.Raycast(transform.position, click, distance, ~enemyLayerMask);


        // on hit - iterates 4 times
        if (hit.collider)
        {
            Debug.Log("Hit");
            
            hit.collider.gameObject.GetComponentInChildren<EnemyManager>().hBar.healthSystem.Damage(20);
        }

    }

}
