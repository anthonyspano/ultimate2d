using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{

    private float x;
    private float y;

    private Vector2 lastMove;

    public float radius; // 2.5
    
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        Vector3 moveDirection = new Vector3(x, y);
        moveDirection.Normalize();

        if (x != 0 || y != 0)
            lastMove = new Vector2(x, y);
        
        if (lastMove.x != 0 || lastMove.y != 0)
        {
            // pos
            var pos = GameObject.Find("Player").transform.XandY();
            pos += lastMove.normalized * radius;
            transform.position = pos;
            
            // rotate
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) *
                          Mathf.Rad2Deg;     // return angle in radians tan(y/x)
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
        }
    }
}
