using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackBase
{
    // 원거리 공격

    public GameObject projectilePrefab;  // 투사체 프리팹 (활, 총알 인지 모르니 투사체로 정의)
    public Transform firePoint;  // 투사체가 나갈 방향

    public float attackRange;  

    public LayerMask targetLayer;


    private void Update()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, attackRange, targetLayer);

        if (targets.Length > 0)
        {
            TryAttack();
        }
    }

    protected override void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Projectile>().Initialize(damage, gameObject);
        ManaRecovery(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
