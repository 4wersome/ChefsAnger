using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.PoolingSystem;

public class DestroyableBaseObject : MonoBehaviour, IDestroyable, IPoolRequester {
    [SerializeField]
    private PoolData[] prefabsToSpawn;
    [SerializeField]
    private HealthModule healthModule;
    [SerializeField]
    private float spawnRadius;

    #region ReferenceGetter
    /*public List<GameObject> PrefabsToSpawn {
        get { return prefabsToSpawn; }
    }*/
    public PoolData[] Datas { get => prefabsToSpawn; }
    #endregion

    /*
    public DestroyableBaseObject(List<GameObject> prefabsToSpawn){
        this.prefabsToSpawn = prefabsToSpawn;
    }*/

    #region Mono
    public void Awake(){
        healthModule.Reset();
        healthModule.OnDamageTaken += InternalOnDamageTaken;
        healthModule.OnDeath += InternalOnDeath;
    }
    #endregion

    #region HealthModule
    public void TakeDamage(DamageContainer damage){
        healthModule.TakeDamage(damage);
    }

    public void InternalOnDamageTaken(DamageContainer damage) {
        Debug.Log("Hitted! Lost " + damage.Damage + " points");
    }

    public void InternalOnDeath() {
        Destroy();
    }
    #endregion

    #region Destroy Methods
    public void Destroy()
    {
       Debug.Log("Destroyed!");
       SpawnPrefabsInCircle();
       DeSpawnItem();
    }

    public void DeSpawnItem()
    {
        gameObject.SetActive(false);
    }
    #endregion

    #region Internal Methods
    private void SpawnPrefabsInCircle(){
        for (int i = 0; i < prefabsToSpawn.Length; i++) {
            float angle = i * Mathf.PI * 2 / prefabsToSpawn.Length;
            float x = Mathf.Cos(angle) * spawnRadius;
            float z = Mathf.Sin(angle) * spawnRadius;
            Vector3 pos = transform.position + new Vector3(x, 0.5f, z);
            float angleDegrees = -angle * Mathf.Rad2Deg;
            Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
            GameObject destroyableToSpawn = Pooler.Instance.GetPooledObject(prefabsToSpawn[i]);

            if (destroyableToSpawn) {
                destroyableToSpawn.SetActive(true);
                destroyableToSpawn.transform.position = pos;
                destroyableToSpawn.transform.rotation = rot;
            }
        }
    }
    #endregion

    #region PublicMethods
    public void SpawnObject(Transform spawnTransform) {
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;
        healthModule.Reset();
        gameObject.SetActive(true);
    }
    #endregion
}
