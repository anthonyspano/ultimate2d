using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class EnemyManager : MonoBehaviour
    {
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

        void Start()
        {
            
        }

        public void CanMoveAgain()
        {
            CanMove = true;
        }




    }

}
