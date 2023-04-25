using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ThirdAttack : State
    {
        private BattleSystem bs;


        public ThirdAttack(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }
        public override IEnumerator Start() 
        {
            // wait until animation is on second to last frame and use event to trigger bool?
            // or wait until animation is finished?
            yield return new WaitUntil(() => PlayerManager.Instance.AnimFinished());
            PlayerManager.Instance.FinishAnimation();

            // play third slash anim
            PlayerManager.Instance.GetComponent<Animator>().Play("PlayerStrike3", 0);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();

            // do damage to area
            PlayerManager.Instance.DoDamage();
            
            var cooldown = PlayerManager.Instance.cooldownRate;
            while(cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                yield return null;
            }

            // yield return new WaitForSeconds(chainCD);

            BattleSystem.SetState(new FirstAttack(BattleSystem));
                




        }


    }
}
