using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class PursuePlayer : State
    {
        BattleSystem bs;
        public PursuePlayer(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }

        public override IEnumerator Start()
        {
            while(bs.PlayerIsInRange(EnemyManager.PursuitRange) && !bs.PlayerIsInRange(EnemyManager.AttackRange))
            {
                bs.Enemy.position = Vector3.MoveTowards(bs.Enemy.position, PlayerManager.Instance.transform.position, 1);
            }

            // set bs target to player's current position
            //bs.PlayerPosition = PlayerManager.player.transform.position;
            
            // checking if player is in range
            yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.AttackRange));
            
            BattleSystem.SetState(new AttackPlayer(BattleSystem));
        }


        
    }
}