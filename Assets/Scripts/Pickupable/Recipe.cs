using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RecipeNameEnum
{
    None,
    AppleGrenade,
    CheeseWheel,
    PumpkinMine,
    LifeUp,
    DefenceUp
}

public class Recipe : MonoBehaviour, IPickupable
{
    [SerializeField]
    private RecipeNameEnum recipeName;
    [SerializeField]
    private List<Ingredient> requiredIngredients;

    #region ReferenceGetter
    public RecipeNameEnum RecipeName {
        get { return recipeName; }
    }

    public List<Ingredient> RequiredIngredients {
        get { return requiredIngredients; }
    }
    #endregion

    private void Awake()
    {
        SetRequiredIngredients();
    }

    #region Required Ingredients
    public void SetRequiredIngredients(){
        requiredIngredients = new List<Ingredient>();

        switch(recipeName){
            case RecipeNameEnum.AppleGrenade:
                requiredIngredients.Add(new Ingredient(IngredientType.GreenApple, 2));
                requiredIngredients.Add(new Ingredient(IngredientType.RedApple, 1));
                break;

            case RecipeNameEnum.CheeseWheel:
                requiredIngredients.Add(new Ingredient(IngredientType.Cheese, 6));
                requiredIngredients.Add(new Ingredient(IngredientType.Wheel, 1));
                break;

            case RecipeNameEnum.PumpkinMine:
                requiredIngredients.Add(new Ingredient(IngredientType.Pumpkin, 5));
                break;

            case RecipeNameEnum.LifeUp:
                requiredIngredients.Add(new Ingredient(IngredientType.RedApple, 2));
                requiredIngredients.Add(new Ingredient(IngredientType.Meat, 1));
                break;

            case RecipeNameEnum.DefenceUp:
                requiredIngredients.Add(new Ingredient(IngredientType.Pumpkin, 2));
                requiredIngredients.Add(new Ingredient(IngredientType.Meat, 3));
                break;
        }
    }
    #endregion

    #region PickUp Methods
    public PickupableItemType GetPickupableItemType(){
        return PickupableItemType.Recipe;
    }

    public void OnPickup()
    {
       Debug.Log("Picked up: " + recipeName);
       foreach(Ingredient ingr in requiredIngredients){
            Debug.Log("Required Ingredient: " + ingr.IngredientType + " x" + ingr.Number);
       }
       DeSpawnItem();
    }
    
    public void SpawnItemInWorld(Transform position)
    {
        gameObject.SetActive(true);
    }

    public void DeSpawnItem()
    {
        gameObject.SetActive(false);
    }
    #endregion

}
