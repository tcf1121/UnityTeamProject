using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullets : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody rigid;
    [SerializeField] private float returnTime;
    private float timer;


    [Header("Property")]
    [SerializeField] private float attackPower;
    [SerializeField] protected float speed;
    public float AttackPower
    { get { return attackPower; } set { attackPower = value; } }

    private void OnEnable()
    {
        timer = returnTime;
    }


    protected void OnCollisionEnter(Collision collision)
    {
        IDamgable damgable = collision.gameObject.GetComponent<IDamgable>();
        if (damgable != null)
        {
            Attack(damgable);
        }
        Destroy(gameObject);
    }

    public void Shot()
    {
        AttackMethod();
        timer -= Time.time;
        if (timer < 0)
        {
            Destroy(gameObject);
        }

    }
    protected abstract void AttackMethod();

    public void Attack(IDamgable damgable)
    {
        damgable.TakeDamage(gameObject, attackPower);
    }

}
