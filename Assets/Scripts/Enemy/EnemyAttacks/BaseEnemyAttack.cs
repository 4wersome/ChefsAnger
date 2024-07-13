using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAttack : MonoBehaviour {
    #region SerializedField
    [SerializeField] protected DamageContainer damageContainer;
    [SerializeField] protected bool hasCD;
    [SerializeField] protected float abilityCooldown;
    #endregion

    #region PrivateAttribute
    protected bool canAttack;
    private float elapsedTime;
    #endregion

    #region Mono
    private void Update() {
        if (hasCD && !canAttack) {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= abilityCooldown) {
                canAttack = true;
            }
        }
    }
    #endregion

    protected void ResetTimer() {
        elapsedTime = 0;
        canAttack = false;
    }
    
    public abstract void Attack();
}
