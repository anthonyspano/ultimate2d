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
            // checking if player is in range
            yield return new WaitUntil(() => bs.PlayerIsInRange(EnemyManager.PursuitRange));

            if(bs.transform.gameObject.CompareTag("Enemy"))
            {
                audio.Play();
                BattleSystem.SetState(new PursuePlayer(BattleSystem));
            }
            if(bs.transform.gameObject.CompareTag("Shooter"))
                BattleSystem.SetState(new ShootPlayer(BattleSystem));
            if(bs.transform.gameObject.CompareTag("Player"))
                BattleSystem.SetState(new FirstAttack(BattleSystem));
        }
 
        
    }
}