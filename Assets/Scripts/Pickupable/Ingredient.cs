using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientType
{
    GreenApple,
    RedApple,
    Cheese,
    Pumpkin,
    Wheel,
    Meat
}

public class Ingredient : MonoBehaviour, IPickupable
{
    [SerializeField]
    private IngredientType ingredientType;
    [SerializeField]
    private int number;

    #region ReferenceGetter
    public IngredientType IngredientType {
        get { return ingredientType; }
    }

    public int Number {
        get { return number; }
    }
    #endregion

    public Ingredient(IngredientType ingredientType, int number){
        this.ingredientType = ingredientType;
        this.number = number;
    }

    #region PickUp Methods

    public PickupableItemType GetPickupableItemType(){
        return PickupableItemType.Ingredient;
    }

    public void OnPickup()
    {
       Debug.Log("Picked up: " + ingredientType + " x" + number);
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