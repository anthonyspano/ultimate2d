using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private bool once;

    public GameObject DoorOpenPrompt;

    void Start()
    {

    }

    void Update()
    {
        GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if(aliveEnemies.GetLength(0) <= 0)
        {
            
            // unlock door to next room
            if(once == false)
            {
                once = true;
                Debug.Log("prompt!");
                var door = GameObject.Find("Door2");
                door.transform.GetChild(0).gameObject.SetActive(true);
                DoorOpenPrompt.gameObject.SetActive(true);

            }
        }
    }
}
