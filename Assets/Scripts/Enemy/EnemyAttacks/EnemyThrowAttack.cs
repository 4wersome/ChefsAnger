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
        //transform.position + (transform.forward * 2)
        EnemyBullet bullet = Pooler.Instance.GetPooledObject(bullets[(int)EnemyBulletType.EnemyApple]).GetComponent<EnemyBullet>();
        if (bullet) {
            bullet.DamageContainer = damageContainer;
            bullet.Shoot(transform.position + (transform.forward * 2), bulletSpeed, bulletLifeSpan);
        }
    }
}