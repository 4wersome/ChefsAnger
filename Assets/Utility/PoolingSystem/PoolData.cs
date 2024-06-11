using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PoolData", menuName = "ObjectPooling/Pool", order = 1)]
public class PoolData : ScriptableObject {

    #region SerializedField
    [SerializeField]
    private string poolKey;
    [SerializeField]
    private GameObject prefab;
    [SerializeField]
    private int poolNumber;
    [SerializeField]
    private bool resizablePool;
    #endregion

    #region Property
    public string PoolKey {
        get { return poolKey; }
    }
    public GameObject Prefab {
        get { return prefab; }
    }
    public int PoolNumber {
        get { return poolNumber; }
    }
    public bool ResizablePool {
        get { return resizablePool; }
    }
    #endregion
}
