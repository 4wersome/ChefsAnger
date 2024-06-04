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
        OnSpawn?.Invoke();
    }

    public void Spawn(Vector3 position) {
        transform.position = position;
        healthModule.Reset();
        OnSpawn?.Invoke();
    }
    public void TakeDamage(DamageType type, float amount) {
        healthModule.OnDamageTaken?.Invoke(type, amount);
    }
}
