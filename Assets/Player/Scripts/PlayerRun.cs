using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class PlayerRun : State
    {
        public PlayerRun(PlayerBattleSystem playerBattleSystem) : base(playerBattleSystem) { }

        public override IEnumerator Start()
        {
            while(PlayerController.Instance.playerStatus == PlayerController.PlayerStatus.Move && PlayerManager.Instance.CanMove)
            {
                var direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0) + PlayerManager.Instance.transform.position;
                PlayerManager.Instance.transform.position = Vector2.MoveTowards(PlayerManager.Instance.transform.position, direction, PlayerManager.Instance.moveSpeed * Time.deltaTime);
                yield return null;
            }

            //PlayerController.Instance.playerStatus = PlayerController.PlayerStatus.Idle;
            //PlayerManager.Instance.isBusy = false;

            PlayerBattleSystem.SetState(new Begin(PlayerBattleSystem));

        }
    }

}