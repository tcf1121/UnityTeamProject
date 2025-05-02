using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackBase
{
    // ���Ÿ� ����

    public GameObject projectilePrefab;  // ����ü ������ (Ȱ, �Ѿ� ���� �𸣴� ����ü�� ����)
    public Transform firePoint;  // ����ü�� ���� ����

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    protected override void Attack()
    {
        Debug.Log("���� �����!");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Projectile>().Initialize(damage, gameObject);

    }
}
