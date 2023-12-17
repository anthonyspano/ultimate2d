using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class EnemyManager : MonoBehaviour
    {
        [HideInInspector]
        public int enemyLayerMask = 1 << 8;
        private static float fovRange;
        private static float damage;
        public static float atkSpeed;
        private static float moveSpeed;

        private static float retreatRange;
        private static bool canMove = true;

        public float pursuitRange;
        public float attackRange;

        public AudioClip attackSound;

        public static Transform AttackBox;

        // Reaction Attack
        public bool timeToReact;
        public bool ReactOnce;

        public static float RetreatRange
        {
            get { return retreatRange; }
            set { retreatRange = value; }
        }
        
        protected float Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        protected float AtkSpeed
        {
            get { return atkSpeed; }
            set { atkSpeed = value; }
        }
        protected float MoveSpeed
        {
            get { return moveSpeed; }
            set { moveSpeed = value; }
        }
        public static float FoVRange
        {
            get { return fovRange; }
            set { fovRange = value; }
        }

        public bool CanMove
        {
            get { return canMove; }
            set { canMove = value; }
        }

        private SpriteRenderer sr;

        public static bool Busy = false;
        public static bool Retreating;

        public static int jumpSpeed = 15;

        public void CanMoveAgain()
        {
            CanMove = true;
        }

        private Animator anim;

        void Start()
        {
            anim = GetComponent<Animator>();
        }

        public Vector2 PlayerFacingVector()
        {
            // the direction the enemy is facing is the 
            // some trig
            Vector3 n = transform.position - PlayerManager.Instance.transform.position;
            n.Normalize();
            float rot_z = Mathf.Atan2(n.y, n.x) * Mathf.Rad2Deg;
            rot_z = 90 - rot_z;           
            
            if(rot_z > 45f && rot_z <= 135f) // left face
            {
                return new Vector2(-1,0);
            }
            else if(rot_z <= 225f && rot_z > 135f) // up face
            {
                return new Vector2(0,1);
            }
            else if(rot_z < -45f || rot_z > 225f) // right face
            {
                return new Vector2(1,0);    
            }
            else // down face
            {
                return new Vector2(0,-1);
            }




        }



    }

}
