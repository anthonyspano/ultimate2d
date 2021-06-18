﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tbi: enhanced strike
[RequireComponent(typeof(PlayerAttack))]
public class TranslateDash : MonoBehaviour
{
	// speed that determines slow enough to move back instead of jumping forward
	public float idleBuffer; // (0.2f) if close to not moving, dash backwards 
	public float dashDistance; // 12 seems good
    public Transform shadowPrefab;
    
    // cooldown
    private int consecutiveDashes = 0;
    public int maxConsecutiveDashes = 2;
    private float cooldownTimer = 0;
    [SerializeField] private float cooldownRate;
    
    // attack on second dash
    private PlayerAttack pAtk;

    private void Start()
    {
        pAtk = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(InputAxis.jumpKey) && cooldownTimer <= 0)
        {
            // increment to max dashes
            consecutiveDashes++;
            
            
            // dash
            Dash(dashDistance);
            
            // second strike
            if (consecutiveDashes >= maxConsecutiveDashes)
            {
                // strike
                pAtk.Strike("strike", 50); // tbi: enhanced strike
                
                // reset
                consecutiveDashes = 0;
                cooldownTimer = cooldownRate;
            }
        }

        cooldownTimer -= Time.deltaTime;
    }

    protected virtual void Dash(float speed)
    {
        var x = Input.GetAxis(InputAxis.x);
        var y = Input.GetAxis(InputAxis.y);
        var direction = new Vector2(x, y);
        // if (x < idleBuffer && y < idleBuffer)
	       //  direction = Vector2.left;
        transform.Translate(direction * (dashDistance * Time.deltaTime));
    }
    
}
