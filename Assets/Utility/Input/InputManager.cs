using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public static class InputManager
{

    private static Inputs input;

    static InputManager()
    {
        input = new Inputs();
        //TEMPORARY SUPER TBD MA RIMARRA' COSI' FINO ALLA FINE DELLO SVILUPPO. UN CLASSICO
        input.Player.Enable();
    }

    public static Inputs.PlayerActions Player
    {
        get { return input.Player; }
    }



    #region mouse and keyboard
    public static float Player_LaunchAbility
    {
        get { return input.Player.LaunchAbility.ReadValue<float>(); }
    }

    public static Vector2 Player_Movement
    {
        get { return input.Player.Movement.ReadValue<Vector2>(); }
    }
    #endregion



    public static Vector2 RightAxis
    {
       get { return input.Player.Looking.ReadValue<Vector2>(); }
    }

    public static void EnablePlayerMap(bool enable)
    {
        if (enable)
        {
            input.Player.Enable();
        }
        else
        {
            input.Player.Disable();
        }
    }


}
