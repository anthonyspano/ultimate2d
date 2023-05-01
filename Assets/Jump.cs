using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float coolDownRate;
    private float coolDownTimer;
    public float jumpDistance;

    void Start() 
    {
        coolDownTimer = coolDownRate;
    }

    void Update()
    {
        if(PlayerInput.Jump() && coolDownTimer <= 0)
        {
            // perform jump
            var x = Input.GetAxis(PlayerInput.x);
            var y = Input.GetAxis(PlayerInput.y);
            var direction = new Vector2(x, y);

            transform.position = Vector2.Lerp(transform.position, transform.XandY() + direction * jumpDistance, 0.3f); // jump speed

            coolDownTimer = coolDownRate;
        }   

        coolDownTimer -= Time.deltaTime;     
    }

    
}
