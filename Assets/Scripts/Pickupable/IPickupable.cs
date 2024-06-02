using System;
using System.Collections.Generic;
using UnityEngine.Events;

public interface IPickupable
{
    public void OnPickupCallback(ref List<FoodInfo> inventory);

}

public enum PickupableItemType
{
    Food,
    Recipe
}


public struct FoodInfo
{
    public PickupableItemType ItemType;
    public bool UsedForRecipe;
    
    public FoodInfo(PickupableItemType itemType, bool usedforRecipe = false )
    {
        ItemType = itemType;
        UsedForRecipe = usedforRecipe;
       

    }
}
