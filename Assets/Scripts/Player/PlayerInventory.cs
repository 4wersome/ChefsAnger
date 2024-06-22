using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    #region PrivateMembers
    private Dictionary<IngredientType, int> IngredientInventory;
    [SerializeField]
    private List<Recipe> UncompletedRecipes;
    [SerializeField]
    private List<Recipe> CompletedRecipes;
    #endregion

    #region Events
    public Action<Ingredient> OnIngredientGot;
    public Action<Recipe> OnRecipeFound;
    public Action<Recipe> OnRecipeCompleted;
    public Action<Potion> OnPotionGot;
    public Action<Shield> OnShieldGot;
    #endregion

    private void Awake()
    {
        SetUpIngredients();
        UncompletedRecipes = new List<Recipe>();
        CompletedRecipes = new List<Recipe>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IPickupable pickupable = other.GetComponent<IPickupable>();

        if(pickupable != null){
            pickupable.OnPickup();
            PickItem(pickupable);
        }
    }

    private void PickItem(IPickupable item){

        switch (item.GetPickupableItemType()){

            case PickupableItemType.Ingredient:
                Ingredient ingredient = (Ingredient) item;
                PickIngredient(ingredient);
                break;

            case PickupableItemType.Recipe:
                Recipe recipe = (Recipe) item;
                AddRecipe(recipe);
                break;

            case PickupableItemType.Potion:
                Potion potion = (Potion) item;
                PickPotion(potion);
                break;

            case PickupableItemType.Shield:
                Shield shield = (Shield) item;
                PickShield(shield);
                break;
        }
    }

    #region Ingredients
    public void SetUpIngredients(){
        IngredientInventory = new Dictionary<IngredientType, int>();
        foreach(IngredientType type in Enum.GetValues(typeof(IngredientType))){
            IngredientInventory.Add(type, 0);
        }
    }

    public void PickIngredient(Ingredient ingredient){
        IngredientInventory[ingredient.IngredientType] += ingredient.Number;
        OnIngredientGot?.Invoke(ingredient);
        TryToCompleteRecipes();
    }

    public void ConsumeIngredient(Ingredient ingredient){
        if(IngredientInventory[ingredient.IngredientType] <= 0){
            return;
        }
        
        IngredientInventory[ingredient.IngredientType] -= ingredient.Number;

        if(IngredientInventory[ingredient.IngredientType] <= 0){
            IngredientInventory[ingredient.IngredientType] = 0;
        }
    }
    #endregion

    #region Recipes
    public void AddRecipe(Recipe recipe){

        if(CheckIfDuplicatedAbility(recipe.RecipeName)){
            recipe.ChangeIntoPowerUpRecipe();
        }

        UncompletedRecipes.Add(recipe);
        OnRecipeFound?.Invoke(recipe);
        TryToCompleteRecipes();
    }

    public void CompleteRecipe(Recipe recipe){
        CompletedRecipes.Add(recipe);
        UncompletedRecipes.Remove(recipe);
        OnRecipeCompleted?.Invoke(recipe);
    }

    private void TryToCompleteRecipes(){
        foreach(Recipe recipe in UncompletedRecipes){
            bool completed = CheckRecipeCompletition(recipe);
            if(completed){
                RemoveUsedIngredients(recipe);
                CompleteRecipe(recipe);
                break;
            }
        }
    }

    private bool CheckRecipeCompletition(Recipe recipe){
        foreach(Ingredient ingredient in recipe.RequiredIngredients){
            if(IngredientInventory[ingredient.IngredientType] < ingredient.Number){
                return false;
            }
        }
        return true;
    }

    private void RemoveUsedIngredients(Recipe recipe){
        foreach(Ingredient ingredient in recipe.RequiredIngredients){
            ConsumeIngredient(ingredient);
        }
    }

    private bool CheckIfDuplicatedAbility(RecipeNameEnum recipeName){
        if((int) recipeName > 0 && (int) recipeName <= (int) RecipeNameEnum.PumpkinMine){
            foreach(Recipe rec in CompletedRecipes){
                if(recipeName == rec.RecipeName){
                    return true;
                }
            }
            foreach(Recipe rec in UncompletedRecipes){
                if(recipeName == rec.RecipeName){
                    return true;
                }
            }
        }
        return false;
    }
    #endregion

    #region Temporary PowerUp
    public void PickPotion(Potion potion){
        OnPotionGot?.Invoke(potion);
    }
    
    public void PickShield(Shield shield){
        OnShieldGot?.Invoke(shield);
    }
    #endregion
}
