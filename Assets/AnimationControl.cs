using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public void EndAnim()
    {
        GetComponent<Animator>().Play("Neutral");
    }
}
