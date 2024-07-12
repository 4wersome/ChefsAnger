using System;
using Utility.PoolingSystem;

namespace SerializedClass.SerializedPoolClass {
    [Serializable]
    public class EnemyTypePoolData {
        public PoolData enemyPoolType;
        public int levelStartSpawn;
        public int levelStopSpawn;

        public bool CanSpawn(int level) {
            return level >= levelStartSpawn && (levelStopSpawn <= 0 || level < levelStopSpawn);
        }
    }
    
}