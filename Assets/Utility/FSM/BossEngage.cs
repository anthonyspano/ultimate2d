using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    // root of decision tree for bosses
    public class BossEngage : State
    {
        BossBattleSystem bs;
        Animator anim;
        EnemyManager em;
        AudioSource audio;

        public BossEngage(BossBattleSystem _bossBattleSystem) : base(_bossBattleSystem)
        {
            bs = _bossBattleSystem;
            anim = bs.GetComponent<Animator>();
            bs.Player = PlayerManager.Instance.transform;
            em = bs.GetComponent<EnemyManager>();
            audio = bs.GetComponent<AudioSource>();


        }

        public override IEnumerator Start()
        {
            if(em.timeToReact && !em.ReactOnce)
            {
                em.ReactOnce = true;
                em.timeToReact = false;
                BossBattleSystem.SetState(new ReactionAttack(BossBattleSystem));
            }
            else
            {
                yield return new WaitUntil(() => bs.PlayerIsInRange(em.pursuitRange));
                while(bs.PlayerIsInRange(em.pursuitRange) && !bs.PlayerIsInRange(em.attackRange) && bs.CanMove)
                {
                    anim.SetBool("Running", true);
                    anim.SetFloat("MoveX", em.PlayerFacingVector().x);
                    anim.SetFloat("MoveY", em.PlayerFacingVector().y);
                    bs.transform.position = Vector3.MoveTowards(bs.transform.position, bs.Player.position, 5f * Time.deltaTime);
                    yield return null;
                }

                //Debug.Log(em.pursuitRange);
                // set bs target to player's current position
                //bs.PlayerPosition = PlayerManager.player.transform.position;
                anim.SetBool("Running", false);
                bs.CanMove = false;
                // checking if player is in range
                //yield return new WaitUntil(() => bs.PlayerIsInRange(em.attackRange));
                
                //yield return new WaitForSeconds(1f); // the wait before attacking
                BossBattleSystem.SetState(new BossAttackPlayer(BossBattleSystem));
                yield return null;
            }
        }




        

    }
}