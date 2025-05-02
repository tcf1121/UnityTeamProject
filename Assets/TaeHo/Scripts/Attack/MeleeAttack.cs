using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    // 근거리 공격

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
                break; // 한 대상만 공격
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
                Debug.Log($"{gameObject.name}이 {hit.name}에게 {damage} 근거리 피해를 줌");

            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

}
