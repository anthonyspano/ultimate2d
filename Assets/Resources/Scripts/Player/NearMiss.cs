using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NearMiss : TranslateDash
{
    protected override void Dash(float speed)
    {
        // create "shadow" of where hitbox is - determine area where hitbox is at this moment
        // instantiate clone object with only boxcollider2d with the same parameters as this object
        var shadowClone = Instantiate(shadowPrefab, transform.position, Quaternion.identity);
        
        // coroutine for shadow
        StartCoroutine(Shadow(shadowClone));
    
        base.Dash(speed);
    }
    
    IEnumerator Shadow(Transform clone)
    {
        yield return new WaitForSeconds(2);
        Destroy(clone.gameObject);
    }
}
