using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyBulletType {
    EnemyApple
}
public class EnemyThrowAttack : BaseEnemyAttack, IEnemyAttack, IPoolRequester {
    
    [SerializeField] 
    private PoolData[] bullets;
    [SerializeField] private float bulletSpeed = 3;
    [SerializeField] [Range(1,15)] private float bulletLifeSpan = 3;
    public PoolData[] Datas { get => bullets; }
    
    public override void Attack() {
        if (canAttack) {
            GameObject bulletGameObject = Pooler.Instance.GetPooledObject(bullets[(int)EnemyBulletType.EnemyApple]);
            if(!bulletGameObject) return;
            EnemyBullet bullet = bulletGameObject.GetComponent<EnemyBullet>();
            if (bullet) {
                bullet.DamageContainer = damageContainer;
                Debug.Log("proiettile spawnato");
                bullet.Shoot(gameObject.transform.position, bulletSpeed, bulletLifeSpan);
            }
            ResetTimer();
        }
    }
}