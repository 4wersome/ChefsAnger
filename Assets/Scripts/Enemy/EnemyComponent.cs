using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyComponent : MonoBehaviour, IDamageble {

    [SerializeField] private HealthModule healthModule;
    public Action OnSpawn;
    [SerializeField]
    private GameObject projectileToSpawn;
    
    public HealthModule HealthModule { get => healthModule; }
    public GameObject ProjectileToSpawn {get => projectileToSpawn;}
    
    private void Awake() {
        OnSpawn?.Invoke();
    }

    public void Spawn(Vector3 position) {
        transform.position = position;
        healthModule.Reset();
        OnSpawn?.Invoke();
    }

    public void Shoot() {
        if(projectileToSpawn) Instantiate(projectileToSpawn ,transform.position + (transform.forward * 2), transform.rotation);
    }
    
    public void TakeDamage(DamageContainer damage) {
        
        healthModule.TakeDamage(damage);
    }

}
