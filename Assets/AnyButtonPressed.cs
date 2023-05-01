using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class AnyButtonPressed : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("AnyButton");
    }

    IEnumerator AnyButton()
    {
        
        yield return new WaitUntil(() => Input.anyKey || (Input.GetAxis("Horizontal") != 0) 
                                                      || (Input.GetAxis("Vertical") != 0));
        Debug.Log("input");
        //Debug.Log(Gamepad.current);
        yield return null;

    }
}
