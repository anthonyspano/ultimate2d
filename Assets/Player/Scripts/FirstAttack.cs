﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class FirstAttack : State
    {
        private bool continueChain = false;
        private BattleSystem bs;

        public FirstAttack(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }
        public override IEnumerator Start() 
        {
            PlayerManager.Instance.CanMove = false;

            // play slash anim
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", true);
            //PlayerManager.Instance.GetComponent<Animator>().Play("PlayerStrike", 0);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();

            // scoot towards last move
            var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);

            var cooldown = PlayerManager.Instance.cooldownRate;
            while(cooldown > 0)
            {
 
                cooldown -= Time.deltaTime;
                yield return null;
                if(PlayerInput.Slash()) continueChain = true;
            }

            // turn off attack anim
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", false);

            if(continueChain)
            {
                continueChain = false;
                BattleSystem.SetState(new SecondAttack(BattleSystem));
            }
            else
            {
                
                PlayerManager.Instance.CanMove = true;
                BattleSystem.SetState(new Begin(BattleSystem));
            }
                
                




        }
    }
}