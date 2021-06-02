using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateDash : MonoBehaviour
{
	// speed that determines slow enough to move back instead of jumping forward
	public float idleBuffer; // (0.2f) if close to not moving, dash backwards 
	public float dashDistance; // 12 seems good
    public Transform shadowPrefab;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Dash(dashDistance);
    }

    protected virtual void Dash(float speed)
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");
        var direction = new Vector2(x, y);
        if (x < idleBuffer && y < idleBuffer)
	        direction = Vector2.left;
        transform.Translate(direction * (dashDistance * Time.deltaTime));
    }
    
}
