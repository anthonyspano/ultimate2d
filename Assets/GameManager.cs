using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int[] values;
    private bool[] keys;

    public static KeyCode playerInput;

    void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];

    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        for(int i=0; i<values.Length; i++)
        {
            keys[i] = Input.GetKey((KeyCode)values[i]);
            
            if(keys[i])
            {
                playerInput = (KeyCode)values[i];
                //Debug.Log(playerInput);
            }
        }        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
    }
}
