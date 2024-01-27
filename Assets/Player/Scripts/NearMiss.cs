using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;

namespace com.ultimate2d.combat
{
public class NearMiss : MonoBehaviour
{
    public Transform shadowPrefab;
    private float cooldownTimer;
    private float cooldownRate;

    private void Start()
    {
        cooldownRate = PlayerManager.Instance.jumpCooldownRate;
    }


    private void Update()
    {
        if (PlayerInput.Jump() && cooldownTimer <= 0)
        {
            
            PlayerManager.Instance.CanMove = false;
            cooldownTimer = cooldownRate;
            CreateClone();
            // Dash
            StartCoroutine("Dash");
            
        }

        cooldownTimer -= Time.deltaTime;
    }

    public void CreateClone()
    {
        // create "shadow" of where hitbox is - determine area where hitbox is at this moment
        // instantiate clone object with only boxcollider2d with the same parameters as this object
        var shadowClone = Instantiate(shadowPrefab, transform.position, Quaternion.identity);
        
        // coroutine for shadow
        StartCoroutine(DestroyShadow(shadowClone));
        
    }

    public IEnumerator Dash()
    {
        //Debug.Log("dashing");
        GetComponent<Animator>().SetBool("isJumping", true);
        

        // calculate where I'm supposed to land first
        // perform raycast
        RaycastHit2D hit = Physics2D.Raycast(PlayerManager.Instance.transform.position, PlayerManager.Instance.LastMove, 0.5f);
        // set final position where the outer bound of the hit collider is
        Vector2 finalSpace;
        if(hit.collider.name == "Wall")
        {
            finalSpace = hit.point;
        }      
        else
        {
            finalSpace = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.JumpDistance;
        }
        
        yield return new WaitUntil(() => GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"));

        // move player
        while(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
        {
            // Debug.Log(PlayerManager.Instance.transform.position);
            // Debug.Log("final space: " + finalSpace);
            PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, finalSpace, PlayerManager.Instance.JumpSpeed * Time.deltaTime);
            yield return null;
        }
        
    }
    
    IEnumerator DestroyShadow(Transform clone)
    {
        yield return new WaitForSeconds(2);
        Destroy(clone.gameObject);
    }
}

}