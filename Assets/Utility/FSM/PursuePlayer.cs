using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if adding a wait to the attack, then cultist runs in place for that amount of time
namespace com.ultimate2d.combat
{
    public class PursuePlayer : State
    {
        BattleSystem bs;
        Animator anim;
        
        public PursuePlayer(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
            anim = bs.gameObject.GetComponent<Animator>();
            bs.Player = PlayerManager.Instance.transform;
        }

        public override IEnumerator Start()
        {
            while(bs.PlayerIsInRange(bs._enemyManager.pursuitRange) && !bs.PlayerIsInRange(bs._enemyManager.attackRange))
            {
                anim.SetBool("Running", true);
                bs.transform.position = Vector3.MoveTowards(bs.transform.position, bs.Player.position, 5f * Time.deltaTime);
                yield return null;
            }

            // set bs target to player's current position
            //bs.PlayerPosition = PlayerManager.player.transform.position;
            anim.SetBool("Running", false);
            
            // checking if player is in range
            //yield return new WaitUntil(() => bs.PlayerIsInRange(bs._enemyManager.attackRange));
            
            //yield return new WaitForSeconds(1f); // the wait before attacking
            BattleSystem.SetState(new AttackPlayer(BattleSystem));
        }


        
    }
}