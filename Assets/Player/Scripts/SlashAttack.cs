using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class SlashAttack : MonoBehaviour
{
    private float lastMove;
    private GameObject player;

    private SpriteRenderer sr;
    private PlayerMove2D pm2D;
    private void Start()
    {
        player = GameObject.Find("Player");
        sr = GetComponent<SpriteRenderer>();
        pm2D = player.GetComponent<PlayerMove2D>();
    }


    private void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
            lastMove = 1;
        if(Input.GetAxis("Horizontal") < 0)
            lastMove = -1;
        transform.position = player.transform.position + new Vector3(lastMove * player.GetComponent<PlayerAttack2D>().atkRadius, 0, 0);

        sr.flipX = pm2D.isFacingLeft;


    }


    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(
                transform.position + new Vector3(lastMove * player.GetComponent<PlayerAttack2D>().atkRadius, 0, 0),
                .5f);
        }
    }
}
