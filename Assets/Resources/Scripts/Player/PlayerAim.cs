﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    protected Vector2 lastMove;
    public Vector2 center;

    public float range;

    private float x;
    private float y;
    private void Start()
    {
        lastMove = Vector2.down;
    }

    protected void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        
        if (x != 0 || y != 0)
            lastMove = new Vector2(x, y);
        
        center = transform.XandY() + lastMove.normalized * range;
    }
}