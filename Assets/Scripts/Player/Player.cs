using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageble
{
    private List<FoodInfo> FoodInventory;

    private static Player instance;
    public static Player Get () {
        if (instance != null) return instance;
        instance = GameObject.FindObjectOfType<Player>();
        return instance;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
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
