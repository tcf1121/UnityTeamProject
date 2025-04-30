using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class ParentsAM : MonoBehaviour
{
    [Header("Range and Tageting")]
    [SerializeField] private Transform rangeOrigin;
    [SerializeField] protected float attackRange;
    [SerializeField] protected LayerMask targetLayer;

    protected Zombie target;

    [Header("Components")]
    [SerializeField] private float returnTime;
    [SerializeField] protected float delayTime;

    [Header("Property")]
    [SerializeField] private float attackPower;

    public float AttackPower
    { get { return attackPower; } set { attackPower = value; } }

    protected void OnCollisionEnter(Collision collision)
    {
        IDamgable damgable = collision.gameObject.GetComponent<IDamgable>();
        if (damgable != null)
        {
            Attack(damgable);
        }
        Destroy(gameObject);
    }

    public virtual void Shot()
    {
        AcqireTarget();
    }

    private void AcqireTarget()
    {
        Collider[] hits = Physics.OverlapSphere(rangeOrigin.position, attackRange, targetLayer);

        float minDist =float.MaxValue;
        foreach (var hit in hits)
        {
            Zombie zombie = hit.GetComponent<Zombie>();
            float dist = Vector3.Distance(rangeOrigin.position,zombie.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                target = zombie;
            }
        }
    }

    public virtual void AttackMethod()
    {
        target = gameObject.GetComponent<Zombie>();
    }
    public void Attack(IDamgable damgable)
    {
        damgable.TakeDamage(gameObject, attackPower);
    }

}
