﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateDash : MonoBehaviour
{
    public float dashDistance; // 12 seems good
    public Transform shadowPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Dash(dashDistance);
    }

    void Dash(float speed)
    {
        // create "shadow" of where hitbox is - determine area where hitbox is at this moment
        // instantiate clone object with only boxcollider2d with the same parameters as this object
        Instantiate(shadowPrefab, transform.position, Quaternion.identity);

        // dash
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(direction * dashDistance * Time.deltaTime);
    }
}
