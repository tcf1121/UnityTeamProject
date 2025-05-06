using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_s : MonoBehaviour
{
    // ����ü
    public float projectileSpeed = 2f; // ����ü �ӵ�

    private int projectileDamage = 0;  // ����ü ������

    private GameObject target;  // ��� ĳ���� ���̸� ��


    public void Initialize(int damage, GameObject target)
    {
        this.projectileDamage = damage;
        this.target = target;
    }


    private void Update()
    {
        if(target == null || target.activeSelf == false)
            Destroy(gameObject);
        else
        {
            Vector3 targetVec = new Vector3(target.transform.position.x, transform.position.y,
   target.transform.position.z);
            transform.LookAt(targetVec);
            transform.position = Vector3.MoveTowards(transform.position, targetVec
                , projectileSpeed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("����ü �׽�Ʈ1");
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
