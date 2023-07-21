using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimManager : MonoBehaviour
{
    public GameObject ObjectAttackUp;
    public GameObject ObjectAttackLeft;
    public GameObject ObjectAttackRight;
    public GameObject ObjectAttackDown;

    public void PlayAttackUp()
    {
        ObjectAttackUp.GetComponent<Animator>().Play("slash2", 0);
    }

    public void PlayAttackLeft()
    {
        ObjectAttackLeft.GetComponent<Animator>().Play("slash2", 0);
    }

    public void PlayAttackRight()
    {
        ObjectAttackRight.GetComponent<Animator>().Play("slash2", 0);
    }

    public void PlayAttackDown()
    {
        ObjectAttackDown.GetComponent<Animator>().Play("slash2", 0);
    }
}
