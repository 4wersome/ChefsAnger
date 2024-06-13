using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
public enum WaveStage { Safe, EnemyAttack }
public class WaveManager : MonoBehaviour, IPoolRequester {

    #region SerializedField
    [SerializeField] private PoolData[] enemyTypes;
    [SerializeField][Tooltip("Final Level to reach")] private int victoryLevel;
    [SerializeField] private bool isInfinite; 
    
    [SerializeField][Tooltip("Time between the end of a wave and the start of a new one")] 
    private float safeDuration;
    [SerializeField] private float waveDuration;
    [SerializeField][Range(0f, 0.5f)][Tooltip("Time disturbance between each enemy spawn")] 
    private float timeNoise;
    
    [SerializeField][Range(1, 10)] private float levelDifficultyGrowth;
    [SerializeField] private int startingNOfEnemy;
    #endregion

    #region privateAttribute
    private int level;
    private Spawner[] spawners;
    private WaveStage waveStatus;
    private float elapsedTime;
    #endregion
    
    #region Property
    public PoolData[] Datas { get => enemyTypes; }
    #endregion

    #region Mono
    private void Awake() {
        spawners = new Spawner[enemyTypes.Length];
        for (int i = 0; i < enemyTypes.Length; i++) {
            spawners[i] = gameObject.AddComponent<Spawner>();
            spawners[i].Init(enemyTypes[i], timeNoise);
        }

        elapsedTime = 0;
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
                if (elapsedTime > waveDuration || !AreSpawnerActive()) {
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
        ResetTimer();
        waveStatus = WaveStage.EnemyAttack;
        //in base al livello decidi quanti nemici spawnare e di che tipo
        level++;
        float fuzzyDifficulty = level * levelDifficultyGrowth * 0.5f;
        
        //decidi, in base alla difficolta, quale e quanti spawner prendere
        
        //ogni x aumenta il numero di nemici da spawnare
        startingNOfEnemy += (int)fuzzyDifficulty;
        
        //StartSpawn()
        if (level % levelDifficultyGrowth == 0) {
            //ogni x livelli aumenti la difficolta
        }
    }

    private void StartSafeZone() {
        ResetTimer();
        waveStatus = WaveStage.Safe;
        //eventuale reward per aver ucciso tutti i nemici prima della durata della wave
    }

    private void ResetTimer() {
        elapsedTime = 0;
    }
    #endregion
}
