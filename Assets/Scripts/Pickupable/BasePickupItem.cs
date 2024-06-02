using log4net.Appender;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePickupItem : MonoBehaviour, IPickupable
{


  
    public abstract void OnPickupCallback(ref List<FoodInfo> inventory );

    protected  abstract void spawnIteminWorld();

    protected abstract void InternalOnPickup(ref List<FoodInfo> inventory);
    protected abstract void DeSpawnItem();
}
