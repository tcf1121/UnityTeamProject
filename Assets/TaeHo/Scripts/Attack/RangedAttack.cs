using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackBase
{
    // ���Ÿ� ����

    public GameObject projectilePrefab;  // ����ü ������ (Ȱ, �Ѿ� ���� �𸣴� ����ü�� ����)
    public Transform firePoint;  // ����ü�� ���� ����

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
