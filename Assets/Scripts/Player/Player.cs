using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    private List<Ingredient> IngredientInventory;
    private List<Recipe> UncompletedRecipes;
    private List<Recipe> CompletedRecipes;

    // Start is called before the first frame update
    private void Awake()
    {
        IngredientInventory = new List<Ingredient>();
        UncompletedRecipes = new List<Recipe>();
        CompletedRecipes = new List<Recipe>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void AddIngredient(Ingredient ingredient){
        IngredientInventory.Add(ingredient);
    }

    public void AddIngredients(List<Ingredient> ingredients){
        IngredientInventory.AddRange(ingredients);
    }

    public void AddRecipe(Recipe recipe){
        UncompletedRecipes.Add(recipe);
    }

    public void CompleteRecipe(Recipe recipe){
        CompletedRecipes.Add(recipe);
        UncompletedRecipes.Remove(recipe);
    }

    public void TakeDamage(DamageType type, float amount)
    {
        throw new System.NotImplementedException();
    }
}
