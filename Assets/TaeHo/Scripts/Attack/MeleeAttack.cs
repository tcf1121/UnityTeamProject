using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeAttack : AttackBase
{
    // 근거리 공격

    public float attackRange = 2f;  // 공격 범위
    public LayerMask targetMask; // 아군 기물, 적군 기물 Layer로 알 수 있도록 설정함

    private void Update()
    {
        // transform.position에서 시작해서 transform.forward 방향으로 쏘기
        Ray ray = new Ray(transform.position, transform.forward);

        // out RaycastHit hit : 맞은 오브젝트의 정보(위치, 콜라이더 등)를 저장
        if (Physics.Raycast(ray, out RaycastHit hit, attackRange, targetMask))
        {
            ITarget target = hit.collider.GetComponent<ITarget>();
            if (target != null) 
            {
                TryAttack();
            }
        }
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
                ManaRecovery(gameObject);
                Debug.Log($"{gameObject.name}이 {hit.collider.name}에게 {damage} 근거리 피해를 줌");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * attackRange);
    }

}
