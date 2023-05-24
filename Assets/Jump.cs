using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float coolDownRate;
    private float coolDownTimer;
    public float jumpDistance;
    private Animator anim;
    private Rigidbody2D rb;

    void Start() 
    {
        coolDownTimer = coolDownRate;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(PlayerInput.Jump() && coolDownTimer <= 0)
        {
            // perform jump
            var x = Input.GetAxis(PlayerInput.x);
            var y = Input.GetAxis(PlayerInput.y);
            var direction = new Vector2(x, y);

            Debug.Log("Jumping");
            rb.AddForce(direction * jumpDistance);

            coolDownTimer = coolDownRate;
        }   

        coolDownTimer -= Time.deltaTime;     
    }

    
}
