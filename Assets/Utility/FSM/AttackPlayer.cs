using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class AttackPlayer : State
    {
        BattleSystem bs;
        GameObject _atkBox;
        EnemyManager enemyManager;
        public AttackPlayer(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
            enemyManager = bs.GetComponent<EnemyManager>();
        }

        public override IEnumerator Start()
        {
            // play sound
            bs.GetComponent<AudioSource>().PlayOneShot(enemyManager.attackSound, 1f);
            // instantiate attack box
            _atkBox = Resources.Load("AttackBoxIndication") as GameObject;
            // spawn atkbox at player position
            var atkBox = Object.Instantiate(_atkBox, PlayerManager.Instance.transform.position, Quaternion.identity);
            bs.Player = atkBox.transform;
            yield return new WaitForSeconds(1);
            
            // charge toward 
            // play anim
            bs.GetComponent<Animator>().Play("Attack", 0);

            while(Vector3.Distance(bs.transform.position, atkBox.transform.position) > 0.03f)
            {
                //bs.transform.position = Vector2.Lerp(bs.transform.position, atkBox.transform.position, 0.25f);
                bs.transform.position = Vector2.MoveTowards(bs.transform.position, atkBox.transform.position, 0.01f * EnemyManager.jumpSpeed);
                yield return null;
            }
            
//            yield return new WaitUntil(() => !bs.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")); // animator is done playing attack animation
            
            yield return new WaitForSeconds(2f);

            BattleSystem.SetState(new Begin(BattleSystem));
        }


        
    }
}