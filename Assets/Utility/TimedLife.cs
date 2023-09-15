using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Timer(timer));
    }

    IEnumerator Timer(float t)
    {
        yield return new WaitForSeconds(t);

        Destroy(gameObject);
    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     Destroy(gameObject);
    // }

}
