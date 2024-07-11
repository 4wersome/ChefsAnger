using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pan : MonoBehaviour
{
    [SerializeField]
    private DamageContainer damage;

    public float DamageOutput { get { return damage.Damage; } }

    private UnityAction<GlobalEventArgs> addDmg;
    #region Mono
    private void Awake() {
        addDmg += GlobalIncreaseDamage;

        GlobalEventManager.AddListener(GlobalEventIndex.PlayerAttackUpdated, addDmg);
    }
    #endregion

    public void IncreaseDamage(int amount){
        damage.Damage += amount;
    }

    public void GlobalIncreaseDamage(GlobalEventArgs message)
    {
        GlobalEventArgsFactory.PlayerAttackUpdatedParser(message, out float attackDmg);
        damage.Damage += attackDmg;
    }
    private void OnTriggerEnter(Collider other) {
        //Debug.Log("collider found");
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null)
        {
            //Debug.Log("Damageble not found ");
            return;
        }
        
        Audiomngr.Instance.PlayeOneShot(FMODEventMAnager.Instance.panOnHit, transform.position);
        damageble.TakeDamage(damage);
    }

}
