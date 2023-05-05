using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float coolDownRate;
    private float coolDownTimer;
    public float jumpDistance;
    private Animator anim;

    void Start() 
    {
        coolDownTimer = coolDownRate;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(PlayerInput.Jump() && coolDownTimer <= 0)
        {
            // perform jump
            var x = Input.GetAxis(PlayerInput.x);
            var y = Input.GetAxis(PlayerInput.y);
            var direction = new Vector2(x, y);

            Debug.Log("Jumping");
            StartCoroutine("PerformJump", transform.XandY() + direction * jumpDistance);

            coolDownTimer = coolDownRate;
        }   

        coolDownTimer -= Time.deltaTime;     
    }

    IEnumerator PerformJump(Vector2 target)
    {
        // while the absolute value of the distance between the two points is less than x
        while(Vector2.Distance(transform.XandY(), target) > 0.3f)
        {
            Debug.Log(transform.position);
            Debug.Log(target);
            transform.position = Vector2.Lerp(transform.position, target, 0.5f); 
            yield return null;

        }

    }
    
}
