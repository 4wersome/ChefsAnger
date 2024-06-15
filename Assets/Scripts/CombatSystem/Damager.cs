using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Damager : MonoBehaviour, IDamager {
    
    [SerializeField] protected string damagebleTag = "Player";
    private DamageContainer damageContainer;

    public DamageContainer DamageContainer {
        get => damageContainer;
        set => damageContainer = value;
    }
    
    protected virtual void OnTriggerEnter(Collider other) {
        
        DealDamage(other);
    }

    protected virtual void DealDamage(Collider other) {
        if (!other.CompareTag(damagebleTag)) return;
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null) return;
        damageble.TakeDamage(damageContainer);
    }
}
