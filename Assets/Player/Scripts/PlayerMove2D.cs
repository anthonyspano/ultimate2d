using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerMove2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    // animation
    private SpriteRenderer sr;
    private Animator anim;
    private Rigidbody2D rb;
    public bool isFacingLeft;

    public float deadZone; // 0.1
    public float jumpIntensity; // 4

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        
        float x = Input.GetAxis(PlayerInput.x);

        if (x < 0)
            isFacingLeft = true;
        else if (x > 0)
            isFacingLeft = false;
        sr.flipX = isFacingLeft;
        
        if (x > deadZone || x < -deadZone) // if (movement.magnitude > 0)
        {
            var direction = new Vector3(x, 0, 0) + transform.position;
            transform.position = Vector2.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
            anim.SetBool("isMoving", true);
        }
        else anim.SetBool("isMoving", false);
        
        anim.SetFloat("MoveX", x);
        
        
        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpIntensity, ForceMode2D.Impulse);
        }
        
        // dash
        if (Input.GetKeyDown(KeyCode.J))
        {
            var dir = (isFacingLeft) ? Vector2.left : Vector2.right;
            rb.AddForce(dir * (jumpIntensity/2), ForceMode2D.Impulse);
        }
        
        // attack
        
    }

 

  
}