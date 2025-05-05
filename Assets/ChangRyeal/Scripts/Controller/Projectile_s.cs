using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_s : MonoBehaviour
{
    // 투사체
    public float projectileSpeed = 1f; // 투사체 속도

    private int projectileDamage = 0;  // 투사체 데미지

    private GameObject target;  // 쏘는 캐릭터 붙이면 됨


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
        Debug.Log("투사체 테스트1");
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
