using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSBullet : MonoBehaviour
{
    [SerializeField] private float lifetime = 3f;
    [SerializeField] Rigidbody rd;
    public int attackPoint;


    void Start()
    {
        Destroy(gameObject, lifetime);
    }



    private void Update()
    {
        if (rd.velocity.magnitude > 1)
        {
            transform.forward = rd.velocity;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {

        IInteractionYS damagable = collision.gameObject.GetComponent<IInteractionYS>();
        if (damagable != null)
        {
            Debug.Log($"{collision.gameObject.name} ���� �Ѿ��� ������ ���� �� �ִ� ������Ʈ�� ������.");
            Attack(damagable);

        }
        else
        {
            Debug.Log($"{collision.gameObject.name} ���� �ش� ���� ������Ʈ���� ������ ���� �� �ִ� ������Ʈ�� ����");
        }

    }

    private void Attack(IInteractionYS damagable)
    {
        damagable.IInteractionYS(gameObject, attackPoint);
    }

}
