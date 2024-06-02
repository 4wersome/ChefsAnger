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
    private PickupableItemType pickupableType;

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

    private void Awake()
    {
        pickupableType = PickupableItemType.Ingredient;
    }

#region PickUp Methods
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