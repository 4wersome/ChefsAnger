using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.PoolingSystem;
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
    private float timeNoised;
    
    private int activeEnemies;
    private bool isSpawnActive;
    private bool canSpanwEnemy;
    private float levelDiffulty;
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
            if (enemyComponent!=null) {
                enemyComponent.HealthModule.OnDeath += EnemyKill;
                enemyComponent.OnSpawn += EnemySpawn;
            }
        }
    }

    public void StartSpawn(int nOfEnemyToSpawn, float waveDuration, float levelDiffulty) {
        this.nOfEnemyToSpawn = nOfEnemyToSpawn;
        timeBetweenEach = waveDuration / nOfEnemyToSpawn;
        this.levelDiffulty = levelDiffulty;
        isSpawnActive = true;
        activeEnemies = 0;
        ResetTimer();
        
        //if(spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        //spawnCoroutine = StartCoroutine(SpawnCoroutine(levelDiffulty));
    }
    #endregion

    #region Mono
    //DA FARE: spawncoroutine fatta con timer update invece che Coroutine
    private void FixedUpdate() {
        if (isSpawnActive) {
            if (elapsedTime >= timeNoised) {
                if (activeEnemies >= nOfEnemyToSpawn) isSpawnActive = false;
                else {
                    EnemyComponent enemy = Pooler.Instance.GetPooledObject(enemyPulled)?.GetComponent<EnemyComponent>();
                    RaycastHit hit;
                    if (enemy) {
                        Vector3 randomPoint = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                        Vector3 spawnPosition = Player.Get().transform.position + (randomPoint.normalized * Random.Range(30f, 40f));
#if DEBUG
                        Debug.Log("Searching for Position");
                        Debug.DrawRay(spawnPosition, Vector3.up*5f, Color.yellow, 3f);
#endif
                        //yield return waitForFixedUpdate;
                            
                        canSpanwEnemy = Physics.SphereCast(spawnPosition, 1f, Vector3.up, out hit, 10f);
                        
                        if (canSpanwEnemy) {
#if DEBUG
                            Debug.DrawRay(spawnPosition, Vector3.up*5f, Color.green, 15f);
                            Debug.Log("Position Found");
#endif
                            canSpanwEnemy = false;
                            enemy.Spawn(spawnPosition, levelDiffulty);
                            ResetTimer();
                        }
                        
                    }
                }
                
            }
            else {
                elapsedTime += Time.fixedDeltaTime;
            }
        }
    }
    #endregion
    
    #region PrivateMethods
    /*
    private IEnumerator SpawnCoroutine(float difficultyLevel) {
        WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        while (isSpawnActive) {
            if (activeEnemies >= nOfEnemyToSpawn) {
                isSpawnActive = false;
                yield break;
            }
            EnemyComponent enemy = Pooler.Instance.GetPooledObject(enemyPulled)?.GetComponent<EnemyComponent>();
            RaycastHit hit;
            Vector3 randomPoint, spawnPosition;
            if (enemy) {
                do {
                    randomPoint = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
                    spawnPosition = Player.Get().transform.position + (randomPoint.normalized * Random.Range(30f, 40f));
#if DEBUG
                    Debug.Log("Searching for Position");
                    Debug.DrawRay(spawnPosition, Vector3.up*5f, Color.yellow, 15f);
#endif
                    yield return waitForFixedUpdate;
                } while (Physics.SphereCast(spawnPosition, 1f, Vector3.up, out hit, 10f));
#if DEBUG
                Debug.DrawRay(spawnPosition, Vector3.up*5f, Color.green, 15f);
                Debug.Log("Position Found");
#endif
                enemy.Spawn(spawnPosition, difficultyLevel);
            }

            ResetTimer();
            yield return new WaitForSeconds(timeNoised);
        }
        StopCoroutine(spawnCoroutine);
    }
    */
    private void ResetTimer() {
        timeNoised = timeBetweenEach + Random.Range(-timeNoise, timeNoise);
        if (timeNoised <= 0f) timeNoised = 0.1f;
        elapsedTime = 0;
    }
    
    private void EnemyKill() => nOfKills++;
    
    private void EnemySpawn() => activeEnemies++;
    #endregion
}
