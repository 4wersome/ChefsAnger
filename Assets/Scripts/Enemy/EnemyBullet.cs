using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    [SerializeField] private float damage;
    [SerializeField] private string damagebleTag = "Player";
    [SerializeField] private float speed = 3;
    [SerializeField] [Range(0, 10)] private float lifeSpan;
    
    private Rigidbody rigidbody;
    
    #region Mono
    private void OnTriggerEnter(Collider other) {
        DealDamage(other);
    }
    

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
        GameObject player = Player.Get().gameObject;
        
        Vector2 positionDifference = player.transform.position - transform.position;
        rigidbody.velocity = positionDifference.normalized * speed;
        float atan2 = Mathf.Atan2(positionDifference.y, positionDifference.x);
        transform.rotation = Quaternion.Euler(0f, 0f, atan2 * Mathf.Rad2Deg);
    }
    #endregion

    #region PrivateMethods
    private void DealDamage(Collider other) {
        if (!other.CompareTag(damagebleTag)) return;
        IDamageble damageble = other.GetComponent<IDamageble>();
        if (damageble == null) return;
        damageble.TakeDamage(DamageType.Ranged, damage);
        /*sarebbe meglio l'item pool ma non penso di avere il tempo di implementarla. se mi avanza lo faccio.....
         in caso penso basti solo mettere il gameobject nel nemico (magari in una sotto classe di enemy component) e sfruttare "enable"*/
        Destroy(gameObject);
    }
    #endregion
}
