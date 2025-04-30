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


    [SerializeField] protected override float MoveSpeed { get; set; } = 1f; // �̵� �ӵ�

    [SerializeField] protected override float AttackRange { get; set; } = 2f; // ���� ����
    [SerializeField] protected override float AttackSpeed { get; set; } = 1f; // ���� �ӵ�

    [SerializeField] float rotateSpeed { get; set; } = 3f; // �� �⹰ ȸ�� �ӵ�


    [SerializeField] private float viewRadius; // �� ������
    [SerializeField] private float viewDistance; // ������ �Ÿ�

    [SerializeField] private LayerMask allyplayer; // ���� ����� �Ǵ� �Ʊ� ����

    protected Transform currentTarget;

    void Attack() // �� ���� ���
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


    void MoveTarget() // �Ʊ� �⹰�� ����
    {
        if (currentTarget == null) return;

        float distance = Vector3.Distance(transform.position, currentTarget.position);
        Vector3 direction = (currentTarget.position - transform.position).normalized;

        if (distance > AttackRange) // ���ݹ������� �ʸӿ� �ִٸ�?
        {         
            transform.position += direction * MoveSpeed * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); //  Ÿ�� ������ �������� y�� ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed); // ��ǥ ȸ������ �ε巴�� ȸ��

        }
        else 
        {
            Debug.Log("�����մϴ�");
            // ����
            Attack();          
        }
    }



    void DetectAllyPlayer() // �Ʊ� �⹰ ���� �Լ� ����
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, viewRadius, transform.forward, viewDistance, allyplayer);
       

        Transform closestTarget = null; // ���� ����� Ÿ�� �ʱ�ȭ
        float minDistance = Mathf.Infinity; // �ּҰŸ��� �ʱ�ȭ���� ó���� true�� �����


        // �Ʊ� ��� ����
        foreach (RaycastHit hit in hits) 
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position); // �Ʊ����� �Ÿ��� ����

            if (distance < minDistance) 
            {
                minDistance = distance; // Ÿ�� ����
                closestTarget = hit.transform; // Ÿ���� ��ġ(Transform)�� closestTarget�� �����Ѵ�
            }

        }


        if (closestTarget != null) 
        {
            currentTarget = closestTarget;
            Debug.Log("���� �����߽��ϴ�");

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
