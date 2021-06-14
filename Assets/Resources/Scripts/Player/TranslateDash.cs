﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateDash : MonoBehaviour
{
	// speed that determines slow enough to move back instead of jumping forward
	public float idleBuffer; // (0.2f) if close to not moving, dash backwards 
	public float dashDistance; // 12 seems good
    public Transform shadowPrefab;
    
    // cooldown
    private float cooldownTimer = 0;
    [SerializeField] private float cooldownRate;
    
    private void Update()
    {
        if (cooldownTimer <= 0 && Input.GetAxis(InputAxis.jump) > 0)
        {
            cooldownTimer = cooldownRate;
            Dash(dashDistance);
        }

        cooldownTimer -= Time.deltaTime;
    }

    protected virtual void Dash(float speed)
    {
        var x = Input.GetAxis(InputAxis.x);
        var y = Input.GetAxis(InputAxis.y);
        var direction = new Vector2(x, y);
        if (x < idleBuffer && y < idleBuffer)
	        direction = Vector2.left;
        transform.Translate(direction * (dashDistance * Time.deltaTime));
    }
    
}
