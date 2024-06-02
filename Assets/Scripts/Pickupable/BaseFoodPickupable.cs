using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseFoodPickupable : BasePickupItem,IPickupable
{
    
    private PickupableItemType type;

    private FoodInfo info;

    private void Awake()
    {
        type = PickupableItemType.Food;
        info = new FoodInfo(type);
    }

    public override void OnPickupCallback(ref List<FoodInfo> inventory)
    {
       InternalOnPickup(ref inventory);
        DeSpawnItem();
    }

    protected override void InternalOnPickup(ref List<FoodInfo> inventory)
    {
        inventory.Add(info);
    }
    
    protected override void spawnIteminWorld()
    {
        throw new System.NotImplementedException();
    }

    protected override void DeSpawnItem()
    {
        gameObject.SetActive(false);
        Destroy(gameObject);

    }

}
