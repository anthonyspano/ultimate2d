using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    // animation
    private SpriteRenderer sr;
    private Animator anim;
    private bool isFacingLeft;

    public float deadZone;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        var movement = new Vector2(x, y);
        
        if (x < 0)
            isFacingLeft = true;
        else if (x > 0)
            isFacingLeft = false;
        sr.flipX = isFacingLeft;
        
        if (movement.magnitude > deadZone) // if (movement.magnitude > 0)
        {
            movement.Normalize();
            movement *= moveSpeed * Time.deltaTime;
            transform.Translate(movement);
            anim.SetBool("isMoving", true);
        }
        else anim.SetBool("isMoving", false);
        
        anim.SetFloat("MoveX", x);
        anim.SetFloat("MoveY", y);
    }

 

  
}
