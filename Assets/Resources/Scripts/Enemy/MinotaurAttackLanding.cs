using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurAttackLanding : MonoBehaviour
{
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void AttackLands()
    {
        Collider2D[] player = Physics2D.OverlapCircleAll(transform.position, MinotaurAttr.hitRange, EnemyManager.enemyLayerMask);
        if(player.Length > 0)
            if (player[0].name == "Player")
            {
                PlayerManager.Instance.pHealth.Damage(MinotaurAttr.damage);
            }

        Destroy(GameObject.Find("AttackBoxIndication(Clone)"));
        EnemyManager.Busy = false;

        _animator.Play("Idle");
        
        // camera shake
        var cam = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        float distance = Vector2.Distance(transform.position, PlayerManager.player.transform.position);
        // base shake + (inverse of percentage of max distance) * max shake
        float shakeIntensity = 0.06f + (1 - distance / 30) * 0.25f;
        //cam.TriggerShake(shakeIntensity);
        
        // Start event where rocks fall from ceiling

        
        // allow minotaur to move
        GetComponent<EnemyManager>().CanMove = true;
    }
}
