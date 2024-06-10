using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private const string AnimatorMovingBool = "isMoving";
    private const string isDeadAnimatorParameter = "PlayerDead";
    private const string animatorIsAttackingBoolName = "isAttacking";
    private const string animatorIdleStateName = "Idle";
    private const string animatorMovingStateName = "Movement";


    #region Serialized
    [SerializeField]
    Player player;

    [SerializeField]
    private Rigidbody playerRigidBody;

    [SerializeField]
    public PlayerAnimatorMngr AnimatorMgnr;
    
    [SerializeField]
    private bool isGamepadActive;
    #endregion

    private AbilityBase[] abilities;
    public bool IsGamepadActive { get { return isGamepadActive; } }

    #region abilityMelee
    public Action MeleePrevented;
    #endregion
    #region abilityMove
    public Action MovePrevented;
    #endregion

    private UnityAction<GlobalEventArgs> EnableGamepad;
    private void Awake()
    {
        //Global Event to Cast in the UI to Enable the pad controls
        EnableGamepad += SetGamepadActive;
        GlobalEventManager.AddListener(GlobalEventIndex.EnableGamepad, EnableGamepad);

        //search for all the abilities in the player 
        abilities = GetComponentsInChildren<AbilityBase>();

        foreach (AbilityBase ability in abilities)
        {
            ability.Init(this);
        }
    }

    void Update()
    {
        SetAnimatorMovement();

    }

    #region public
    public Vector3 GetForward()
    {
        return playerRigidBody.transform.forward;
    }

    public Vector3 GetTransformRight()
    {
        return playerRigidBody.transform.right;
    }
    public void SetForward(Vector3 forward)
    {
        playerRigidBody.transform.forward = forward;
    }
    public void SetVelocity(float X, float Z)
    {
        Vector3 velocity = playerRigidBody.velocity;
        velocity.z = Z;
        velocity.x = X;
        playerRigidBody.velocity = velocity;
    }
    public void SetVelocity(Vector3 velocity)
    {
        playerRigidBody.velocity = velocity;
    }    

    public void SetRotation(Quaternion rot)
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 10f);

    }
    #endregion

    #region Internal
    private void SetGamepadActive(GlobalEventArgs message)
    {
        GlobalEventArgsFactory.EnableGamepadParses(message, out isGamepadActive);
    }

    private void SetAnimatorMovement()
    {
        bool value = playerRigidBody.velocity == Vector3.zero ?  false :  true;

        AnimatorMgnr.SetAnimatorBool(AnimatorMovingBool, value);

    }
    #endregion

    #region Abilities
    public void UnlockAbility(RecipeNameEnum recipeName){
        foreach(AbilityBase ability in abilities){
            if(ability.RequiredRecipe != RecipeNameEnum.None && ability.RequiredRecipe == recipeName){
                ability.UnlockAbility();
                break;
            }
        }
    }
    #endregion

    #region HealthModule
    private bool isDead;
    public Action<DamageContainer> OnDamageTaken;
    public Action OnDeath;
    public bool IsDead {
        get {
            return isDead;
        }
        set {
            isDead = value;
            AnimatorMgnr.SetAnimatorBool(isDeadAnimatorParameter, value);
        }
    }
    #endregion
}
