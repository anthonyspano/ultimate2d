using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class BlockStatusCheck : State
    {
        PlayerBattleSystem pbs;
        EnemyManager em;
        int myHealth;

        public BlockStatusCheck(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            em = pbs.GetComponent<EnemyManager>();
            myHealth = pbs.GetComponent<EnemyTakeDamage>().healthSystem.GetHealth();
            
        }

        

        public override IEnumerator Start()
        {
            // 80% of max health
            // make this an enum status so it can be turned off
            if(myHealth < pbs.GetComponent<EnemyTakeDamage>().maxHealth * .8f)
            {
                // do spin attack
            }

            
            yield return null;
            
        }
 
        
    }
}
