using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : MonoBehaviour,IDamageble
{
    public void TakeDamage(DamageType type, float amount)
    {
        Debug.Log("Taking damage:" + amount + "type :" + type);
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
