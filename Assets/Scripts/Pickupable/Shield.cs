using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IPickupable
{
    [SerializeField]
    private float shieldTime;

    #region ReferenceGetter
    public float ShieldTime {
        get { return shieldTime; }
    }
    #endregion

    #region PickUp Methods

    public PickupableItemType GetPickupableItemType(){
        return PickupableItemType.Shield;
    }

    public void OnPickup()
    {
       Debug.Log("Picked up Shield - Invulnerability Time: " + shieldTime + " Seconds");
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
