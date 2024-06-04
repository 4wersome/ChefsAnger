using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyComponent : MonoBehaviour, IDamageble {

    [SerializeField] private HealthModule healthModule;
    public Action OnSpawn;
   
    public HealthModule HealthModule { get => healthModule; }
    
    private void Awake() {
        healthModule.Reset();
        OnSpawn?.Invoke();
    }

    public void TakeDamage(DamageType type, float amount) {
        healthModule.OnDamageTaken?.Invoke(type, amount);
    }
}
