using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// maybe use this for roll
public class NormalizedTest : MonoBehaviour
{
    void Update()
    {
        Debug.Log(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized);       
    }
}
