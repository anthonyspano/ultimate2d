using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class BossAttackPlayer : State
    {
        BossBattleSystem bs;
        GameObject _atkBox;
        GameObject shockwavePrefab;
        EnemyManager em;
        Animator anim;
        float telegraphTime = 0.4f;

        public BossAttackPlayer(BossBattleSystem BossBattleSystem) : base(BossBattleSystem)
        {
            bs = BossBattleSystem;
            em = bs.GetComponent<EnemyManager>();
            anim = bs.GetComponent<Animator>();
        }

        public override IEnumerator Start()
        {
            if(!bs.Dead)
            {
                // final check for facing player
                anim.SetFloat("MoveX", em.PlayerFacingVector().x);
                anim.SetFloat("MoveY", em.PlayerFacingVector().y);

                // create attack box 
                _atkBox = Resources.Load("AttackBoxIndication") as GameObject;
                //spawn atkbox at player position
                var atkBox = Object.Instantiate(_atkBox, PlayerManager.Instance.transform.position, Quaternion.identity);
                bs.Player = atkBox.transform;

                // telegraph "charge up" attack
                anim.Play("Telegraph", 0);

                yield return new WaitForSeconds(telegraphTime);

                // play sound
                //bs.GetComponent<AudioSource>().PlayOneShot(enemyManager.attackSound, 1f);
                
                // charge toward 
                bs.transform.position = Vector2.Lerp(bs.transform.position, atkBox.transform.position, 0.4f);

                // play anim
                anim.Play("Attack", 0);

                // while(Vector3.Distance(bs.transform.position, atkBox.transform.position) > 0.03f)
                // {
                //     //bs.transform.position = Vector2.Lerp(bs.transform.position, atkBox.transform.position, 0.25f);
                //     bs.transform.position = Vector2.MoveTowards(bs.transform.position, atkBox.transform.position, 0.01f * EnemyManager.jumpSpeed);
                //     yield return null;
                // }
                
                // yield return new WaitUntil(() => !bs.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")); // animator is done playing attack animation
            

            }

            yield return new WaitForSeconds(1f);
            bs.CanMove = true;
            
            BossBattleSystem.SetState(new BossEngage(BossBattleSystem));
        }


        
    }
}