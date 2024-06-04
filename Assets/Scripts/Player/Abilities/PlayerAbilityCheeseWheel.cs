using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityCheeseWheel : AbilityBase
{
    private const int animationLayer = 0;
    private const string animationStateName = "ThrowCheese";
    private const string animationStateTrigger = "AbilityCheeseWheel";
    private bool isUnlocked = false;

    void Start()
    {
        playerController.MovePrevented += PreventAbility;
        playerController.MeleePrevented += PreventAbility;
    }

    void Update()
    {
        if (isUnlocked) 
        {
            if (InputManager.AbilityCheeseWheelPressed() && !isPrevented && !playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName))
            {
                playerController.AnimatorMgnr.SetTriggerParameter(animationStateTrigger);
                PreventAbility();
            }
            else if (playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName) && isPrevented)
            {
                UnPreventAbility();
            } 
        }
    }

    protected override void PreventAbility()
    {
        isPrevented = true;
    }

    protected override void UnPreventAbility()
    {
        isPrevented = false;
    }

    public override void UnlockAbility() 
    {
        Debug.Log("Cheese wheel unlocked");
        isUnlocked = true;
    }

    public void PrintEvent(string s)
    {
        Debug.Log("PrintEvent called at " + Time.time + " with a value of " + s);
    }
}
