using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeleeAttack_s : AttackBase_s
{
    // �ٰŸ� ����

    protected override void Attack()
    {
        TakeDamage();
        //Debug.Log($"{gameObject.name}�� {target.name}���� {damage} �ٰŸ� ���ظ� ��");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
    }

}
