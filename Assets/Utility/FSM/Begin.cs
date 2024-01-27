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

        

        // close the loop according to notepad on desk
        public override IEnumerator Start()
        {
            //yield return null;
            //Debug.Log("READY");
            // wait for anykey
            yield return new WaitUntil(() => PlayerController.Instance.playerStatus != PlayerController.PlayerStatus.Idle);
            // grab key pressed from gamemanager
            yield return null;
            switch(PlayerController.Instance.playerStatus) // create different begin classes based on detected input
            {
                case PlayerController.PlayerStatus.Move:
                
                    PlayerBattleSystem.SetState(new PlayerRun(PlayerBattleSystem));
                    break;
                    
                case PlayerController.PlayerStatus.Attack:
                    if(PlayerManager.Instance.attackCooldown <= 0)
                    {
                        // start cooldown timer
                        PlayerManager.Instance.attackCooldown = PlayerManager.Instance.attackCooldownRate;
                        PlayerManager.Instance.StartAttackCD();
                        Debug.Log("Attacking");
                        PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));
                    }
                    else 
                    {
                        PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    }

                    break;


                default:
                    PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
                    PlayerManager.Instance.isBusy = false;
                    PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    break;

            }


            // yield return new WaitUntil(() => PlayerInput.Slash());
            //PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));


            
        }
 
        
    }
}