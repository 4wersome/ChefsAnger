using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPumpkinMine : AbilityBase
{
    private const string animatorPumpkinTriggerName = "PumpkinMine";
    private const string animatorisAttackingBoolName = "isAttacking";
    private const string animatorPumpkinStateNane = "PlantPumpkin";

    [SerializeField]
    private ProjectileMine Mine;

    [SerializeField]
    private bool animationIsReady;
    




    protected override void PreventAbility()
    {
        throw new System.NotImplementedException();
    }

    protected override void UnPreventAbility()
    {
        throw new System.NotImplementedException();
    }

    private void Update()
    {
        if (isEnabled)
        {


            if (InputManager.AbilityPumpkinMinePressed() && !Mine.isActiveAndEnabled)
                
            {
                playerController.AnimatorMgnr.SetTriggerParameter(animatorPumpkinTriggerName);
                playerController.AnimatorMgnr.SetAnimatorBool(animatorisAttackingBoolName,true);
               

            }
            if (animationIsReady)
            {
                Mine.transform.position = playerController.transform.position;
                Mine.EnableProjectile();
                Mine.Launch(0, playerController.GetForward());
            }
        }
    }
}
