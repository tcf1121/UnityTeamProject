using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // 투사체
    public float projectileSpeed = 1f; // 투사체 속도

    private int projectileDamage = 0;  // 투사체 데미지

    private GameObject shooter;  // 쏘는 캐릭터 붙이면 됨


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
