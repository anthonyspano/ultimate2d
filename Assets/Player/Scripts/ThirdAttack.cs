using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ThirdAttack : State
    {
        private PlayerBattleSystem pbs;


        public ThirdAttack(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = PlayerBattleSystem;
        }
        public override IEnumerator Start() 
        {
            // wait until animation is on second to last frame and use event to trigger bool?
            // or wait until animation is finished?
            yield return new WaitUntil(() => PlayerManager.Instance.AnimFinished());
            PlayerManager.Instance.FinishAnimation();

            // play third slash anim
            //Debug.Log("Third attack");
            PlayerManager.Instance.CanMove = false;
            //PlayerManager.Instance.GetComponent<Animator>().Play("PlayerStrike3", 0);
            PlayerManager.Instance.GetComponent<AudioSource>().Play();
            // play slash anim
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", true);

            // scoot towards last move
            var newPos = PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.AttackMoveDistance;
            PlayerManager.Instance.transform.position = Vector3.Lerp(PlayerManager.Instance.transform.position, newPos, 0.4f);
            
            var cooldown = PlayerManager.Instance.attackCooldownRate;
            while(cooldown > 0)
            {
                cooldown -= Time.deltaTime;
                yield return null;
            }
            // turn off attack anim
            PlayerManager.Instance.GetComponent<Animator>().SetBool("IsAttacking", false);

            // yield return new WaitForSeconds(chainCD);
            PlayerManager.Instance.CanMove = true;
            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                




        }


    }
}
