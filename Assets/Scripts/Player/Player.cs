using Codice.Client.Common.GameUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    [SerializeField]
    private Dictionary<IngredientType, int> IngredientInventory;

    private List<Recipe> UncompletedRecipes;
    private List<Recipe> CompletedRecipes;

    // Start is called before the first frame update
    private void Awake()
    {
        SetUpIngredients();
        UncompletedRecipes = new List<Recipe>();
        CompletedRecipes = new List<Recipe>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        Ingredient ingredient = other.GetComponent<Ingredient>();
        if(ingredient != null){
            PickIngredient(ingredient);
            ingredient.OnPickup();
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
    }

    public void ConsumeIngredient(Ingredient ingredient){
        if(IngredientInventory[ingredient.IngredientType] == 0 || IngredientInventory[ingredient.IngredientType] <= 0){
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
        UncompletedRecipes.Add(recipe);
    }

    public void CompleteRecipe(Recipe recipe){
        CompletedRecipes.Add(recipe);
        UncompletedRecipes.Remove(recipe);
    }
    #endregion

    public void TakeDamage(DamageType type, float amount)
    {
        throw new System.NotImplementedException();
    }
}
