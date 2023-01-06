using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SimpleAI : MonoBehaviour
{
    GameObject player;

    public float mDD;

    public Transform bulletPrefab;

    Transform temp;

    private float coolDown;
    public float attackRate;

    // Start is called before the first frame update
    void Start()
    {
        // find the main player 
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        if (coolDown <= 0)
        {
            coolDown = attackRate;
            Attack();
        }

        // move to the main player
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, mDD);
    }


    void Attack()
    {


        // Create a bullet in the scene
        Instantiate(bulletPrefab, transform.position, transform.rotation);

    }




}
