using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }        
    }

    public void GameStart()
    {
        SceneManager.LoadScene("CellChamber", LoadSceneMode.Single);
    }
}
