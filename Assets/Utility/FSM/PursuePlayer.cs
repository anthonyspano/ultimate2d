using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }

        public override IEnumerator Start()
        {
            bs.Player = PlayerManager.Instance.transform;
            while(bs.PlayerIsInRange(EnemyManager.PursuitRange) && !bs.PlayerIsInRange(EnemyManager.AttackRange))
            {
                anim.SetBool("Walking", true);
                bs.transform.position = Vector3.MoveTowards(bs.transform.position, bs.Player.position, 5f * Time.deltaTime);
                yield return null;
            }

            // set bs target to player's current position
            //bs.PlayerPosition = PlayerManager.player.transform.position;
            anim.SetBool("Walking", false);
            
            // checking if player is in range
            yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.AttackRange));
            
            BattleSystem.SetState(new AttackPlayer(BattleSystem));
        }


        
    }
}