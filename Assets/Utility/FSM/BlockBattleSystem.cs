using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace com.ultimate2d.combat
{
    public class BlockBattleSystem : StateMachine
    {
        // cleanup: shared variables for class
        [HideInInspector]
        public EnemyManager _enemyManager;
        private bool canAttack = true;
        public bool CanAttack
        {
            get { return canAttack; }
            set { canAttack = value; }
        }

        private bool canMove = true;
        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }
        
        private bool dead = false;
        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }
        private Animator animator;
        public Animator Animator
        {
            get { return animator; }
            set { animator = value; }
        }

        private Vector3 playerPos;
        public Vector3 PlayerPosition
        {
            get { return playerPos; }
            set { playerPos = value; }
        }


        private Transform player;
        public Transform Player
        {
            get { return player; }
            set { player = value; }
        }

        private SpriteRenderer sr;

        private void Start()
        {
            sr = GetComponent<SpriteRenderer>();
            _enemyManager = GetComponent<EnemyManager>();

            SetState(new PursuePlayer(this));  
        }

        private void Update()
        {
            if(transform.CompareTag("Enemy"))
                sr.flipX = Flipped();
        }

        public bool PlayerIsInRange(float range)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, _enemyManager.enemyLayerMask);
            if(colliders.Length > 0) 
                return true;
            
            return false;
        }


        public bool AttackAnimIsPlaying()
        {
            return animator.GetCurrentAnimatorStateInfo(0).IsName("Attacking");
        }

        public bool Flipped()
        {
            if(transform.position.x - PlayerManager.Instance.transform.position.x < 0)
            {
                return false;
            }

            return true;
        }


    }
}