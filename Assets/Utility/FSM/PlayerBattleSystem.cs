using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.ultimate2d.combat
{

    public class PlayerBattleSystem : StateMachine
    {

        void Start()
        {
            SetState(new Begin(this));
        }

    }

}