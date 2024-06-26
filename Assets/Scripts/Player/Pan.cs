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

    public void IncreaseDamage(int amount){
        damage.Damage += amount;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collider found");
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null)
        {
            Debug.Log("Damageble not found ");
            return;
        }
        
        Audiomngr.Instance.PlayeOneShot(FMODEventMAnager.Instance.panOnHit, transform.position);
        damageble.TakeDamage(damage);
    }

}
