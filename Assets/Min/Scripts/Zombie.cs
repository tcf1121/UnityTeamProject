using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    private bool isAttacking;

    private Coroutine AttackCoroutine;

    // 바꿔야될 클래스
    //private TestPlant targetPlant;

    [SerializeField] protected int currentHealth;
    public int CurrentHealth => currentHealth;


    [SerializeField] protected int maxHealth;
    public int MaxHealth => maxHealth;

    [SerializeField] protected int power;
    public int Power => power;

    [SerializeField] protected float attackSpeed;
    public float AttackSpeed => attackSpeed;

    [SerializeField] protected float moveSpeed;
    public float MoveSpeed => moveSpeed;


    [SerializeField] protected int level;
    public int Level => level;
    
    [SerializeField] protected int dropGold;
    public int DropGold => dropGold;


    // 좀비가 죽은 후에 이벤트
    public static event Action<int> OnZombieDied; // 좀비가 죽을 경우에 이벤트 발생
    public int goldReward = 1;
    [SerializeField] GameObject goldDropEffectPrefab;

    protected virtual void Awake()
    {
  
    }


    protected virtual void Update()
    {
        if (!isAttacking)
            Move();
    }


    public virtual void Move()
    {
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"현재 체력: {CurrentHealth}");
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        OnZombieDied?.Invoke(goldReward);
        // Instantiate(goldDropEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    
    public virtual void Heal(int healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);  // 힐을 할 경우 MaxHealth 보다 많이 힐 되면 곤란함
    }

    public virtual bool IsAlive()  // 살아 있는 경우 판단
    {
        return currentHealth > 0;  
    }


}
