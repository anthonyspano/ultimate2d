using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// change clothes
public class TriggerEvent : MonoBehaviour
{
    public event EventHandler ChangeClothes;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
            // trigger event
            if(ChangeClothes != null) ChangeClothes(this, EventArgs.Empty);
        }
        
    }
}
