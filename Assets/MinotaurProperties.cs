using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurProperties : MonoBehaviour
{
    public static float radius = 1.25f;
    public static float SightRange = 10;
    public static float AttackRange = 4.25f;
    public static LayerMask PlayerMask;
    public static float CoolDownRate = 2f;
    public static bool IsBusy;

    void Start()
    {
        PlayerMask = LayerMask.GetMask("Player");
    }

    public void NotBusySignal()
    {
        IsBusy = false;
    }
}
