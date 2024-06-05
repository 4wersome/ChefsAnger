using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableBaseObject : MonoBehaviour, IDestroyable
{
    [SerializeField]
    private List<GameObject> prefabsToSpawn;
    [SerializeField]
    private HealthModule healthModule;
    [SerializeField]
    private float spawnRadius;

    #region ReferenceGetter
    public List<GameObject> PrefabsToSpawn {
        get { return prefabsToSpawn; }
    }
    #endregion

    public DestroyableBaseObject(List<GameObject> prefabsToSpawn){
        this.prefabsToSpawn = prefabsToSpawn;
    }

    #region Mono
    public void Start(){
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
    public void SpawnPrefabsInCircle(){
        for (int i = 0; i < prefabsToSpawn.Count; i++)
       {
           float angle = i * Mathf.PI * 2 / prefabsToSpawn.Count;
           float x = Mathf.Cos(angle) * spawnRadius;
           float z = Mathf.Sin(angle) * spawnRadius;
           Vector3 pos = transform.position + new Vector3(x, 0, z);
           float angleDegrees = -angle*Mathf.Rad2Deg;
           Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);
           Instantiate(prefabsToSpawn[i], pos, rot);
       }
    }
    #endregion
}
