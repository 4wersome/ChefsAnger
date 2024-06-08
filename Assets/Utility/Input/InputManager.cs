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

    public static bool AbilityCheeseWheelPressed()
    {
        return input.Player.AbilityCheeseWheel.WasPressedThisFrame();
    }

    public static bool AbilityAppleThrowPressed()
    {
        return input.Player.AbilityAppleThrow.WasPressedThisFrame();
    }

    #region mouse and keyboard


    public static bool PlayerMelee()
    {
        return input.Player.MeleeAttack.WasPressedThisFrame();
    }
    public static bool PlayerMeleePad()
    {
        return input.Player.MeleeAttackPad.WasPressedThisFrame();
    }
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
