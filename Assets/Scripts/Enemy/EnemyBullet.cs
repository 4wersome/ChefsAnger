using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Damager {
    
    [SerializeField] private LayerMask destroyLayer;
    
    private Coroutine destroyOverTime;

    #region Property
    public Vector3 Velocity {
        get => GetComponent<Rigidbody>().velocity;
        set => GetComponent<Rigidbody>().velocity = value;
    }
    #endregion
    #region Mono
    private void Awake() {
        
    }

    protected override void OnTriggerEnter(Collider other) {
        Debug.Log("SONO ENTRATOOO");
        base.OnTriggerEnter(other);
        if (((1 << other.gameObject.layer) & destroyLayer.value) == 0) return;
        Destroy();
    }
    #endregion

    #region PublicMethods
    public void Shoot(Vector3 startPos, float bulletSpeed, float bulletDuration) {
        gameObject.SetActive(true);
        transform.position = startPos;
        Shoot(bulletSpeed);
        destroyOverTime = StartCoroutine(DestroyOverTime(bulletDuration));
    }

    public void Shoot(float bulletSpeed) {
        Vector3 direction = Player.Get().transform.position - transform.position;
        Velocity = direction.normalized * bulletSpeed;
        
        Quaternion rotation =  Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
    #endregion

    #region PrivateMethods


    private void Destroy() {
        gameObject.SetActive(false);
        if (destroyOverTime != null) StopCoroutine(destroyOverTime);
    }
    #endregion

    private IEnumerator DestroyOverTime(float bulletDuration) {
        yield return new WaitForSeconds(bulletDuration);
        Destroy();
    }
    
}
