using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZombie : Zombie
{

    [SerializeField] protected override string Name { get ; set; }
    [SerializeField] protected override int CurrentHealth { get ; set ; }
    [SerializeField] protected override int MaxHealth { get; set; }
    [SerializeField] protected override int Power { get; set; }
    
  
    [SerializeField] protected override int Level { get; set; }
    [SerializeField] protected override int DropGold { get; set; }
    [SerializeField] protected override int DropExp { get; set; }


    [SerializeField] protected override float MoveSpeed { get; set; } = 1f; // 이동 속도

    [SerializeField] protected override float AttackRange { get; set; } = 2f; // 공격 범위
    [SerializeField] protected override float AttackSpeed { get; set; } = 1f; // 공격 속도

    [SerializeField] float rotateSpeed { get; set; } = 3f; // 적 기물 회전 속도


    [SerializeField] private float viewRadius; // 구 반지름
    [SerializeField] private float viewDistance; // 감지할 거리

    [SerializeField] private LayerMask allyplayer; // 감지 대상이 되는 아군 설정

    protected Transform currentTarget;

    void Attack() // 적 공격 모션
    {
       
    }

    void Start()
    {

    }

    private void FixedUpdate()
    {
        DetectAllyPlayer();
        MoveTarget();
    }

    void Update()
    {

    }


    void MoveTarget() // 아군 기물로 돌진
    {
        if (currentTarget == null) return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);
        Vector3 direction = (currentTarget.position - transform.position).normalized;

        if (distance > AttackRange) // 공격범위보다 너머에 있다면?
        {         
            transform.position += direction * MoveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); //  타겟 방향을 기준으로 y축 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed); // 목표 회전까지 부드럽게 회전

        }
        else 
        {
            Debug.Log("공격합니다");
            // 공격
            Attack();          
        }
    }



    void DetectAllyPlayer() // 아군 기물 감지 함수 구현
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, viewRadius, transform.forward, viewDistance, allyplayer);
       

        Transform closestTarget = null; // 가장 가까운 타겟 초기화
        float minDistance = Mathf.Infinity; // 최소거리를 초기화시켜 처음에 true로 만든다


        // 아군 기믹 감지
        foreach (RaycastHit hit in hits) 
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position); // 아군까지 거리를 젠다

            if (distance < minDistance) 
            {
                minDistance = distance; // 타겟 지정
                closestTarget = hit.transform; // 타겟의 위치(Transform)를 closestTarget에 저장한다
            }

        }


        if (closestTarget != null) 
        {
            currentTarget = closestTarget;
            Debug.Log("적을 감지했습니다");

        }
       
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, viewRadius);

        Vector3 direction = transform.forward * viewDistance;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + direction);
    }


}
