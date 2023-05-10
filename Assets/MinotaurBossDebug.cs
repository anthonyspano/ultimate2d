using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurBossDebug : MonoBehaviour
{
    public Transform AxeHitBox;
    public float radius;
    public float sightRange;
    public float attackRange;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AxeHitBox.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sightRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);


    }
}
