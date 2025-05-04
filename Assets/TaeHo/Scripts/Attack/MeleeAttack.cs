using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    // �ٰŸ� ����

    public float attackRange = 2f;  // ���� ����
    public LayerMask targetMask; // �Ʊ� �⹰, ���� �⹰ Layer�� �� �� �ֵ��� ������

    private void Update()
    {
        // transform.position���� �����ؼ� transform.forward �������� ���
        Ray ray = new Ray(transform.position, transform.forward);

        // out RaycastHit hit : ���� ������Ʈ�� ����(��ġ, �ݶ��̴� ��)�� ����
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, targetMask))
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            if (target != null) 
            {
                TryAttack();
            }
        }

        // ó������ ������ ������ ���������� �ٰŸ��� ��� �ո� �ٶ� ���� ������ �ϱ� ������ �������� �ʾ���         
        //Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, targetMask);
        //foreach (Collider hit in hits)
        //{
        //    if (hit.gameObject == gameObject) 
        //    {
        //        continue;
        //    } 

        //    ITarget target = hit.GetComponent<ITarget>();
        //    if (target != null)
        //    {
        //        TryAttack();
        //        break; // �� ��� ����
        //    }
        //}
    }

    protected override void Attack()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, targetMask))
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Debug.Log($"{gameObject.name}�� {hit.collider.name}���� {damage} �ٰŸ� ���ظ� ��");
            }
        }

        //Collider[] hits = Physics.OverlapSphere(transform.position, attackRange, targetMask);

        //foreach (Collider hit in hits)
        //{
        //    if (hit.gameObject == gameObject)
        //    {
        //        continue;
        //    }

        //    ITarget target = hit.GetComponent<ITarget>();
        //    if (target != null)
        //    {
        //        target.TakeDamage(damage);
        //        Debug.Log($"{gameObject.name}�� {hit.name}���� {damage} �ٰŸ� ���ظ� ��");

        //    }
        //}
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * attackRange);
        // Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
