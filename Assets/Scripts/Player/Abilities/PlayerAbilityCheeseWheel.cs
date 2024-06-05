using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityCheeseWheel : AbilityBase, IThrowAbility
{
    private const int animationLayer = 0;
    private const string animationStateName = "ThrowCheese";
    private const string animationStateTrigger = "AbilityCheeseWheel";
    [SerializeField] //for testing purposes
    private bool isUnlocked = false;
    [SerializeField]
    private GameObject projectilePrefab;

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

    public void ThrowProjectile(Vector3 forward, Quaternion rotation)
    {
        GameObject projectileRef = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody projectileRigidBody;
        projectileRef.TryGetComponent<Rigidbody>(out projectileRigidBody);
        if(projectileRigidBody != null)
        {
            projectileRigidBody.velocity = forward * 5;
            projectileRigidBody.rotation = rotation;
        }
    }
}
