using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.ultimate2d.combat
{
    public class ReactionAttack : State
    {
        BossBattleSystem bs;
        Animator anim;
        EnemyManager em;
        AudioSource audio;
        float telegraphTime = 0.5f;

        public ReactionAttack(BossBattleSystem _bossBattleSystem) : base(_bossBattleSystem)
        {
            bs = _bossBattleSystem;
            anim = bs.GetComponent<Animator>();
            em = bs.GetComponent<EnemyManager>();
            audio = bs.GetComponent<AudioSource>();
        }

        public override IEnumerator Start()
        {
            Debug.Log("reaction attack go!");
            // telegraph "charge up" attack
            anim.Play("Telegraph", 0);

            yield return new WaitForSeconds(telegraphTime);

            anim.Play("Attack", 0);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
            anim.Play("Attack", 0);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
            anim.Play("Attack", 0);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);



            yield return null;
            BossBattleSystem.SetState(new BossEngage(BossBattleSystem));
        }


        
    }
}