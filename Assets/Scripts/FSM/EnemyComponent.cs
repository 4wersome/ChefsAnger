using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour, IDamageble {

    [SerializeField] private float maxHp;
    private float hp;

    private bool isDead;
    
    
    public Action OnSpawn;
    public Action<float> OnTakeDamage;
    public Action OnDeath;
    
    public float Hp {
        get => hp;
        set {
            hp = Math.Clamp(value, 0, maxHp);
            if (hp <= 0) IsDead = true;
        }
    }
    public bool IsDead {
        get {
            return isDead;
        }
        set {
            isDead = value;
            if(value) OnDeath?.Invoke();
            else OnSpawn?.Invoke();
            
            //playerVisual.SetAnimatorParameter(isDeadAnimatorParameter, value);
        }
    }
    
   
    
    
    private void Awake() {
        hp = maxHp;
    }

    public void TakeDamage(DamageType type, float amount) {
        OnTakeDamage?.Invoke(amount);
        Hp -= amount;
    }
}
