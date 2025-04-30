using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public abstract class ParentsAM : MonoBehaviour
{

    [Header("Components")]
    [SerializeField] private float returnTime;
    
    [Header("Property")]
    [SerializeField] private float attackPower;
    [SerializeField] private string tagName;

    private Zombie target;

    public float AttackPower
    { get { return attackPower; } set { attackPower = value; } }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            IDamgable damgable = collision.gameObject.GetComponent<IDamgable>();
            if (damgable != null)
            {
                Attack(damgable);
            }
            
            Debug.Log("¸Â¾Ò´Ù!");
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        MoveMethod(target);
    }
    public void Shot(Zombie target)
    {
        this.target = target;
    }
    
    public abstract void MoveMethod(Zombie target);

    public void Attack(IDamgable damgable)
    {
        damgable.TakeDamage(gameObject, attackPower);
    }

}
