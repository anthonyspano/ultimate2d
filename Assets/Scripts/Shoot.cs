using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    Rigidbody2D rb2D;
    private Camera cam;
    public int validTarget;     // valid target this object will want to hit

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        rb2D = GetComponent<Rigidbody2D>();
        cam = Camera.main;
    }

    private void FixedUpdate()
    {

        // 1 - right click
        if (Input.GetMouseButtonDown(1))
        {
            Fire();
        }
    }

    void Fire()
    {
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
}
