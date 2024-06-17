using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityCheeseWheel : AbilityBase, IThrowAbility
{
    private const int animationLayer = 0;
    private const string animationStateName = "ThrowCheese";
    private const string animationStateTrigger = "AbilityCheeseWheel";
   
    [SerializeField]
    private ProjectileCheese projectile;
    [SerializeField]
    private float throwForce = 75f;

    private void Awake()
    {
        requiredRecipe = RecipeNameEnum.CheeseWheel;
        isPrevented = true;
        if (projectile == null) {
            Debug.LogWarning("Projectile reference is null");
        }
    }

    private void Start()
    {
        projectile.DisableProjectile();
    }

    void Update()
    {
        if (isEnabled) 
        {
            if (InputManager.AbilityCheeseWheelPressed() && !isPrevented && !playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName))
            {
                playerController.AnimatorMgnr.SetTriggerParameter(animationStateTrigger);

            }
            else if (playerController.AnimatorMgnr.CheckCurrentAnimationState(animationLayer, animationStateName) && isPrevented)
            {

            }

            isPrevented = projectile.isActiveAndEnabled;
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
        isEnabled = true;
        isPrevented = false;
    }

    public void TriggerThrow()
    {
        projectile.transform.position = playerController.transform.position;
        projectile.transform.rotation = Quaternion.LookRotation(playerController.GetForward());
        projectile.transform.Rotate(new Vector3(0, 0, 90));
        projectile.EnableProjectile();
        projectile.Launch(throwForce, playerController.GetForward());
    }
}
