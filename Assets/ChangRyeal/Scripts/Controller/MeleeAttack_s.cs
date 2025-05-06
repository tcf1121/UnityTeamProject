using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeAttack_s : AttackBase_s
{
    // 근거리 공격

    protected override void Attack()
    {
        TakeDamage();
        //Debug.Log($"{gameObject.name}이 {target.name}에게 {damage} 근거리 피해를 줌");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }

}
