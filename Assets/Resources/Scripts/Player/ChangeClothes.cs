using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ChangeClothes : MonoBehaviour
{
    private TriggerEvent te;
    public float waittime;
    private void Awake()
    {
        te.ChangeClothes += ChangeClothes_Event;
    }

    private void ChangeClothes_Event(object sender, System.EventArgs e)
    {
        Debug.Log("changing");
        StartCoroutine(ChangeClothes_Enum());
    }

    private IEnumerator ChangeClothes_Enum()
    {
        Debug.Log("doing the change");
        // disable player movement/attacks
        var scriptComponents = PlayerManager.player.GetComponents<MonoBehaviour>();
        foreach (var s in scriptComponents)
        {
            s.enabled = false;
        }
        
        // play animation
        
        
        yield return new WaitForSeconds(waittime);
        
        // equip new clothes
        
    }
}
