using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWrongWay : MonoBehaviour
{
    public GameObject wrongWayPanel;
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0) 
            wrongWayPanel.SetActive(false);
    }
}
