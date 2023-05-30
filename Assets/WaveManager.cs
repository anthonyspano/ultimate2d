using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject DoorOpenPrompt;

    public GameObject[] Rooms;
    public GameObject[] Doors;
    
    private int currentRoom; 
    private int aliveEnemies;


    private void Start()
    {
        currentRoom = 0;
    }

    void Update()
    {
        //GameObject[] aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        aliveEnemies = Rooms[currentRoom].gameObject.transform.childCount;
        
        if(aliveEnemies == 0)
        {  
            // unlock door to next room
            Doors[currentRoom].transform.GetChild(0).gameObject.SetActive(true);
            DoorOpenPrompt.gameObject.SetActive(true);

            currentRoom++;

        }

        if(currentRoom == 4)
        {
            // you win screen
            
        }
    }
}
