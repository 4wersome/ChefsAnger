using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseProjectile : ProjectileBase
{
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
