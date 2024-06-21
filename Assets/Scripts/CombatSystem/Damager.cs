using UnityEngine;

public class Damager : MonoBehaviour, IDamager {

    [SerializeField] protected string damagebleTag = "Player";

    [SerializeField]
    protected DamageContainer damageContainer;

    public DamageContainer DamageContainer {
        get => damageContainer;
        set => damageContainer = value;
    }

    protected virtual void OnTriggerEnter(Collider other) {
        DealDamage(other);
    }

    //protected virtual void DealDamage(Collider other) {
    //    if (!other.CompareTag(damagebleTag)) return;
    //    IDamageble damageble = other.GetComponent<IDamageble>();
    //    if (damageble == null) return;
    //    damageble.TakeDamage(damageContainer);
    //}



    protected virtual void DealDamage(Collider other) {
        if (other.gameObject.CompareTag(damagebleTag)) {
            Debug.Log("ddamage taken");
            IDamageble dmg = other.GetComponentInParent<IDamageble>();
            if (dmg != null) {
                dmg.TakeDamage(DamageContainer);
            }
        }
    }
}
