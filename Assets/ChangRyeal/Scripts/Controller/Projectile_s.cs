using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_s : MonoBehaviour
{
    // ����ü
    public float projectileSpeed = 1f; // ����ü �ӵ�

    private int projectileDamage = 0;  // ����ü ������

    private GameObject target;  // ��� ĳ���� ���̸� ��


    public void Initialize(int damage, GameObject target)
    {
        this.projectileDamage = damage;
        this.target = target;
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("����ü �׽�Ʈ1");
        if (other.gameObject == target.gameObject)
        {
            
            if (target.GetComponent<HeroStatus_>() != null)
                target.GetComponent<HeroStatus_>().TakeDamage(projectileDamage);
            else
                target.GetComponent<MonsterStatus>().TakeDamage(projectileDamage);
            Destroy(gameObject);
        }

    }
}
