using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SecondAttack : State
    {
        private bool continueChain;
        private PlayerBattleSystem pbs;


        public SecondAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = PlayerBattleSystem;
        }

        public override IEnumerator Start() 
        {
            Debug.Log("Starting second attack");
            // play second slash anim
            PlayerManager.Instance.GetComponent<Animator>().Play("Attack Tree", 0);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();

            // scoot towards last move
            // var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            // PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);
            
            var cooldown = PlayerManager.Instance.cooldownRate;
            while(cooldown > 0)
            {
                if(PlayerInput.Slash()) continueChain = true;
                cooldown -= Time.deltaTime;
                yield return null;
            }

            // if(continueChain)
            // {
            //     continueChain = false;
            //     PlayerBattleSystem.SetState(new ThirdAttack(PlayerBattleSystem));
            // }
            // else
            // {
                PlayerManager.Instance.CanMove = true;
                PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
            //}    




        }


    }
}