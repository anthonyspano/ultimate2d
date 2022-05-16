using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDash : MonoBehaviour
{
    [SerializeField] private float dashDistance; // 600

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis(InputAxis.x);
        var y = Input.GetAxis(InputAxis.y);
        var direction = new Vector2(x, y);
        
        if (Input.GetKeyDown(InputAxis.jumpKey)) 
        {
            Debug.Log("Dash!");
            transform.Translate(direction * (dashDistance * Time.deltaTime));
        }
    }
}
