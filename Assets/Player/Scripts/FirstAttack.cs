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
            PlayerManager.Instance.GetComponent<AudioSource>().Play();
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", true);

            // scoot towards last move
            //var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            //PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);

            yield return new WaitForSeconds(0.117f); // current length of all attack anims
            //yield return new WaitForSeconds(PlayerManager.Instance.cooldownRate); // arbitrary wait to read player input
            //yield return new WaitForSeconds(PlayerManager.Instance.cooldownRate); 
            
            if(PlayerInput.Slash()) continueChain = true;
            
            if(continueChain)
            {
                continueChain = false;
                BlockBattleSystem.SetState(new SecondAttack(PlayerBattleSystem));
            }
            else
            {
                
            PlayerManager.Instance.CanMove = true;

            // time waiting until player can attack again after combo over
            yield return new WaitForSeconds(PlayerManager.Instance.cooldownRate);
            PlayerBattleSystem.SetState(new Begin(pbs));
            //}
                
                




        }
    }
}