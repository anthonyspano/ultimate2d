using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class AttackPlayer : State
    {
        BattleSystem bs;
        GameObject _atkBox;
        public AttackPlayer(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
        }

        public override IEnumerator Start()
        {

            // instantiate attack box
            _atkBox = Resources.Load("AttackBoxIndication") as GameObject;
            // spawn atkbox at player position
            var atkBox = Object.Instantiate(_atkBox, PlayerManager.Instance.transform.position, Quaternion.identity);
            
            yield return new WaitForSeconds(1);
            
            // charge toward 
            while(Vector3.Distance(bs.transform.position, atkBox.transform.position) > 0.03f)
            {
                bs.transform.position = Vector2.Lerp(bs.transform.position, atkBox.transform.position, 0.25f);
                yield return null;
            }
            
//            yield return new WaitUntil(() => !bs.Animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")); // animator is done playing attack animation
            
            yield return new WaitForSeconds(2f);

            BattleSystem.SetState(new Begin(BattleSystem));
        }


        
    }
}