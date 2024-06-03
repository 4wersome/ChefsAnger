using Codice.Client.Common.GameUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    #region SerializeFields
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float shield;
    #endregion

    #region PrivateMembers
    private float currentHP;
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
        playerInventory.OnRecipeCompleted += InternalOnRecipeCompleted;
    }
    #endregion

    #region Inventory
    public void InternalOnRecipeCompleted(Recipe recipe) {
            RecipeNameEnum recipeName = recipe.RecipeName;
            switch(recipeName){
                case RecipeNameEnum.LifeUp:
                    // Increment Player Max HP
                    // TODO: Add Health Module Management
                    maxHP++;
                    break;
                case RecipeNameEnum.DefenceUp:
                    // Increment Player Defence
                    // TODO: Create better Management
                    shield++;
                    break;
                default:
                    // Unlock Ability
                    playerController.UnlockAbility(recipeName);
                    break;
            }
            Debug.Log("Completed Recipe: " + recipeName);
    }
    #endregion

    public void TakeDamage(DamageType type, float amount)
    {
        throw new System.NotImplementedException();
    }
}
