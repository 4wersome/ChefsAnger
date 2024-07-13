using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, IPickupable
{
    [SerializeField]
    private float healAmount;

    #region ReferenceGetter
    public float HealAmount {
        get { return healAmount; }
    }
    #endregion

    #region PickUp Methods

    public PickupableItemType GetPickupableItemType(){
        return PickupableItemType.Potion;
    }

    public void OnPickup()
    {
       Debug.Log("Picked up Potion - Heal Power: " + healAmount + " HP");
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
