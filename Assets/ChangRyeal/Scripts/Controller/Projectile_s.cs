using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile_s : MonoBehaviour
{
    // 투사체
    public float projectileSpeed = 2f; // 투사체 속도

    private int projectileDamage = 0;  // 투사체 데미지

    private GameObject target;  // 쏘는 캐릭터 붙이면 됨


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
        //Debug.Log("투사체 테스트1");
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
