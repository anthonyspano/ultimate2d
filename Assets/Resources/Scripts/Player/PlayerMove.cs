using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float x;
    float y;
    [SerializeField]
    private float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        Movement();
    }

    void Movement()
    {
        if(Input.GetAxis("Horizontal") != 0)
        {
            transform.Translate(Vector3.right * (x * Time.deltaTime * moveSpeed));
        }

        if(Input.GetAxis("Vertical") != 0)
        {
            transform.Translate(Vector3.up * (y * Time.deltaTime * moveSpeed));
        }
        
    }
}
