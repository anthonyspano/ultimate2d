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

        private PlayerBattleSystem pbs;
        private bool once;

        public Jump(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem)
        {
            pbs = playerBattleSystem;
            anim = pbs.GetComponent<Animator>();
            rb = pbs.GetComponent<Rigidbody2D>();
        }

        // 1 adjust
        // animation too long
        // move less
        public override IEnumerator Start()
        {
            PlayerManager.Instance.jumpCooldown -= Time.deltaTime;
            if(PlayerManager.Instance.jumpCooldown <= 0)
            {
                PlayerManager.Instance.jumpCooldown = PlayerManager.Instance.jumpCooldownRate;
                // perform jump
                anim.Play("Player_Jump", 0);
                PlayerManager.Instance.CanMove = false;
                // while animator is playing clip, add force
                yield return null;

                while(anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
                {
                    // addforce + directional movement amplifies is greatly
                    //rb.AddForce(PlayerManager.Instance.LastMove * jumpDistance);
                    //Debug.Log(PlayerManager.Instance.LastMove);
                    PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position,
                                                                                    PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.JumpDistance,
                                                                                    PlayerManager.Instance.MDD);

                    // var x = Input.GetAxis(PlayerInput.x);
                    // var y = Input.GetAxis(PlayerInput.y);
                    // var direction = new Vector2(x, y);
                    // PlayerManager.Instance.transform.Translate(direction * PlayerManager.Instance.JumpDistance * Time.deltaTime);
                    yield return null;
                }

                // start cooldown of jump
                PlayerManager.Instance.StartJumpCD();

                PlayerManager.Instance.CanMove = true;

            }

            // end jump endeavors
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;

            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));

        }

    }

}
