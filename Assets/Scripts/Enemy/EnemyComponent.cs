using System;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.PoolingSystem;
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
#if DEBUG
        healthModule.OnDamageTaken += (damageContainer) => Debug.Log(gameObject.name + " is being attacked");
#endif
        enemyAttackComponent = GetComponent<IEnemyAttack>();
        if(enemyDrops != null) healthModule.OnDeath += SpawnDrop;
        healthModule.OnDeath += InternalOnDeath;
    }
    #endregion
    
    #region PublicMethods
    public void Spawn(Vector3 position) {
        transform.position = position;
        gameObject.SetActive(true);
        OnSpawn?.Invoke();
    }
    
    public void Spawn(Vector3 position, float levelDifficulty) {
        healthModule.Reset(levelDifficulty);
        Spawn(position);
    }
    
    public void TakeDamage(DamageContainer damage) {
        healthModule.TakeDamage(damage);
    }
    
    public void Attack() {
#if DEBUG
        //Debug.Log(gameObject.name + " is attacking player");
#endif
        if (enemyAttackComponent != null) enemyAttackComponent.Attack();
        //else Debug.Log("NO IEnemyAttack Component Found");
    }

    private void SpawnDrop() {
        if (enemyDrops.Length > 0 && Random.Range(0f, 1f) <= dropChance) {
            PoolData data = enemyDrops[Random.Range(0, enemyDrops.Length)];
            GameObject drop = Pooler.Instance.GetPooledObject(data);
            if (drop) {
                drop.transform.position = transform.position;
                drop.SetActive(true);
                Debug.Log("Spawn Drop");

            }
            
        }
    }
    #endregion

    
    private void InternalOnDeath()
    {
        
        Audiomngr.Instance.PlayeOneShot(FMODEventMAnager.Instance.EnemyMeleeOnDeath, transform.position);

        newScoreManager.instance.AddPoint();
        
    }
}
