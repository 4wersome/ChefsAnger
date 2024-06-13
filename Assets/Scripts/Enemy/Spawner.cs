using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {
    #region PrivateAttribute
    [SerializeField]
    private PoolData enemyPulled;
    
    private int nOfEnemyToSpawn;
    private int nOfKills;
    
    private float timeBetweenEach;
   
    private float elapsedTime;
    private float timeNoise;
    
    private int activeEnemies;
    private bool isSpawnActive;
    private Coroutine spawnCoroutine;
    #endregion

    #region Property
    public int NumeberOfKills { get => nOfKills; }
    public bool IsSpawnActive { get => isSpawnActive; }
    #endregion

    #region PublicMethods
    public void Init(PoolData enemyPoolData, float noise) {
        this.enemyPulled = enemyPoolData;
        this.timeNoise = noise;
        isSpawnActive = false;
        nOfKills = 0;
        foreach (GameObject enemy in Pooler.Instance.GetObjectPool(enemyPulled)) {
            EnemyComponent enemyComponent = enemy.GetComponent<EnemyComponent>(); 
            enemyComponent.HealthModule.OnDeath += EnemyKill;
            enemyComponent.OnSpawn += EnemySpawn;
        }
    }

    public void StartSpawn(int nOfEnemyToSpawn, float waveDuration, float levelDiffulty) {
        this.nOfEnemyToSpawn = nOfEnemyToSpawn;
        timeBetweenEach = waveDuration / nOfEnemyToSpawn;
        
        isSpawnActive = true;
        activeEnemies = 0;
        elapsedTime = 0;
        
        if(spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnCoroutine(levelDiffulty));
    }
    #endregion
    
    #region PrivateMethods
    private IEnumerator SpawnCoroutine(float difficultyLevel) {
        while (isSpawnActive) {
            if (activeEnemies >= nOfEnemyToSpawn) {
                isSpawnActive = false;
                yield break;
            }
            EnemyComponent enemy = Pooler.Instance.GetPooledObject(enemyPulled).GetComponent<EnemyComponent>();
            if (enemy) {
                //get position. based on level increase enemyStats
                Vector3 randomPoint = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                //per ora 25, poi sarebbe da calcolare la dimensione dello schermo
                Vector3 spawnPosition = Player.Get().transform.position + (randomPoint.normalized * 25f);
                enemy.Spawn(spawnPosition, difficultyLevel);
            }

            float timeBetweenEachNoised = timeBetweenEach + Random.Range(-timeNoise, timeNoise);
            if (timeBetweenEachNoised <= 0f) timeBetweenEachNoised = 0.1f;
            yield return new WaitForSeconds(timeBetweenEachNoised);
        }
        StopCoroutine(spawnCoroutine);
    }
    private void EnemyKill() => nOfKills++;
    
    private void EnemySpawn() => activeEnemies++;
    #endregion
}
