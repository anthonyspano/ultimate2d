using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.CompareTag("BigCultist"))  
        {
            // parent take damage
            PlayerManager.Instance.pHealth.Damage(20);

        }
    }
}
