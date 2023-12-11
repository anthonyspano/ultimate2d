using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// add constructor here for each new implementation
namespace com.ultimate2d.combat
{
    public abstract class State
    {
        protected BlockBattleSystem BlockBattleSystem;
        protected PlayerBattleSystem PlayerBattleSystem;
        protected BossBattleSystem BossBattleSystem;

        public State(BlockBattleSystem blockBattleSystem)
        {
            BlockBattleSystem = blockBattleSystem;

        }

        public State(PlayerBattleSystem playerBattleSystem)
        {
            PlayerBattleSystem  = playerBattleSystem;
        }

        public State(BossBattleSystem bossBattleSystem)
        {
            BossBattleSystem = bossBattleSystem;
        }
        
        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Attack()
        {
            yield break;
        }

        public virtual IEnumerator Heal()
        {
            yield break;
        }
    }

}