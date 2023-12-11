using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// if adding a wait to the attack, then cultist runs in place for that amount of time
namespace com.ultimate2d.combat
{
    public class PursuePlayer : State
    {
        BlockBattleSystem bs;
        Animator anim;
        EnemyManager em;
        AudioSource audio;

        public PursuePlayer(BlockBattleSystem _blockBattleSystem) : base(_blockBattleSystem)
        {
            bs = _blockBattleSystem;
            anim = bs.GetComponent<Animator>();
            bs.Player = PlayerManager.Instance.transform;
            em = bs.GetComponent<EnemyManager>();
            audio = bs.GetComponent<AudioSource>();
        }

        public override IEnumerator Start()
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
            BlockBattleSystem.SetState(new AttackPlayer(BlockBattleSystem));
        }


        
    }
}