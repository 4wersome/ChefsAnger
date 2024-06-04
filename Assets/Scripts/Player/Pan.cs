using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [SerializeField]
    private DamageContainer damage;

    #region Mono
    private void Awake() {
        damage.DamageType = DamageType.Melee;
        damage.Damage = 1;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null) return;

        damageble.TakeDamage(damage);
    }

}
