using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveWrongWay : MonoBehaviour
{
    public GameObject wrongWayPanel;
    void Update()
    {
        StartCoroutine(RemovePanel());
    }

    private IEnumerator RemovePanel()
    {
        yield return new WaitForSeconds(2f);

        wrongWayPanel.SetActive(false);
    }
}
