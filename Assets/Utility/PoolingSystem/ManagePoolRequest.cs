using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagePoolRequest : MonoBehaviour
{
    private void Awake() {
        IPoolRequester[] requesters = GetComponentsInChildren<IPoolRequester>(true);
        if (requesters == null || requesters.Length == 0) return;
        foreach(IPoolRequester requester in requesters) {
            foreach(PoolData data in requester.Datas) {
                Pooler.Instance.AddToPool(data);
                Debug.Log("added " + data.PoolKey);
            }
        }
    }
}
