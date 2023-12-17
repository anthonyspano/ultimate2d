using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{

    private float x;
    private float y;

    private Vector2 lastMove;
    public int Z_offset;
    public float cursorDistanceFromPlayer;
    
    private void Update()
    {
        x = Input.GetAxis(PlayerInput.x);
        y = Input.GetAxis(PlayerInput.y);
        Vector3 moveDirection = new Vector3(x, y);
        if (moveDirection.x != 0 || moveDirection.y != 0)
        {
            moveDirection.Normalize();
            
            // pos
            var pos = PlayerManager.Instance.transform.XandY();
            pos += (Vector2)moveDirection * cursorDistanceFromPlayer; // 1 is good for final
            transform.position = pos;

            // rotate
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) *
                          Mathf.Rad2Deg;     // return angle in radians tan(y/x)
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward - new Vector3(0,0, Z_offset)); // needs to be flipped
            
        }
    }

} 
