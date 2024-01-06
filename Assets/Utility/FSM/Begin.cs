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
                case PlayerController.PlayerStatus.Dodge: 
                    Debug.Log("dodging");
                    PlayerBattleSystem.SetState(new Jump(PlayerBattleSystem));
                    break;

                default:
                    PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
                    PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));
                    break;

            }


            // yield return new WaitUntil(() => PlayerInput.Slash());
            //PlayerBattleSystem.SetState(new FirstAttack(PlayerBattleSystem));


            
        }
 
        
    }
}