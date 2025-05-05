using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RangedAttack_s : AttackBase_s
{
    // ���Ÿ� ����

    public GameObject projectilePrefab;  // ����ü ������ (Ȱ, �Ѿ� ���� �𸣴� ����ü�� ����)

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
