using System.Collections;
using System.Collections.Generic;
using com.ultimate2d.combat;
using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    // change the color of the editor in play mode
    private void OnTriggerStay2D(Collider2D col)
    {
        col.gameObject.GetComponentInChildren<BossTakeDamage>().healthSystem.Damage(5);
    }
}
