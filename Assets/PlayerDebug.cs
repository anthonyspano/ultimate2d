using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDebug : MonoBehaviour
{


    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(PlayerManager.Instance.playerAim.transform.position,
                              PlayerManager.Instance.range);
    }
}
