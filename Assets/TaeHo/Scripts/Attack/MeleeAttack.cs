using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    // �ٰŸ� ����

    public float attackRange = 2f;
    public LayerMask targetMask;

    private void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject) 
            {
                continue;
            } 

            ITarget target = hit.GetComponent<ITarget>();
            if (target != null)
            {
                TryAttack();
                break; // �� ��� ����
            }
        }
    }

    protected override void Attack()
    {

        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, targetMask);

        foreach (Collider hit in hits)
        {
            if (hit.gameObject == gameObject)
            {
                continue;
            }

            ITarget target = hit.GetComponent<ITarget>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log($"{gameObject.name}�� {hit.name}���� {damage} �ٰŸ� ���ظ� ��");

            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
