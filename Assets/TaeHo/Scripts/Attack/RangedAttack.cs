using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : AttackBase
{
    // 원거리 공격

    public GameObject projectilePrefab;  // 투사체 프리팹 (활, 총알 인지 모르니 투사체로 정의)
    public Transform firePoint;  // 투사체가 나갈 방향

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }


    protected override void Attack()
    {
        Debug.Log("공격 실행됨!");
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Projectile>().Initialize(damage, gameObject);

    }
}
