using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int[] values;
    private bool[] keys;

    public static KeyCode playerInput;

    public Text timerText;

    void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];

        timerText.text = "0";
    }

    void Start()
    {
        Application.targetFrameRate = 60;

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
                //Debug.Log((int)playerInput);
            }
        }  

        timerText.text = Time.timeSinceLevelLoad.ToString();      
    }

    public void GameStart()
    {
        SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
    }
}
