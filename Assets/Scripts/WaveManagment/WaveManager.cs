using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Utility.PoolingSystem;
using WaveManagment.SerializedPoolClass;
using Random = UnityEngine.Random;

public enum WaveStage { Safe, EnemyAttack, DestroyableSpawn }

public class WaveManager : MonoBehaviour, IPoolRequester {

    #region SerializedField
    [SerializeField] private PoolData[] destroyables;
    [SerializeField] private GameObject [] destroyableSpawnPositions;
    [SerializeField] [Range(1,5)] private int numberOfWave;
    [SerializeField] private int maxNumeberOfDestroyable;
    
    [SerializeField][Tooltip("List of different type of enemy and the level they starts to spawn")] 
    private EnemyTypePoolData[] enemyTypes;
    
    [SerializeField][Tooltip("Final Level to reach")] private int victoryLevel;
    [SerializeField] private bool isInfinite;
    [SerializeField] private WaveStage startingWaveStatus;
    
    [SerializeField][Tooltip("Time between the end of a wave and the start of a new one")] 
    private float safeDuration;
    [SerializeField] private float waveDuration;
    [SerializeField][Range(0f, 0.5f)][Tooltip("Time disturbance between each enemy spawn")] 
    private float timeNoise;
    
    [SerializeField][Range(1, 10)] private int levelDifficultyGrowth;
    [SerializeField][Tooltip("Number Of enemy at the first wave")] private int numberOfEnemy;
    #endregion

    #region privateAttribute
    private int level;
    private Spawner[] spawners;
    private WaveStage waveStatus;
    private float elapsedTime;
    #endregion
    
    #region Property
    public PoolData[] Datas {
        get {
            PoolData[] poolData = new PoolData[enemyTypes.Length+destroyables.Length];
            int i = 0;
            for (; i < enemyTypes.Length; i++) {
                poolData[i] = enemyTypes[i].enemyPoolType;
            }

            foreach (PoolData destroyable in destroyables) {
                poolData[i] = destroyable;
                i++;
            }
            return poolData;
        }
    }
    #endregion

    #region Mono
    private void Start() {
        if (maxNumeberOfDestroyable >= destroyables.Length) maxNumeberOfDestroyable = destroyables.Length;
        if (maxNumeberOfDestroyable < 0) maxNumeberOfDestroyable = 1;
        foreach (GameObject destroyableSpawnPosition in destroyableSpawnPositions) {
            destroyableSpawnPosition.SetActive(true);
        }
        if (spawners == null) {
            spawners = new Spawner[enemyTypes.Length];
            for (int i = 0; i < enemyTypes.Length; i++) {
                spawners[i] = gameObject.AddComponent<Spawner>();
                spawners[i].Init(enemyTypes[i].enemyPoolType, timeNoise);
                //Debug.Log( i + enemyTypes[i].Key.PoolKey);
            }
        }
        elapsedTime = 0;
        waveStatus = startingWaveStatus;
        StartWaveStage();
        //ResetTimer();
        
    }

    private void Update() {
        elapsedTime += Time.deltaTime;

        switch (waveStatus) {
            case WaveStage.EnemyAttack:
                if (elapsedTime > waveDuration) {
                    if (level % numberOfWave == 0) {
                        StartRespawnDestroyableZone();
                    }
                    else {
                        StartSafeZone();
                    }
                }
                break;
            
            case WaveStage.Safe:
                if (elapsedTime > safeDuration) {
                    StartNextWave();
                }
                break;
            
            case WaveStage.DestroyableSpawn:
                if (elapsedTime > safeDuration) {
                    StartNextWave();
                }
                break;
        }
    }
    #endregion

    #region PrivateMethods
    private bool AreSpawnerActive() {
        foreach (Spawner spawner in spawners) {
            if (spawner.IsSpawnActive) return true;
        }
        return false;
    }

    private void StartNextWave() {
        waveStatus = WaveStage.EnemyAttack;
        ResetTimer();
        level++;
        int fuzzyDifficulty = (int) (level * levelDifficultyGrowth * 0.5f);
        numberOfEnemy += fuzzyDifficulty;
        int nOfActiveSpawner = NumberOfActiveSpawn(fuzzyDifficulty);
        
        for (int i = 0; i < enemyTypes.Length; i++) {
            if (enemyTypes[i].CanSpawn(fuzzyDifficulty)) {
                spawners[i].StartSpawn(numberOfEnemy/nOfActiveSpawner, waveDuration, fuzzyDifficulty);
            }
        }
    }
    
    private void StartSafeZone() {
        waveStatus = WaveStage.Safe;
        ResetTimer();
        //eventuale reward per aver ucciso tutti i nemici prima della durata della wave
    }
    
    private void StartRespawnDestroyableZone() {
        waveStatus = WaveStage.DestroyableSpawn;
        RespawnDestroyable();
        ResetTimer();
        
        //eventuale reward per aver ucciso tutti i nemici prima della durata della wave
    }

    private int NumberOfActiveSpawn(int levelDifficulty) {
        int nActiveSpawners = 0;
        foreach (EnemyTypePoolData enemyType in enemyTypes) {
            if (enemyType.CanSpawn(levelDifficulty)) nActiveSpawners++;
        }

        return nActiveSpawners;
    }
    
    private void StartWaveStage() {
        switch (waveStatus) {
            case WaveStage.EnemyAttack:
                StartNextWave();
                break;
            
            case WaveStage.Safe:
                StartSafeZone();
                break;
            
            case WaveStage.DestroyableSpawn:
                StartRespawnDestroyableZone();
                break;
            
            default:
                StartSafeZone();
#if DEBUG
                Debug.LogWarning("Wave Stage Missing Initial SetUp");
#endif
                break;
        }
    }


    private void RespawnDestroyable() {
        List<GameObject> freePosition = new List<GameObject>();
        foreach (GameObject spawnPosition in destroyableSpawnPositions) {
            if(spawnPosition.activeSelf) freePosition.Add(spawnPosition);
        }
        
        int nOfItem = Math.Clamp(Random.Range(1, maxNumeberOfDestroyable), 0, freePosition.Count);
        
        for (int i = 0; i < nOfItem; i++) {
            DestroyableBaseObject destroyableBaseObject = Pooler.Instance.GetPooledObject(destroyables[Random.Range(0, destroyables.Length)])?.GetComponent<DestroyableBaseObject>();
            if (destroyableBaseObject) {
                GameObject spawnPos = freePosition[Random.Range(0, freePosition.Count)];
                destroyableBaseObject.SpawnObject(spawnPos.transform);
                freePosition.Remove(spawnPos);
                if(freePosition.Count<=0) return;
            }
        }
    }

    private void ResetTimer() {
        elapsedTime = 0;
        //Debug.Log("Wave State: " + waveStatus);
    }
    #endregion
}