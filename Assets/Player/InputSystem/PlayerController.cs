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

        public enum PlayerStatus {Idle, Move, Dodge, Attack};
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
            // playerInputActions.Keyboard.Enable();
            playerInputActions.Player.Enable();
            playerInputActions.Player.Dodge.performed += Dodge;
            playerInputActions.Player.Attack.performed += Attack;
            //playerInputActions.Keyboard.Movement.performed += Movement;
            
            //playerInputActions.Player.Movement.performed += c => Debug.Log(c.ReadValue<Vector2>());
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
                }
                else
                    playerStatus = PlayerStatus.Idle;
            }
            // var value1 = playerInputActions.Player.Movement.ReadValue<Vector2>(); // 2DVector
            // if(value1 != null)
            //     Debug.Log(value1);
            

        }

        public void Dodge(InputAction.CallbackContext context)
        {
            // started, performed, canceled
            // if(context.performed)
            //     Debug.Log("dodge " + context.phase);

            // start dodge cooldown  
            if(!PlayerManager.Instance.isBusy)
            {
                PlayerManager.Instance.isBusy = true;
                playerStatus = PlayerStatus.Dodge;

            }
        }

        public void Movement(InputAction.CallbackContext context)
        {
            Debug.Log(context.ReadValue<Vector2>());
            
            
        }

        public void Attack(InputAction.CallbackContext context)
        {
            // start Attack cooldown
            // player manager subscribes to this event

            if(!PlayerManager.Instance.isBusy)
            {
                PlayerManager.Instance.isBusy = true;
                playerStatus = PlayerStatus.Attack;
            }
        }
    }

}