using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionStay2D(Collision2D col)
    {

        if(col.gameObject.CompareTag("Player") && PlayerInput.Interact())
        {
            audioSource.Play();

            var doorExit = transform.GetChild(0);
            PlayerManager.Instance.transform.position = doorExit.position;
        }
    }
}
