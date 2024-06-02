using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    private List<FoodInfo> FoodInventory;


    // Start is called before the first frame update
    private void Awake()
    {
        FoodInventory = new List<FoodInfo>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }



    private void OnTriggerEnter(Collider other)
    {
        IPickupable item = other.GetComponent<IPickupable>();
        if (item != null)
        {
            item.OnPickupCallback(ref FoodInventory);
        }
        Debug.Log("item picked up: " + FoodInventory[0].ItemType);

    }

    public void TakeDamage(DamageType type, float amount)
    {
        throw new System.NotImplementedException();
    }
}
