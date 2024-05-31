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
  


    public static Vector2 PlayerMovement
    {
        get { return input.Player.Movement.ReadValue<Vector2>(); }
    }
    #endregion

    #region Pad
    public static Vector2 PlayerMovementPad
    {
        get { return input.Player.MovementPad.ReadValue<Vector2>(); }
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
