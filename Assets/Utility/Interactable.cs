using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public AudioSource audioSource;
    public Object icon;
    private Object prompt;
    bool once;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionStay2D(Collision2D col)
    {
        // prompt comes up
        // generate image
        if(!once)
        {
            once = true;
            prompt = GameObject.Instantiate(icon, transform.position + new Vector3(1,1,50), Quaternion.identity);
        }

        if(col.gameObject.CompareTag("Player") && PlayerInput.Interact())
        {
            // remove prompt
            once = false;
            Object.Destroy(prompt);

            audioSource.Play();

            var doorExit = transform.GetChild(0);
            PlayerManager.Instance.transform.position = doorExit.position;
        }
    }
}
