using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour {
    [SerializeField] protected float damage;
    [SerializeField] protected string damagebleTag = "Player";

    protected void OnTriggerEnter(Collider other) {
        DealDamage(other);
    }

    protected virtual void DealDamage(Collider other) {
        if (!other.CompareTag(damagebleTag)) return;
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null) return;
        damageble.TakeDamage(DamageType.Ranged, damage);
    }
}
