using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class Begin : State
    {
        BattleSystem bs;
        AudioSource audio;
        EnemyManager em;

        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {
            
            bs = battleSystem;
            if(bs.transform.gameObject.CompareTag("Enemy"))
                audio = bs.GetComponent<AudioSource>();

            bs._enemyManager = bs.GetComponent<EnemyManager>();

            
        }

        public override IEnumerator Start()
        {

            if(bs.transform.gameObject.CompareTag("PlayerAttack"))
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
                    case 350:
                        BattleSystem.SetState(new FirstAttack(BattleSystem));
                        break;
                    case 352: // 'x'
                        BattleSystem.SetState(new Jump(BattleSystem));
                        break;
                    default:
                        BattleSystem.SetState(new Begin(BattleSystem));
                        break;

                }


                // yield return new WaitUntil(() => PlayerInput.Slash());
                // BattleSystem.SetState(new FirstAttack(BattleSystem));

            }
            // if(bs.transform.gameObject.CompareTag("Enemy"))
            // {
            //     // checking if player is in range
            //     //yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.pursuitRange));
            //     audio.Play();
            //     BattleSystem.SetState(new PursuePlayer(BattleSystem));
            // }
            if(bs.transform.gameObject.CompareTag("BigCultist"))
            {
                yield return new WaitUntil(() => bs.PlayerIsInRange(bs._enemyManager.pursuitRange));
                Debug.Log("pursuing");
                BattleSystem.SetState(new PursuePlayer(BattleSystem));
            }
            if(bs.transform.gameObject.CompareTag("Shooter"))
            {
                // checking if player is in range
                //yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.pursuitRange));
                BattleSystem.SetState(new ShootPlayer(BattleSystem));
            }

            yield return null;

            
        }
 
        
    }
}