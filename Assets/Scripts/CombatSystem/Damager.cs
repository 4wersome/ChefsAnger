using UnityEngine;

public class Damager : MonoBehaviour, IDamager
{

    [SerializeField] protected string damagebleTag = "Player";

    [SerializeField]
    private DamageContainer damageContainer;

    public DamageContainer DamageContainer
    {
        get => damageContainer;
        set => damageContainer = value;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {

        DealDamage(other);
    }

    //protected virtual void DealDamage(Collider other) {
    //    if (!other.CompareTag(damagebleTag)) return;
    //    IDamageble damageble = other.GetComponent<IDamageble>();
    //    if (damageble == null) return;
    //    damageble.TakeDamage(damageContainer);
    //}



    protected virtual void DealDamage(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            IDamageble dmg = other.GetComponentInParent<IDamageble>();
            if (dmg!=null)
            {
                dmg.TakeDamage(DamageContainer);
            }
        }
    }
}
