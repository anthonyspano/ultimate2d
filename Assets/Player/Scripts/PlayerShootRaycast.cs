using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootRaycast : MonoBehaviour
{
    private Camera cam;
    public LayerMask enemyLayerMask;     // layer mask of enemy (9)
    
    private UltimateBar ultBar;
    public int ultChargeAmt;
    
    void Start()
    {
        //Physics2D.queriesStartInColliders = false;
        //ultBar = GameObject.Find("UltBar").GetComponent<UltimateBar>();
    }

    void RaycastShoot()		
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
          RaycastHit2D hit = Physics2D.Raycast(transform.position, click, distance, enemyLayerMask);
          
          if (hit.collider)
          {
           //hit.collider.gameObject.GetComponent<EnemyManager>().hSystem.Damage(20); // change for boss (tbi)
           ultBar.AddUlt(ultChargeAmt);
          }
    
      }
}
