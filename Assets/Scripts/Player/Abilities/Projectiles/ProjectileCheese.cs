using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileCheese : ProjectileBase
{
    [SerializeField]
    private float projectileDespawnTime = 2f;
    [SerializeField]
    private DamageContainer damage;

    private bool despawnTimerRunning = false;
    private float despawnTimerElapsedTime = 0f;
    private float projectileCurrentLifetime = 0f;
    private float projectileLifetimeMax = 2f;

    private static ProjectileCheese instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (despawnTimerRunning)
        {
            despawnTimerElapsedTime += Time.deltaTime;
            if (despawnTimerElapsedTime > projectileDespawnTime)
            {
                Debug.Log("despawn timer elapsed, disabling projectile");
                despawnTimerRunning = false;
                DisableProjectile();
                return;
            }
        }

        if (gameObject.activeSelf)
        {
            projectileCurrentLifetime += Time.deltaTime;
            if (projectileCurrentLifetime > projectileLifetimeMax)
            {
                Debug.Log("projectile lifetime exceeded max, starting despawn timer");
                despawnTimerRunning = true;
            }
        }
    }

    public override void Launch(float force, Vector3 direction)
    {
        //base.Launch(force, direction);
        if (!launch)
        {
            Vector3 ThrowForce = direction * force;
            rb.AddForceAtPosition(ThrowForce, this.transform.position, ForceMode.Impulse);
            launch = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int) Layers.Enemy) {
            StartDespawnTimer();
        }

        IDamageble damageble = collision.gameObject.GetComponent<IDamageble>();
        if (damageble == null)
        {
            Debug.Log("Damageble not found ");
            return;
        }
        damageble.TakeDamage(damage);
    }

    private void StartDespawnTimer()
    {
        despawnTimerRunning = true;
        despawnTimerElapsedTime = 0f;
    }

    public override void EnableProjectile()
    {
        base.EnableProjectile();
        projectileCurrentLifetime = 0f;
    }
    public override void DisableProjectile()
    {
        base.DisableProjectile();
        projectileCurrentLifetime = 0f;
    }

}
