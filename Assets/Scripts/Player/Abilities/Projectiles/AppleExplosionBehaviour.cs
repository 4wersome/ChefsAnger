using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleExplosionBehaviour : MonoBehaviour,IDamager
{
    [SerializeField]
    private float Damage = 5f;
    private DamageType type = DamageType.Explosive;

    private DamageContainer damageContainer = new DamageContainer();

    private void Start()
    {
        damageContainer.Damage = Damage;
        damageContainer.DamageType = type;
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            IDamageble Enemy = other.gameObject.GetComponent<IDamageble>();
            if (Enemy != null)
                Enemy.TakeDamage(damageContainer);

        }
    }
}
