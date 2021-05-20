using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RBDash : MonoBehaviour
{
    Rigidbody2D rb;
    public float dashForce; // 1000 force and 100 linear drag

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(dashForce);
        }
    }

    void Dash(float force)
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.AddForce(direction * force);
        Debug.Log("Force added");
    }
}
