using System;
using UnityEngine;

[Serializable]
public class HealthModule
{

    #region SerializeField
    [SerializeField]
    private float maxHP;
    [SerializeField]
    private float defence;
    #endregion

    #region Events
    public Action<DamageContainer> OnDamageTaken;
    public Action OnDeath;
    #endregion

    #region PublicProperties
    public float MaxHP {
        get { return maxHP; }
    }
    public float CurrentHP {
        get { return currentHP; }
    }
    public bool IsDead {
        get { return currentHP <= 0; }
    }
    #endregion

    #region PrivateAttributes
    private bool invulnerable;
    private float currentHP;
    #endregion

    #region PublicMethods
    public void Reset () {
        currentHP = maxHP;
    }

    public void IncreaseMaxHP(int amount){
        maxHP += amount;
    }

    public void IncreaseDefence(float amount){
        defence += amount;
    }

    public void SetInvulnerable (bool value) {
        invulnerable = value;
    }

    public void TakeDamage (DamageContainer damage) {
        if (IsDead || invulnerable) return;

        // Remove Defence from Damage
        float finalDamage = damage.Damage - defence;
        if(finalDamage <= 0) finalDamage = 0;

        currentHP -= finalDamage;
        OnDamageTaken?.Invoke(damage);
        if (currentHP > 0) return;
        OnDeath?.Invoke();
    }
    #endregion

}