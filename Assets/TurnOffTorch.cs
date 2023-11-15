using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.ultimate2d.combat;

public class TurnOffTorch : MonoBehaviour
{
    public GameObject Torch;

    void OnDestroy()
    {
        // disable torch firing
        Torch.GetComponent<BlockBattleSystem>().CanAttack = false;

        // change torch anim state to off
        Torch.GetComponent<Animator>().Play("Off");
       
    }
}
