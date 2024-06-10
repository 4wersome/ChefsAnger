using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    [SerializeField]
    private DamageContainer damage;

    #region Mono
    private void Awake() {
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider found");
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null)
        {
            Debug.Log("Damageble not found ");
            return;
        }
        damageble.TakeDamage(damage);
    }

}
