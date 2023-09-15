using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveTrigger : MonoBehaviour
{
    public void StartShockwave()
    {
        var shockwavePrefab = Resources.Load("Shockwave") as GameObject;
        Vector3 spawnPosition = transform.position - transform.up;
        var shockwave = Object.Instantiate(shockwavePrefab, (gameObject.GetComponent<SpriteRenderer>().flipX) ? spawnPosition + transform.right : spawnPosition - transform.right, Quaternion.identity);

    }
}
