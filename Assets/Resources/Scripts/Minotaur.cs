using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : MonoBehaviour
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

    private bool isSwinging;
    
    private void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        var playerPos = player.transform.position;
        //Debug.Log(playerPos);
        distX = transform.position.x - playerPos.x;
        distY = transform.position.y - playerPos.y;
        //Debug.Log(inSight(distX, distY));
        // sight range 
        if (inSight(distX, distY))
        {  
            //anim.SetBool("inSight", true);
            // pursue player 
            if(!inRange(distX, distY))// || !isSwinging)
            {
                // move towards player
                Debug.Log(playerPos);
                transform.Translate(playerPos * Time.deltaTime * runSpeed);
                
            }
            
        }
        else
        {
            //anim.SetBool("inSight", false);
        }
        
        // attack range
        if (inRange(distX, distY))
        {
            isSwinging = true;
            //anim.SetBool("inRange", true);
            // swing
            //StartCoroutine("Attack");
        }
        
        
    }

    private IEnumerator Attack()
    {
        isSwinging = false;
        yield return new WaitForSeconds(2f);

        //Debug.Log("Attack finished");
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
    
    }
    
    
    
}
