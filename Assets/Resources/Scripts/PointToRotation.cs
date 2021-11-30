using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
  0 = (0,1)
 90 = (-1,0)
180 = (0,1)
270 = (1,0)
 
45 = (-0.7, 0.7) 
 
 
 */

public class PointToRotation : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis(PlayerInput.x);
        float y = Input.GetAxis(PlayerInput.y);
        Vector2 movement = new Vector2(x, y);
        movement.Normalize();

        Vector3 moveDirection = movement;
        if (moveDirection != Vector3.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) *
                          Mathf.Rad2Deg;     // return angle in radians tan(y/x)
            
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }



    }

}
