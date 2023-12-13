using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WaveManager : MonoBehaviour
{
    public GameObject DoorOpenPrompt;

    public GameObject[] Rooms;
    public GameObject[] Doors;
    
    public int currentRoom; 
    private int aliveEnemies;

    public Text youWinText;

    private void Start()
    {
        currentRoom = 0;
    }

    void Update()
    {

        try
        {
            aliveEnemies = Rooms[currentRoom].gameObject.transform.childCount;
        }
        catch (Exception e)
        {
            Debug.Log("index out of bounds dummy");

        }


        if(aliveEnemies == 0)
        {  
            if(currentRoom < 2)
            {
                // unlock door to next room
                Doors[currentRoom].transform.GetChild(0).gameObject.SetActive(true);
                DoorOpenPrompt.gameObject.SetActive(true);

                currentRoom++;
            }
            else
            {
                // you win screen
                youWinText.gameObject.SetActive(true);

                // record time on clock
                string time = GameObject.Find("GameManager").GetComponent<GameManager>().timerText.text;
                youWinText.text += "\n" + time;

                Destroy(this);
            }

        }


    }
}
