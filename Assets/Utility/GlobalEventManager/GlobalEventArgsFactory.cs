using System.Collections.Generic;
using UnityEngine;
using System;


public static class GlobalEventArgsFactory
{

#if DEBUG
    private delegate string EventDebug(GlobalEventArgs message);
    private static Dictionary<GlobalEventIndex, EventDebug> methodDebugString;

    static GlobalEventArgsFactory()
    {
        methodDebugString = new Dictionary<GlobalEventIndex, EventDebug>();
        methodDebugString.Add(GlobalEventIndex.CAMERAPlayerDeath, new EventDebug(PlayerDeathDebug));
        methodDebugString.Add(GlobalEventIndex.PlayerHealthUpdated, new EventDebug(PlayerHealthUpdatedDebug));
        methodDebugString.Add(GlobalEventIndex.PlayerMovement, new EventDebug(PlayerPositionDebug));
    }

    public static string GetDebugString(GlobalEventIndex eventType, GlobalEventArgs message)
    {
        return methodDebugString[eventType](message);
    }
#endif



    
    #region PlayerDeath
    public static GlobalEventArgs PlayerDeathFactory()
    {
        GlobalEventArgs message = new GlobalEventArgs();
        return message;
    }

    public static void PlayerDeathParser(GlobalEventArgs message)
    {

    }

    public static string PlayerDeathDebug(GlobalEventArgs message)
    {
        return string.Empty;
    }
    #endregion

    #region PlayerHealthUpdated
    public static GlobalEventArgs PlayerHealthUpdatedFactory(float maxHP, float currentHP)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[2];
        message.args[0] = new ExtendedVariable("MaxHP", ExtendedVariableType.Float, maxHP);
        message.args[1] = new ExtendedVariable("CurrenctHP", ExtendedVariableType.Float, currentHP);
        return message;
    }

    public static void PlayerHealthUpdatedParser(GlobalEventArgs message, out float maxHP, out float currentHP)
    {
        maxHP = (float)message.args[0].GetValue();
        currentHP = (float)message.args[1].GetValue();
    }

    public static string PlayerHealthUpdatedDebug(GlobalEventArgs message)
    {
        return " maxHP: " + message.args[0].GetValue().ToString() + " currentHP: " +
            message.args[1].GetValue().ToString();
    }
    #endregion

    #region PlayerMoving

    public static GlobalEventArgs PlayerMoveFactory(Vector3 position, Vector3 forward)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[2];
        message.args[0] = new ExtendedVariable("Position", ExtendedVariableType.Vector3, position);
        message.args[1] = new ExtendedVariable("Position", ExtendedVariableType.Vector3, forward);

        return message;
    }

    public static void PlayerMoveParser(GlobalEventArgs message, out Vector3 position, out Vector3 forward)
    {
        position = (Vector3)message.args[0].GetValue();
        forward = (Vector3)message.args[1].GetValue();
    }

    public static string PlayerPositionDebug(GlobalEventArgs message)
    {
        return "position" + message.args[0].GetValue().ToString() + "Forward: " + message.args[1].GetValue().ToString();
    }
    #endregion

    #region EnableGamepad

    public static GlobalEventArgs EnableGamepadFactory(bool value)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[1];
        message.args[0] = new ExtendedVariable("GamepadEnabled", ExtendedVariableType.Bool, value);
        return message;
    }

    public static void EnableGamepadParses(GlobalEventArgs message, out bool value)
    {
        value = (bool)message.args[0].GetValue();
    }

    #endregion

    #region UIRecipeCompleted
    public static GlobalEventArgs UIRecipeCompletedFactory(Recipe recipe)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[1];
        message.args[0] = new ExtendedVariable("Recipe", ExtendedVariableType.Recipe, recipe);
        return message;
    }

    public static void UIRecipeCompletedParses(GlobalEventArgs message, out Recipe recipe)
    {
        recipe = (Recipe)message.args[0].GetValue();
    }
    #endregion

    #region IngredientObtained
    public static GlobalEventArgs IngredientObtainedFactory(Ingredient ingredient)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[1];
        message.args[0] = new ExtendedVariable("Ingredient", ExtendedVariableType.Ingredient, ingredient);
        return message;
    }

    public static void IngredientObtainedParses(GlobalEventArgs message, out Ingredient ingredient)
    {
        ingredient = (Ingredient)message.args[0].GetValue();
    }
    #endregion

    #region PlayerDefenceUpdated
    public static GlobalEventArgs PlayerDefenceUpdatedFactory(float currentDefence)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[0];
        message.args[0] = new ExtendedVariable("CurrenctDefence", ExtendedVariableType.Float, currentDefence);
        return message;
    }

    public static void PlayerDefenceUpdatedParser(GlobalEventArgs message, out float currentDefence)
    {
        currentDefence = (float)message.args[0].GetValue();
    }

    public static string PlayerDefenceUpdatedDebug(GlobalEventArgs message)
    {
        return "CurrentDefence: " + message.args[0].GetValue().ToString();
    }
    #endregion

    #region PlayerAttackUpdated
    public static GlobalEventArgs PlayerAttackUpdatedFactory(float currentAttack)
    {
        GlobalEventArgs message = new GlobalEventArgs();
        message.args = new ExtendedVariable[1];
        message.args[0] = new ExtendedVariable("CurrentAttack", ExtendedVariableType.Float, currentAttack);
        return message;
    }

    public static void PlayerAttackUpdatedParser(GlobalEventArgs message, out float currentAttack)
    {
        currentAttack = (float)message.args[0].GetValue();
    }

    public static string PlayerAttackUpdatedDebug(GlobalEventArgs message)
    {
        return "CurrentAttack: " + message.args[0].GetValue().ToString();
    }
    #endregion
}
