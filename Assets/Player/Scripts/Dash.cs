using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashDistance; // 600

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis(PlayerInput.x);
        var y = Input.GetAxis(PlayerInput.y);
        var direction = new Vector2(x, y);
        
        if (PlayerInput.Jump())
        {
            GetComponent<Animator>().Play("PlayerDash");
            transform.Translate(direction * (dashDistance * Time.deltaTime));
        }
    }

}
