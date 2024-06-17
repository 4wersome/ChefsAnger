using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    private const string isDeadAnimatorParameter = "PlayerDead";

    #region SerializeFields
    [SerializeField]
    private HealthModule healthModule;
    [SerializeField]
    private float damageInvTime;
    [SerializeField]
    private float shieldInvTime;
    [SerializeField]
    private Pan playerWeapon;
    #endregion

    #region PrivateMembers
    private Coroutine invCoroutine;
    #endregion

    #region PublicEvents
    public Action<DamageContainer> onDamageTaken;
    #endregion

    #region StaticMembers
    private static Player instance;

    public static Player Get () {
        if (instance != null) return instance;
            instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    #endregion //StaticMembers

    #region PlayerReferences
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerInventory playerInventory;
    #endregion //PlayerReferences


    #region MonoCallbacks
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        if (instance != this) return;
        ResetHealth();
        healthModule.OnDamageTaken += InternalOnDamageTaken;
        healthModule.OnDeath += InternalOnDeath;
        playerInventory.OnRecipeCompleted += InternalOnRecipeCompleted;
        playerInventory.OnPotionGot += InternalOnPotionGot;
        playerInventory.OnShieldGot += InternalOnShieldGot;
    }
    #endregion

    #region HealthModule
    public void ResetHealth () {
        healthModule.Reset();
        NotifyHealthUpdatedGlobal();
        playerController.IsDead = false;

    }

    public void TakeDamage(DamageContainer damage) {
        healthModule.TakeDamage(damage);
    }

    public void InternalOnDamageTaken(DamageContainer container) {
        NotifyHealthUpdatedGlobal();
        onDamageTaken?.Invoke(container);
        playerController.OnDamageTaken?.Invoke(container);
        SetInvulnerable(damageInvTime);
    }

    public void InternalOnDeath() {
        playerController.IsDead = true;
       
        playerController.OnDeath?.Invoke();
        playerController.AnimatorMgnr.SetTriggerParameter(isDeadAnimatorParameter);
    }
    #endregion

    #region Inventory
    // Recipe Completition
    public void InternalOnRecipeCompleted(Recipe recipe) {
        RecipeNameEnum recipeName = recipe.RecipeName;
        switch(recipeName){
            case RecipeNameEnum.LifeUp:
                // Increment Player Max HP
                healthModule.IncreaseMaxHP(1);
                break;
            case RecipeNameEnum.DefenceUp:
                // Increment Player Defence
                healthModule.IncreaseDefence(1);
                break;
            case RecipeNameEnum.AttackUp:
                // Increment Player Offence
                playerWeapon.IncreaseDamage(1);
                break;
            default:
                // Unlock Ability
                playerController.UnlockAbility(recipeName);
                break;
        }
        Debug.Log("Completed Recipe: " + recipeName);
    }

    // Potion
    public void InternalOnPotionGot(Potion potion) {
        healthModule.HealDamage(potion.HealAmount);
        if(healthModule.CurrentHP == healthModule.MaxHP){
            Debug.Log("Full Healed!");
        }
        else{
            Debug.Log("Healed of: " + potion.HealAmount + "HP");
        }
    }

    // Shield
    public void InternalOnShieldGot(Shield shield) {
        SetInvulnerable(shield.ShieldTime);
        Debug.Log("Shield Activated for " + shield.ShieldTime + " seconds");
    }
    #endregion

    #region PrivateMethods
    private void SetInvulnerable (float invTime) {
        if (invCoroutine != null) {
            StopCoroutine(invCoroutine);
        }
        invCoroutine = StartCoroutine(InvulnerabilityCoroutine(invTime));
    }

    private void NotifyHealthUpdatedGlobal () {
        GlobalEventManager.CastEvent(GlobalEventIndex.PlayerHealthUpdated,
        GlobalEventArgsFactory.PlayerHealthUpdatedFactory(healthModule.MaxHP, healthModule.CurrentHP));
    }
    #endregion

    #region Coroutine
    private IEnumerator InvulnerabilityCoroutine (float invTime) {
        healthModule.SetInvulnerable(true);
        yield return new WaitForSeconds(invTime);
        healthModule.SetInvulnerable(false);
    }
    #endregion
}