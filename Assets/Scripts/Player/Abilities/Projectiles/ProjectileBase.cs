using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ProjectileBase : MonoBehaviour
{
    
    protected Rigidbody rb;
    protected bool launch;
    protected bool projectileIsReady;

    public bool ProjectileIsReady
    { get { return projectileIsReady; } }

    void Awake()
    {
      
    }

    public virtual void EnableProjectile()
    {

        gameObject.SetActive(true);
    }

    public virtual void DisableProjectile()
    {

        gameObject.SetActive(false);
        launch = false;
    }

    public virtual void Launch(float force, Vector3 direction)
    {
        Vector3 ThrowForce = direction * force;

        if (!launch)
        {
            rb.AddForce(ThrowForce, ForceMode.Impulse);
            launch = true;
        }
    }

}
