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
            Debug.Log($"{collision.gameObject.name} 에서 총알이 데미지 받을 수 있는 컴포넌트를 가져옴.");
            Attack(damagable);

        }
        else
        {
            Debug.Log($"{collision.gameObject.name} 에서 해당 게임 오브젝트에는 데미지 받을 수 있는 컴포넌트가 없음");
        }

    }

    private void Attack(IInteractionYS damagable)
    {
        damagable.IInteractionYS(gameObject, attackPoint);
    }

}
