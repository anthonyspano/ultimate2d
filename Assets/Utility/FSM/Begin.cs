using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class Begin : State
    {
        PlayerBattleSystem pbs;
        AudioSource audio;
        EnemyManager em;

        public Begin(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            
        }

        

        public override IEnumerator Start()
        {
            // wait for anykey
            yield return new WaitUntil(() => Input.anyKey);
            // grab key pressed from gamemanager
            yield return null;
            switch((int)GameManager.playerInput) // create different begin classes based on detected input
            {
                case 107: // 'k'
                    PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));
                    break;
                case 350:
                    PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));
                    break;
                case 352: // 'x'
                    PlayerBattleSystem.SetState(new Jump(PlayerBattleSystem));
                    break;
                default:
                    PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    break;

            }


            // yield return new WaitUntil(() => PlayerInput.Slash());
            // PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));

            

            
        }
 
        
    }
}