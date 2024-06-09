using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileMine : ProjectileBase
{
    [SerializeField]
    protected bool isActive;
    protected SphereCollider sphereCollider;
    protected BoxCollider boxCollider ;
    protected MeshRenderer Renderer;

    protected float timeToDespawn = .5f;
    protected float timer;
    protected bool timerIsActive;
    
    
    private void Start()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        Renderer = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider>();

        DisableProjectile();
    }

    public override void EnableProjectile()
    {
        base.EnableProjectile();
        Renderer.enabled = true;    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Enemy)
        {
            boxCollider.enabled = false;
            Renderer.enabled = false;
            sphereCollider.enabled = true;
            StartDespawnTimer();

       }

        
    }


    public override void DisableProjectile()
    {
        base.DisableProjectile();
        sphereCollider.enabled = false;
    }

    private void StartDespawnTimer()
    {
        timer = timeToDespawn;
        timerIsActive = true;
    }

    private void Update()
    {
        if (isActive )
         if (timerIsActive)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timerIsActive = false;
                DisableProjectile();
            }
        }
    }
}
