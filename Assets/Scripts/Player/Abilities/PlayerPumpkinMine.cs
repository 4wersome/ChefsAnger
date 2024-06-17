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



    private void Awake()
    {
        requiredRecipe = RecipeNameEnum.PumpkinMine;
        isEnabled = false;
        isPrevented = false; 
    }

    protected override void PreventAbility()
    {
        isPrevented=false; 
    }

    protected override void UnPreventAbility()
    {
       isPrevented= true;
    }

    private void Update()
    {
        if (isEnabled && !isPrevented)
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
