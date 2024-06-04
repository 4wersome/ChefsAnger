using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour,IDamageble
{
    [SerializeField]
    private bool testbool;
    public void TakeDamage(DamageContainer damage)
    {
        Debug.Log("Taking damage:" + damage.Damage + "type :" + damage.DamageType);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
