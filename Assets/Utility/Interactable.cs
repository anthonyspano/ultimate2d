using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{

    public Object icon;
    private Object prompt;
    bool once;

    protected void OnCollisionStay2D(Collision2D col)
    {
        // prompt comes up
        // generate image
        if(!once)
        {
            once = true;
            prompt = GameObject.Instantiate(icon, transform.position + new Vector3(1,1,50), Quaternion.identity);
        }

        if(PlayerInput.Interact())
        {

            // remove prompt
            once = false;
            Object.Destroy(prompt);
           
            Trigger();
        }

    }

    void OnCollisionExit2D(Collision2D col)
    {
        Destroy(prompt);
        once = false;
    }

    protected virtual void Trigger(){}
}
