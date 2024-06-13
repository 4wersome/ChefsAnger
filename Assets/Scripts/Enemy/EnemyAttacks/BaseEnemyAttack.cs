using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEnemyAttack : MonoBehaviour {
    [SerializeField] protected DamageContainer damageContainer;
    public abstract void Attack();
}
