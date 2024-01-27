using System.Collections;
using System.Collections.Generic;
using com.ultimate2d.combat;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    // change the color of the editor in play mode
    private void OnTriggerStay2D(Collider2D col)
    {
        try {
        col.gameObject.GetComponentInChildren<BossTakeDamage>().healthSystem.Damage(5);
        } catch (System.Exception) {
            Debug.Log("No boss");
        }
        
        try {
           col.gameObject.GetComponentInChildren<EnemyTakeDamage>().healthSystem.Damage(5);
        } catch (System.Exception) {
            Debug.Log("boss");
        } 
        





    }
}
