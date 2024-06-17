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
    public PoolData Key;
    public int Value;
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
    
    [SerializeField][Range(1, 10)] private float levelDifficultyGrowth;
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
                poolData[i] = enemyTypes[i].Key;
            }
            
            return poolData;
        }
    }
    #endregion

    #region Mono
    private void Start() {
        spawners = new Spawner[enemyTypes.Length];
        for (int i = 0; i < enemyTypes.Length; i++) {
            spawners[i] = gameObject.AddComponent<Spawner>();
            spawners[i].Init(enemyTypes[i].Key, timeNoise);
            //Debug.Log( i + enemyTypes[i].Key.PoolKey);

          
        }
        elapsedTime = safeDuration;
        waveStatus = startingWaveStatus;
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
        //in base al livello decidi quanti nemici spawnare e di che tipo
        level++;
        float fuzzyDifficulty = level * levelDifficultyGrowth * 0.5f;
        
        //ogni x aumenta il numero di nemici da spawnare
        numberOfEnemy += (int)fuzzyDifficulty;
        //Debug.Log("Level: " + level + ", Difficulty: " + fuzzyDifficulty);
        //decidi, in base alla difficolta, quale e quanti spawner prendere
        for (int i = 0; i < enemyTypes.Length; i++) {
            if (enemyTypes[i].Value <= fuzzyDifficulty) {
                //Debug.Log("Start Spawn " + enemyTypes[i].Key.PoolKey);
                spawners[i].StartSpawn(numberOfEnemy, waveDuration, fuzzyDifficulty );
            }
        }
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
