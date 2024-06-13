using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemyComponent : MonoBehaviour, IPoolRequester, IDamageble {

    #region privateAttribute
    private IEnemyAttack enemyAttackComponent;
    
    #endregion
    
    #region publicAttribute
    public Action OnSpawn;
    #endregion
    
    #region SerializedField
    [SerializeField] private HealthModule healthModule;
    [SerializeField] private PoolData[] enemyDrops;
    [SerializeField] [Range(0,1)] private float dropChance;
    #endregion

    #region Property
    public PoolData[] Datas { get => enemyDrops; }
    public HealthModule HealthModule { get => healthModule; }
    #endregion

    #region Mono
    private void Awake() {
        enemyAttackComponent = GetComponent<IEnemyAttack>();
        if(enemyDrops != null) OnSpawn += SpawnDrop;
    }
    #endregion
    
    #region PublicMethods
    public void Spawn(Vector3 position) {
        transform.position = position;
        healthModule.Reset();
        gameObject.SetActive(true);
        OnSpawn?.Invoke();
    }
    
    public void Spawn(Vector3 position, float levelDifficulty) {
        transform.position = position;
        
        healthModule.Reset(levelDifficulty);
        gameObject.SetActive(true);
        OnSpawn?.Invoke();
    }
    
    public void TakeDamage(DamageContainer damage) {
        healthModule.TakeDamage(damage);
    }
    
    public void Attack() {
        if (enemyAttackComponent != null) enemyAttackComponent.Attack();
        else Debug.Log("NO IEnemyAttack Component Found");
    }

    private void SpawnDrop() {
        if (Random.Range(0f, 1f) <= dropChance) {
            GameObject drop = enemyDrops[Random.Range(0, enemyDrops.Length)].Prefab;
            drop.transform.position = transform.position;
            drop.SetActive(true);
        }
    }
    #endregion
}
