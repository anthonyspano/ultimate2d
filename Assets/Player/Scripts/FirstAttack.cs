using System.Collections;
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
            PlayerManager.Instance.GetComponent<Animator>().Play("Attack Tree", 0);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();

            // scoot towards last move
            //var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            //PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);

            //yield return new WaitForSeconds(0.117f); // current length of all attack anims
            yield return new WaitForSeconds(PlayerManager.Instance.cooldownRate); // arbitrary wait to read player input
            
            if(PlayerInput.Slash()) continueChain = true;
            
            // if(continueChain)
            // {
            //     continueChain = false;
            //     BattleSystem.SetState(new SecondAttack(BattleSystem));
            // }
            // else
            // {
                
                PlayerManager.Instance.CanMove = true;
                BattleSystem.SetState(new Begin(BattleSystem));
            //}
                
                




        }
    }
}