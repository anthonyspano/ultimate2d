using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    private Vector2 landingSpot;
    
    private void Start()
    {
        landingSpot = GameObject.Find("RockShadow(Clone)").transform.position;
        
    }

    private void Update()
    {
        //landingSpot = GameObject.Find("RockShadow(Clone)").transform.position;
        transform.position = Vector2.MoveTowards(transform.position, landingSpot, 0.2f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.tag == "RockShadow")
        {
            
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
