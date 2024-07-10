using System;
using UnityEngine;
using UnityEngine.Events;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    private const string AnimatorMovingBool = "isMoving";
    private const string animatorIsAttackingBoolName = "isAttacking";
    private const string animatorIdleStateName = "Idle";
    private const string animatorMovingStateName = "Movement";
    private const string isDeadAnimatorParameter = "PlayerDead";
    private const string PlayerPrefsGamepadEnabledName = "GamepadEnabled";

    private const string PlayerPrefsLastScoreName = "LastScore";
    private const string PlayerPrefCurrentScore = "Score";



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

    #region Audio

    private EventInstance playerFootsteps;


    private void UpdateSound()
    {
        if (AnimatorMgnr.GetAnimatorBool(AnimatorMovingBool)&& !IsDead)
        {
            
            PLAYBACK_STATE playbackstate;
            playerFootsteps.getPlaybackState(out playbackstate);

            if (playbackstate.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
            else
            {
                playerFootsteps.stop(STOP_MODE.IMMEDIATE);
            }
    }
    #endregion


    #region abilityMelee
    public Action MeleePrevented;
    #endregion
    #region abilityMove
    public Action MovePrevented;
    #endregion


    private UnityAction<GlobalEventArgs> EnableGamepad;

    #region mono
    private void Awake()
    {
        //Global Event to Cast in the UI to Enable the pad controls
        EnableGamepad += SetGamepadActive;
        OnDeath += PreventAllAbilities;
        OnDeath += InternalOnDeath;
        GlobalEventManager.AddListener(GlobalEventIndex.EnableGamepad, EnableGamepad);

        //search for all the abilities in the player 
        abilities = GetComponentsInChildren<AbilityBase>();

        foreach (AbilityBase ability in abilities)
        {
            ability.Init(this);
        }
        
        isGamepadActive = (PlayerPrefs.GetInt(PlayerPrefsGamepadEnabledName) != 0);

    }


    private void Start()
    {
        if (Audiomngr.Instance == null) return;
        playerFootsteps = Audiomngr.Instance.CreateEventInstance(FMODEventMAnager.Instance.PlayerRunning);
    }

    void FixedUpdate()
    {
        if (!isDead)
        {
            SetAnimatorMovement();
        }
            UpdateSound();

    }

    #endregion

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
        bool value = playerRigidBody.velocity.magnitude <= .5f ? false : true;

        AnimatorMgnr.SetAnimatorBool(AnimatorMovingBool, value);

    }
    #endregion

    #region Abilities

    private void PreventAllAbilities()
    {
        foreach (AbilityBase ability in abilities)
        {
            ability.StopAbility();
        }
    }

    public void UnpreventAllAbilities()
    {
        foreach (AbilityBase ability in abilities)
        {
            ability.ResumeAbility();
        }
    }
    public void UnlockAbility(RecipeNameEnum recipeName)
    {
        foreach (AbilityBase ability in abilities)
        {
            if (ability.RequiredRecipe != RecipeNameEnum.None && ability.RequiredRecipe == recipeName)
            {
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
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;


        }
    }

    private void InternalOnDeath()
    {
        SetIsKinematic(true);
        PlayerPrefs.SetInt(PlayerPrefsLastScoreName, PlayerPrefs.GetInt(PlayerPrefCurrentScore));
        PlayerPrefs.SetInt(PlayerPrefCurrentScore,0);

    }

    public void SetIsKinematic(bool value) => playerRigidBody.isKinematic = value;
    #endregion
}
