using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // ����ü
    public float projectileSpeed = 1f; // ����ü �ӵ�

    private int projectileDamage = 0;  // ����ü ������

    private GameObject shooter;  // ��� ĳ���� ���̸� ��


    public void Initialize(int damage, GameObject shooter)
    {
        this.projectileDamage = damage;
        this.shooter = shooter;
    }


    private void Update()
    {
        transform.Translate(Vector3.forward * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
       ITarget target = other.GetComponent<ITarget>();
        
        if (target != null)
        {
            target.TakeDamage(projectileDamage);
            Destroy(gameObject);
        }

    }
}
