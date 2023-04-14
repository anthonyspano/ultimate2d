using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class BattleSystem : StateMachine
    {
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

        private Transform enemy;
        public Transform Enemy
        {
            get { return enemy; }
            set { enemy = value; }
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
            Enemy = transform;
            SetState(new Begin(this));

            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if(transform.CompareTag("Enemy"))
                sr.flipX = Flipped();
        }

        public bool PlayerIsInRange(float range)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range, EnemyManager.enemyLayerMask);
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
            if(transform.position.x - Player.transform.position.x < 0)
            {
                return false;
            }

            return true;
        }


    }
}