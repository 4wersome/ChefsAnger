using System;
using System.Collections.Generic;
using UnityEngine;

public enum PickupableItemType
{
    Ingredient,
    Recipe,
    Potion,
    Shield
}

public interface IPickupable
{
    public void OnPickup();
    public void SpawnItemInWorld(Transform position);
    public void DeSpawnItem();
}