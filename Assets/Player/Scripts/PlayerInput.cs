using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static string x = "Horizontal";
    public static string y = "Vertical";
    private static KeyCode c_jump = KeyCode.JoystickButton2; // X
    private static KeyCode k_jump = KeyCode.Space;
    private static KeyCode c_shoot = KeyCode.JoystickButton3; // Y
    private static KeyCode k_shoot = KeyCode.J;
    private static int c_ultimate = 355; // RB
    private static KeyCode k_ultimate = KeyCode.Space;
    private static KeyCode c_slash = KeyCode.JoystickButton0; // A
    private static KeyCode k_slash = KeyCode.K;

    private static KeyCode c_interact = KeyCode.JoystickButton1; // B
    private static KeyCode k_interact = KeyCode.I;

    public static bool Jump() 
    {
        if(Input.GetKeyDown(k_jump) || Input.GetKeyDown(c_jump))
            return true;
        return false;
    }

    public static bool Shoot()
    {
        if(Input.GetKeyDown(k_shoot) || Input.GetKeyDown(c_shoot))
            return true;
        return false;
    }

    public static bool Ultimate() 
    {
        if(Input.GetKeyDown(k_ultimate) || Input.GetKeyDown((KeyCode)c_ultimate))
            return true;
        return false;
    }
    public static bool Slash()
    {
        if(Input.GetKeyDown(k_slash) || Input.GetKeyDown(c_slash))
            return true;
        return false;
    }

    public static bool Interact()
    {
        if(Input.GetKey(c_interact) || Input.GetKey(k_interact))
            return true;
        return false;
    }

}
