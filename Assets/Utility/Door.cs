using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public AudioSource audioSource;
    protected override void Trigger()
    {
        audioSource.Play();

        var doorExit = transform.GetChild(0);
        PlayerManager.Instance.transform.position = doorExit.position;

    }
}
