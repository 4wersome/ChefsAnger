using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileMine : ProjectileBase
{
    
    protected bool isExploded;
    protected SphereCollider sphereCollider;
    protected BoxCollider boxCollider ;
    protected MeshRenderer Renderer;

    protected float timeToDespawn = .5f;
    protected float timer;
    protected bool timerIsActive;
    

    public bool IsExploded { get { return isExploded; } }
    private static ProjectileMine instance;
    #region Mono

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
    private void Start()
    {
       // rb.GetComponent<Rigidbody>();
        sphereCollider = GetComponentInChildren<SphereCollider>();
        Renderer = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider>();

        DisableProjectile();
    }

    private void Update()
    {
        if (isExploded)
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == (int)Layers.Enemy)
        {
            isExploded = true;
            boxCollider.enabled = false;
            Renderer.enabled = false;
            sphereCollider.enabled = true;
            StartDespawnTimer();


       }

        
    }

    #endregion


    #region Internal
    private void StartDespawnTimer()
    {
        timer = timeToDespawn;
        timerIsActive = true;
    }

    #endregion

    #region overrides
    public override void EnableProjectile()
    {
        base.EnableProjectile();
        boxCollider.enabled = true;
        Renderer.enabled = true;    
    }
    public override void DisableProjectile()
    {
        base.DisableProjectile();
        isExploded = false;
        sphereCollider.enabled = false;
    }



    #endregion


}
