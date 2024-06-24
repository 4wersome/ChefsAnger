using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using Unity.Properties;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
public enum WaveStage { Safe, EnemyAttack }

[Serializable]
public class DictionaryItem {
    public PoolData ObjectPoolType;
    public int levelStartSpawn;
    public int levelStopSpawn;

    public bool CanSpawn(int level) {
        return level >= levelStartSpawn && (levelStopSpawn <= 0 || level < levelStopSpawn);
    }
}
public class WaveManager : MonoBehaviour, IPoolRequester {

    #region SerializedField
    //[SerializeField] private PoolData[] enemyTypes;
    [SerializeField][Tooltip("List of different type of enemy and the level they starts to spawn")] 
    private DictionaryItem[] enemyTypes;
    
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
            PoolData[] poolData = new PoolData[enemyTypes.Length];
            for (int i = 0; i < enemyTypes.Length; i++) {
                poolData[i] = enemyTypes[i].ObjectPoolType;
            }
            
            return poolData;
        }
    }
    #endregion

    #region Mono
    private void Start() {
        if (spawners == null) {
            spawners = new Spawner[enemyTypes.Length];
            for (int i = 0; i < enemyTypes.Length; i++) {
                spawners[i] = gameObject.AddComponent<Spawner>();
                spawners[i].Init(enemyTypes[i].ObjectPoolType, timeNoise);
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
            case WaveStage.Safe:
                if (elapsedTime > safeDuration) {
                    StartNextWave();
                }
                break;
            case WaveStage.EnemyAttack:
                if (elapsedTime > waveDuration) {
                    StartSafeZone();
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

    private void StartWaveStage() {
        switch (waveStatus) {
            case WaveStage.EnemyAttack:
                
                StartNextWave();
                break;
            case WaveStage.Safe:
                break;
            default:
#if DEBUG
                Debug.Log("Wave Stage Missing Initial SetUp");
#endif
                break;
        }
    }

    private int NumberOfActiveSpawn(int levelDifficulty) {
        int nActiveSpawners = 0;
        foreach (DictionaryItem enemyType in enemyTypes) {
            if (enemyType.CanSpawn(levelDifficulty)) nActiveSpawners++;
        }

        return nActiveSpawners;
    }
    private void StartSafeZone() {
        waveStatus = WaveStage.Safe;
        ResetTimer();
        //eventuale reward per aver ucciso tutti i nemici prima della durata della wave
    }

    private void ResetTimer() {
        elapsedTime = 0;
        //Debug.Log("Wave State: " + waveStatus);
    }
    #endregion
}
