using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{
    public class ShooterBattleSystem : StateMachine
    {
        private Transform enemy;
        public Transform Enemy
        {
            get { return enemy; }
            set { enemy = value; }
        }

        void Start()
        {
            Enemy = transform;
            SetState(new Begin(this));
        }
    }
}