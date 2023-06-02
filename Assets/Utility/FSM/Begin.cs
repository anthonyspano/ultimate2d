using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class Begin : State
    {
        BattleSystem bs;
        AudioSource audio;

        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
            
            bs = battleSystem;
            if(bs.transform.gameObject.CompareTag("Enemy"))
                audio = bs.GetComponent<AudioSource>();

            
        }

        public override IEnumerator Start()
        {

            if(bs.transform.gameObject.CompareTag("Player"))
            {
                // wait for anykey
                yield return new WaitUntil(() => Input.anyKey);
                // grab key pressed from gamemanager
                yield return null;
                switch((int)GameManager.playerInput) // create different begin classes based on detected input
                {
                    case 107: // 'k'
                        BattleSystem.SetState(new FirstAttack(BattleSystem));
                        break;
                    case 32: // 'space'
                        //BattleSystem.SetState(new Jump(BattleSystem));
                        break;
                    default:
                        BattleSystem.SetState(new Begin(BattleSystem));
                        break;

                }


                // yield return new WaitUntil(() => PlayerInput.Slash());
                // BattleSystem.SetState(new FirstAttack(BattleSystem));

            }
            if(bs.transform.gameObject.CompareTag("Enemy"))
            {
                // checking if player is in range
                yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.PursuitRange));
                audio.Play();
                BattleSystem.SetState(new PursuePlayer(BattleSystem));
            }
            if(bs.transform.gameObject.CompareTag("Shooter"))
            {
                // checking if player is in range
                yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.PursuitRange));
                BattleSystem.SetState(new ShootPlayer(BattleSystem));
            }

            
        }
 
        
    }
}