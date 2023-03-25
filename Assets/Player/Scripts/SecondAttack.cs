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
            yield return new WaitUntil(() => PlayerManager.Instance.AnimFinished());
            PlayerManager.Instance.FinishAnimation();

            // play second slash anim
            PlayerManager.Instance.GetComponent<Animator>().Play("PlayerStrike2", 0);

            var cooldown = PlayerManager.Instance.cooldownRate;
            while(cooldown > 0)
            {
                if(PlayerInput.Slash()) continueChain = true;
                cooldown -= Time.deltaTime;
                yield return null;
            }

            
            if(continueChain)
            {
                continueChain = false;
                BattleSystem.SetState(new ThirdAttack(BattleSystem));
            }
            else
            {
                BattleSystem.SetState(new FirstAttack(BattleSystem));
            }    




        }


    }
}