using Codice.Client.GameUI.Explorer;
using PlasticGui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class ProjectileApple : ProjectileBase
{
    protected SphereCollider sphereRadiusCollider;

    protected BoxCollider boxCollider;
    protected MeshRenderer meshRender;

    protected bool startPositionCalculated;

    protected bool timerIsAwake;
    protected float appleDespawnTime = 3f;
    protected float ActualDisableTime = 3.5f;
    protected float elapsedTime;
    public bool StartPositionCalculated
    { get { return startPositionCalculated; } }


    private void Awake()

    {
        rb = GetComponent<Rigidbody>();

        meshRender = GetComponentInChildren<MeshRenderer>();
        boxCollider = GetComponentInChildren<BoxCollider>();
        sphereRadiusCollider = gameObject.GetComponentInChildren<SphereCollider>();
    }

    #region overrides

    public override void Launch(float force, Vector3 direction)
    {
        Vector3 ThrowForce = direction * force;

        ThrowForce.y *= ThrowForce.y * 0.5f;

        if (!launch)
        {
            rb.AddForce(ThrowForce, ForceMode.Impulse);
            launch = true;
        }

    }
    public override void DisableProjectile()
    {
        base.DisableProjectile();
        startPositionCalculated = false;
        projectileIsReady = false;
        sphereRadiusCollider.enabled = false;

    }
    #endregion




    public void SetStartPositionCalculated(bool value)
    {
        projectileIsReady = value;
        startPositionCalculated = value;
        sphereRadiusCollider.enabled = false;
        meshRender.enabled = true;
    }





    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StartDespawnTimer();
        }

    }

    private void Explode()
    {
        meshRender.enabled = false;
        sphereRadiusCollider.enabled = true;

    }
    private void Update()
    {
        //a timer to  make the apple disappear before the trigger of the explosion collider 
        if (timerIsAwake)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > appleDespawnTime && meshRender.enabled)
            {
                Explode();
            }
            else if (elapsedTime > ActualDisableTime)
            {
                timerIsAwake = false;
                DisableProjectile();

            }
        }
    }

    #region DespanmTimer
    private void StartDespawnTimer()
    {
        elapsedTime = 0;
        timerIsAwake = true;

    }

    #endregion

}
