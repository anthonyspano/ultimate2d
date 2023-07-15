using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class SecondAttack : State
    {
        private bool continueChain;
        private BattleSystem bs;


        public SecondAttack(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }
        public override IEnumerator Start() 
        {
            // wait until animation is on second to last frame and use event to trigger bool?
            // or wait until animation is finished?
            Debug.Log(PlayerManager.Instance.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            yield return new WaitUntil(() => PlayerManager.Instance.AnimFinished());
            
            Debug.Log("preparing second attack");

            // play second slash anim
            //Debug.Log("Second attack");
            PlayerManager.Instance.CanMove = false;
            //PlayerManager.Instance.GetComponent<Animator>().Play("PlayerStrike2", 0);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();
            // play slash anim
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", true);

            // scoot towards last move
            var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);
            
            var cooldown = PlayerManager.Instance.cooldownRate;
            while(cooldown > 0)
            {
                if(PlayerInput.Slash()) continueChain = true;
                cooldown -= Time.deltaTime;
                yield return null;
            }

            // turn off attack anim
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", false);

            if(continueChain)
            {
                continueChain = false;
                BattleSystem.SetState(new ThirdAttack(BattleSystem));
            }
            else
            {
                PlayerManager.Instance.CanMove = true;
                BattleSystem.SetState(new Begin(BattleSystem));
            }    




        }


    }
}