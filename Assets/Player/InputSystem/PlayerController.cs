using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace com.ultimate2d.combat
{
    public class PlayerController : MonoBehaviour
    {
        // singleton
        private static PlayerController _instance;
        public static PlayerController Instance
        {
            get { return _instance; }
        }


        private Animator anim;
        private PlayerInputActions playerInputActions;
        private Vector2 inputVector;

        public float movementSpeed;

        public enum PlayerStatus {Idle, Move, Dodge};
        public PlayerStatus playerStatus;

        private void Awake()
        {
            // singleton
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
            
            anim = GetComponent<Animator>();

            playerStatus = PlayerStatus.Idle;

            playerInputActions = new PlayerInputActions();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Dodge.performed += Dodge;
            
            //playerInputActions.Player.Movement.performed += Movement;
        }

        private void FixedUpdate()
        {
            // move
            inputVector = playerInputActions.Player.Movement.ReadValue<Vector2>();
            if(!PlayerManager.Instance.isBusy)
            {
                if(inputVector != new Vector2(0, 0))
                {
                    playerStatus = PlayerStatus.Move;
                    //PlayerManager.Instance.isBusy = true;
                }
                else
                    playerStatus = PlayerStatus.Idle;
            }

        }

        // TBI
        public void Dodge(InputAction.CallbackContext context)
        {
            // started, performed, canceled
            playerStatus = PlayerStatus.Dodge;
            PlayerManager.Instance.isBusy = true;
            if(context.performed)
                Debug.Log("dodge " + context.phase);
        }

        public void Movement(InputAction.CallbackContext context)
        {
            //Debug.Log(context.ReadValue<Vector2>());
            
            
        }
    }

}