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
            PlayerManager.Instance.GetComponent<Animator>().SetBool("SecondAttack", true);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();

            // scoot towards last move
            // var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            // PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);
            
            // var cooldown = PlayerManager.Instance.cooldownRate;
            // while(cooldown > 0)
            // {
            //     if(PlayerInput.Slash()) continueChain = true;
            //     cooldown -= Time.deltaTime;
            //     yield return null;
            // }
            yield return new WaitForSeconds(0.117f); // current length of all attack anims
            // if(continueChain)
            // {
            //     continueChain = false;
            //     PlayerBattleSystem.SetState(new ThirdAttack(PlayerBattleSystem));
            // }
            // else
            // {
            PlayerManager.Instance.CanMove = true;
            yield return new WaitForSeconds(PlayerManager.Instance.cooldownRate); 
            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
            //}    




        }


    }
}