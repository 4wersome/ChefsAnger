using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Damager {
    
    [SerializeField] private LayerMask destroyLayer;
    
    private Rigidbody rigidbody;
    private Coroutine destroyOverTime;
    
    #region Mono
    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }

    protected override void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if (((1 << other.gameObject.layer) & destroyLayer.value) == 0) return;
        Destroy();
    }
    #endregion

    #region PublicMethods
    public Vector2 GetVelocity() => rigidbody.velocity;
    public void SetVelocity(Vector2 newVelocity) => rigidbody.velocity = newVelocity;

    public void Shoot(Vector3 startPos, float bulletSpeed, float bulletDuration) {
        gameObject.SetActive(true);
        transform.position = startPos;
        Shoot(bulletSpeed);
        destroyOverTime = StartCoroutine(DestroyOverTime(bulletDuration));
    }

    public void Shoot(float bulletSpeed) {
        Vector3 direction = Player.Get().transform.position - transform.position;
        rigidbody.velocity = direction.normalized * bulletSpeed;
        
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
