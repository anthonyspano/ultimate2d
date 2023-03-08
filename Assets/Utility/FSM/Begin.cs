using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class Begin : State
    {
        BattleSystem bs;
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }

        public override IEnumerator Start()
        {
            Debug.Log("patrolling");

            // checking if player is in range
            yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.PursuitRange));
            
            BattleSystem.SetState(new PursuePlayer(BattleSystem));
        }


        
    }
}