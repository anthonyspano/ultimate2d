using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public Transform box;
    void Start()
    {
        Debug.Log("here");
        var b = Instantiate(box, GameObject.Find("Player").transform.position, Quaternion.identity);
    }

}
