using UnityEngine;

namespace Utility.PoolingSystem {
    public class ManagePoolRequest : MonoBehaviour {
        private void Awake() {
            IPoolRequester[] requesters = GetComponentsInChildren<IPoolRequester>(true);
            if (requesters == null || requesters.Length == 0) return;
            foreach(IPoolRequester requester in requesters) {
                foreach(PoolData data in requester.Datas) {
                    Pooler.Instance.AddToPool(data);
#if DEBUG
                    Debug.Log("added " + data.PoolKey);
#endif
                }
            }
        }
    }
}
