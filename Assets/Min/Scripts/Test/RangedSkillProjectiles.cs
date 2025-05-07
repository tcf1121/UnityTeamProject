using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedSkillProjectiles : MonoBehaviour
{
    public float rangedProjectileSpeed = 2f; // ����ü �ӵ�

    private int projectileDamage = 0;  // ����ü ������

    private Vector3 targetVec;  // ��� ĳ���� ���̸� ��

    private int range;

    public void Initialize(int damage, Vector3 target, int range)
    {
        Vector3 targetVec = new Vector3(target.x, 1, target.z);
        projectileDamage = damage;
        this.range = range;
    }

    private void Update()
    {
        //if (transform.position == targetVec)
        //    Destroy(gameObject);
        //transform.LookAt(targetVec);
        transform.Translate(Vector3.forward * rangedProjectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
            other.GetComponent<MonsterStatus>().TakeDamage(projectileDamage);
    }
    //Destroy(gameObject, (float)((range * 0.8666) / projectileSpeed)); // ���� ���� ��
}
