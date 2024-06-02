using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    private  float damage = 1f;
    private DamageType type = DamageType.Melee;


    private void OnTriggerEnter(Collider other)
    {
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null) return;

        damageble.TakeDamage(type, damage);
    }

}
