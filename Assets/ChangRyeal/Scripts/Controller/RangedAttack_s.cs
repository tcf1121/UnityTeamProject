using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangedAttack_s : AttackBase_s
{
    // 원거리 공격

    public GameObject projectilePrefab;  // 투사체 프리팹 (활, 총알 인지 모르니 투사체로 정의)

    protected override void Attack()
    {
        
        GameObject projectile = Instantiate(projectilePrefab, 
            new Vector3(transform.position.x, 1f, transform.position.z), transform.rotation);
        projectile.GetComponent<Projectile_s>().Initialize(damage, target.gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 3f);
    }

}
