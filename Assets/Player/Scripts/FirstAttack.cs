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
            yield return new WaitUntil(() => PlayerInput.Slash());

            PlayerManager.Instance.CanMove = false;

            // play slash anim
            PlayerManager.Instance.GetComponent<Animator>().Play("PlayerStrike", 0);
            //PlayerManager.Instance.audioSource.Play();
            PlayerManager.Instance.GetComponent<AudioSource>().Play();

            // scoot towards last move
            var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove;
            PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.8f);

            // do damage to area
            PlayerManager.Instance.DoDamage();

            var cooldown = PlayerManager.Instance.cooldownRate;
            while(cooldown > 0)
            {
 
                cooldown -= Time.deltaTime;
                yield return null;
                if(PlayerInput.Slash()) continueChain = true;
            }

            if(continueChain)
            {
                continueChain = false;
                BattleSystem.SetState(new SecondAttack(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new Begin(BattleSystem));
            }
                
                




        }
    }
}