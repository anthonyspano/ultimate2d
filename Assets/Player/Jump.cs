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
            // start cooldown of jump
            //PlayerManager.Instance.StartJumpCD();
            
            // perform jump
            
            PlayerManager.Instance.CanMove = false;
            var direction = PlayerManager.Instance.LastMove;
            // while animator is playing clip, add force
            //yield return null;

            // while player isn't at intended final space after jump
            Vector2 finalSpace = PlayerManager.Instance.transform.position + direction * PlayerManager.Instance.JumpDistance;
            anim.Play("Player_Jump", 0);
            while(Mathf.Abs((PlayerManager.Instance.transform.XandY() - finalSpace).magnitude) > 0.1f)
            {
                // raycast into wall, if going to hit wall, then stop at wall
                RaycastHit2D hit = Physics2D.Raycast(PlayerManager.Instance.transform.position, direction, 0.5f);
                if(hit)
                {
                    if(hit.collider.CompareTag("Wall"))
                    {
                        Debug.Log("stopping");
                        anim.Play("Player_Idle");
                        break;
                    }
                }

                PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position,
                                                                                PlayerManager.Instance.transform.position + direction * PlayerManager.Instance.JumpDistance,
                                                                                PlayerManager.Instance.MDD);


                yield return null;

            }

            //while(anim.GetCurrentAnimatorStateInfo(0).IsName("Player_Jump"))
            // {
            //     // addforce + directional movement amplifies is greatly
            //     //rb.AddForce(PlayerManager.Instance.LastMove * jumpDistance);
            //     //Debug.Log(PlayerManager.Instance.LastMove);
            //     PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position,
            //                                                                     PlayerManager.Instance.transform.position + PlayerManager.Instance.LastMove * PlayerManager.Instance.JumpDistance,
            //                                                                     PlayerManager.Instance.MDD);

            //     // var x = Input.GetAxis(PlayerInput.x);
            //     // var y = Input.GetAxis(PlayerInput.y);
            //     // var direction = new Vector2(x, y);
            //     // PlayerManager.Instance.transform.Translate(direction * PlayerManager.Instance.JumpDistance * Time.deltaTime);
            //     yield return null;
            // }

            

            // end jump endeavors
            PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            PlayerManager.Instance.isBusy = false;

            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));

        }

    }

}
