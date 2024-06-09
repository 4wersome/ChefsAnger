using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityCheeseWheel : AbilityBase, IThrowAbility
{
    private const int animationLayer = 0;
    private const string animationStateName = "ThrowCheese";
    private const string animationStateTrigger = "AbilityCheeseWheel";
   
    [SerializeField]
    private ProjectileBase projectile;
    [SerializeField]
    private float throwForce = 10f;

    void Start()
    {
        projectile.gameObject.SetActive(false);
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
    }

    public void TriggerThrow()
    {
        //GameObject projectileRef = Instantiate(projectilePrefab, transform.position, transform.rotation);
        //Rigidbody projectileRigidBody;
        //projectileRef.TryGetComponent<Rigidbody>(out projectileRigidBody);
        //if(projectileRigidBody != null)
        //{
        //    projectileRigidBody.velocity = forward * 5;
        //    projectileRigidBody.rotation = rotation;
        //}
        projectile.EnableProjectile();
        projectile.Launch(throwForce, playerController.GetForward());
    }
}
