using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class Jump : State
    {
        private float coolDownTimer = 0;
        private Animator anim;
        private Rigidbody2D rb;

        private BattleSystem bs;

        public Jump(BattleSystem battleSystem) : base(battleSystem)
        {
            bs = battleSystem;
            anim = bs.GetComponent<Animator>();
            rb = bs.GetComponent<Rigidbody2D>();
        }

        public override IEnumerator Start() 
        {
            if(coolDownTimer <= 0)
            {
                // perform jump
                Debug.Log("Jump");
                //anim.Play("PlayerJump", 0);
                PlayerManager.Instance.CanMove = false;
                // while animator is playing clip, add force
                // while(anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerJump"))
                // {
                    // addforce + directional movement amplifies is greatly
                    //rb.AddForce(PlayerManager.Instance.LastMove * jumpDistance);
                    //Debug.Log(PlayerManager.Instance.LastMove);
                    //PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, 
                    //                                                                    PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.JumpDistance, PlayerManager.Instance.MDD);
                        
                var x = Input.GetAxis(PlayerInput.x);
                var y = Input.GetAxis(PlayerInput.y);
                var direction = new Vector2(x, y);
                PlayerManager.Instance.transform.Translate(direction * PlayerManager.Instance.JumpDistance * Time.deltaTime);
                yield return null;
                //}
                coolDownTimer = PlayerManager.Instance.jumpCooldownRate;
                PlayerManager.Instance.CanMove = true;
            }   
 
            BattleSystem.SetState(new Begin(BattleSystem));  
       
        }

    }
    
}