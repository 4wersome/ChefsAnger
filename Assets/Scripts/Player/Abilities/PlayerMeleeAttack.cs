using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : AbilityBase
{
    private const int animationLayer = 0;
    private const string animationStateName = "Melee";
    private const string animationAttackTrigger = "Attack";

    private void Start()
    {
        playerController.MeleePrevented += PreventAbility;
    }
    void Update()
    {
        if (playerController.IsGamepadActive)
        {
            meleeAttackPad();
        }
        else
        {
            meleeAttack();
        }
    }

    // simple  meleeAttack based on starting the animation. In The animation the Weapon Collider is enabled for a short period of time , and then gets disabled 
    private void meleeAttack() 
    {
        if (InputManager.PlayerMelee() && !isPrevented && !playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName))
        {

            playerController.AnimatorMgnr.SetTriggerParameter(animationAttackTrigger);
            PreventAbility();
        }
        else if (playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName) && isPrevented)
        {
            UnPreventAbility();
        }
    }


    private void meleeAttackPad()
    {
        if (InputManager.PlayerMeleePad() && !isPrevented && !playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName))
        {

            playerController.AnimatorMgnr.SetTriggerParameter(animationAttackTrigger);
            PreventAbility();
        }
        else if (playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName) && isPrevented)
        {
            UnPreventAbility();
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
}
