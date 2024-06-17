using UnityEngine;

public class PlayerAppleThrow : AbilityBase
{
    private const string animationAppleThrowTriggerName = "ThrowApple";
    private const string animationisAttackingBoolName = "isAttacking";
    private const string animationState = "Apple";

    #region Serialized
    [SerializeField]
    private ProjectileApple projectile;
    [SerializeField]
    private float throwForce;
    [SerializeField]
    private bool canThrow;


    #endregion




    public bool IsEnabled { get { return isEnabled; } }



    #region mono

    private void Awake()
    {
        requiredRecipe = RecipeNameEnum.AppleGrenade;
        isEnabled = false;
        isPrevented = true;
    }
    private void Start()
    {
        projectile.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (IsEnabled)
        {

            if (InputManager.AbilityAppleThrowPressed() && !isPrevented && !playerController.AnimatorMgnr.GetAnimatorBool(animationisAttackingBoolName))
            {
                isPrevented = true;
                playerController.AnimatorMgnr.SetTriggerParameter(animationAppleThrowTriggerName);
                playerController.AnimatorMgnr.SetAnimatorBool(animationisAttackingBoolName, true);
                

            }
            if (canThrow && !projectile.ProjectileIsReady)
            {
               
                 
                projectile.EnableProjectile();
                SetProjectilePosition();
                if (projectile.StartPositionCalculated)
                    projectile.Launch(throwForce, playerController.GetForward());
                
                

            }

            isPrevented = projectile.isActiveAndEnabled;

        }

    }
    #endregion

    #region internal


    private void SetProjectilePosition()
    {
        //to be changed 
        if (!projectile.StartPositionCalculated)
        {
            projectile.transform.position = transform.position + new Vector3(0, 3, 0);
            projectile.SetStartPositionCalculated(true);
            playerController.AnimatorMgnr.SetAnimatorBool(animationisAttackingBoolName, false);

        }




    }
    #endregion
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
        isEnabled = true;
        isPrevented = false;
    }

}
