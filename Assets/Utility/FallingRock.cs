using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    // set gravity scale in the prefab inspector (4)
    private void Start()
    {
        transform.parent = null;
        GetComponent<Rigidbody2D>().gravityScale = 4;
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("RockShadow"))
        {
            Destroy(col.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
