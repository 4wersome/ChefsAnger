using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Damager {
    [SerializeField] private float speed = 3;
    [SerializeField] [Range(0, 10)] private float lifeSpan;
    
    private Rigidbody rigidbody;
    
    #region Mono
    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
    }
    #endregion

    private void Start() {
        Shoot();
        if(lifeSpan > 0) Destroy(gameObject, lifeSpan);
    }

    #region PublicMethods
    public Vector2 GetVelocity() => rigidbody.velocity;
    public void SetVelocity(Vector2 newVelocity) => rigidbody.velocity = newVelocity;

    public void Shoot(Vector2 startPos) {
        transform.position = startPos;
        Shoot();
    }

    public void Shoot() {
        Vector3 playerPosition = Player.Get().transform.position;
        
        Vector3 direction = playerPosition - transform.position;
        rigidbody.velocity = direction.normalized * speed;
        
        Quaternion rotation =  Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }
    #endregion

    #region PrivateMethods
    protected override void DealDamage(Collider other) {
        base.DealDamage(other);
        //to implement the item pool
        Destroy(gameObject);
    }
    #endregion
}
