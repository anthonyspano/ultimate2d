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
            // checking if player is in range
            yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.PursuitRange));
            
            if(bs.Enemy.gameObject.CompareTag("Enemy"))
                BattleSystem.SetState(new PursuePlayer(BattleSystem));
            if(bs.Enemy.gameObject.CompareTag("Shooter"))
                BattleSystem.SetState(new ShootPlayer(BattleSystem));
            if(bs.Enemy.gameObject.CompareTag("Player"))
                BattleSystem.SetState(new FirstAttack(BattleSystem));
        }
 
        
    }
}