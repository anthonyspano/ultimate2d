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

    private void FixedUpdate()
    {
        // if(Input.GetKeyDown(KeyCode.Escape))
        //     Application.Quit();
        
        float x = Input.GetAxis(PlayerInput.x);
        float y = Input.GetAxis(PlayerInput.y);
        var movement = new Vector2(x, y);
        
        // if (x < 0)
        // {
        //     isFacingLeft = true;
        // }
        // else if (x > 0)
        // {
        //     isFacingLeft = false;
        // }
        //sr.flipX = isFacingLeft;
       
        
        if (movement.magnitude > deadZone && PlayerManager.Instance.CanMove) 
        {
            var direction = new Vector3(movement.x, movement.y, 0) + transform.position;
            transform.position = Vector2.MoveTowards(transform.position, direction, moveSpeed * Time.deltaTime);
            anim.SetBool("isMoving", true);
            
        }
        else anim.SetBool("isMoving", false);
        
        anim.SetFloat("MoveX", x);
        anim.SetFloat("MoveY", y);
    }

 

  
}
