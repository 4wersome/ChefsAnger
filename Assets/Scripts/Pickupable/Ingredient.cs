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

    private void Awake()
    {
        pickupableType = PickupableItemType.Ingredient;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject otherObject = other.GetComponent<GameObject>();
        
    }

#region PickUp Methods
    public void OnPickup()
    {
       InternalOnPickup();
       DeSpawnItem();
    }

    private void InternalOnPickup()
    {
        // TODO
        Debug.Log("Picked up: " + ingredientType + " x" + number);
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