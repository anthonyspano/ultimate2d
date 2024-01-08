using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class FirstAttack : State
    {
        private bool continueChain = false;
        private PlayerBattleSystem pbs;

        public FirstAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
        }
        public override IEnumerator Start() 
        {
            PlayerManager.Instance.CanMove = false;
            // play slash anim
            //if(!PlayerManager.Instance.GetComponent<AudioSource>().isPlaying)
                PlayerManager.Instance.GetComponent<AudioSource>().Play();
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", true);

            // scoot towards last move
            var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            //Debug.Log(PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance);
            PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);

            while(PlayerManager.Instance.GetComponent<Animator>().GetBool("IsAttacking") == true)
            {
                if(PlayerInput.Slash()) 
                    continueChain = true;
                yield return null;
            }
            //yield return new WaitForSeconds(0.117f); // current length of all attack anims
            //yield return new WaitForSeconds(PlayerManager.Instance.cooldownRate); // arbitrary wait to read player input
   
            // if(continueChain)
            // {
            //     continueChain = false;
            //     yield return null;
            //     PlayerBattleSystem.SetState(new SecondAttack(PlayerBattleSystem));
            // }

            PlayerManager.Instance.isBusy = false;
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            // time waiting until player can attack again after combo over
            PlayerBattleSystem.SetState(new Begin(pbs));
            //}
                
                




        }
    }
}