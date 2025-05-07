using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedSkillProjectiles : MonoBehaviour
{
    public float rangedProjectileSpeed = 2f; // ����ü �ӵ�

    private int projectileDamage = 0;  // ����ü ������

    private GameObject target;  // ��� ĳ���� ���̸� ��

    private int range;

    public void Initialize(int damage, GameObject target, int range)
    {
        projectileDamage = damage;
        this.target = target;
        this.range = range;
    }

    private void Update()
    {
        Vector3 targetVec = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetVec);
        transform.Translate(targetVec * rangedProjectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Monster"))
            target.GetComponent<MonsterStatus>().TakeDamage(projectileDamage);
    }
    //Destroy(gameObject, (float)((range * 0.8666) / projectileSpeed)); // ���� ���� ��
}
