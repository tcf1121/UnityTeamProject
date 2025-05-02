using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    private bool isAttacking;

    private Coroutine AttackCoroutine;

    // �ٲ�ߵ� Ŭ����
    //private TestPlant targetPlant;

    [SerializeField] protected int currentHealth { get; set; }
    public int CurrentHealth => currentHealth;


    [SerializeField] protected int maxHealth { get; set; }
    public int MaxHealth => maxHealth;

    [SerializeField] protected  int power { get; set; }
    public int Power => power;

    [SerializeField] protected float attackSpeed { get; set; }
    public float AttackSpeed => attackSpeed;    

    [SerializeField] protected float moveSpeed { get; set; }
    public float MoveSpeed => moveSpeed;


    [SerializeField] protected int level { get; set; }
    public int Level => level;
    
    [SerializeField] protected int dropGold { get; set; }
    public int DropGold => dropGold;


    // ���� ���� �Ŀ� �̺�Ʈ
    public static event Action<int> OnZombieDied; // ���� ���� ��쿡 �̺�Ʈ �߻�
    public int goldReward = 1;
    [SerializeField] GameObject goldDropEffectPrefab;


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
        Debug.Log($"���� ü��: {CurrentHealth}");
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
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);  // ���� �� ��� MaxHealth ���� ���� �� �Ǹ� �����
    }

    public virtual bool IsAlive()  // ��� �ִ� ��� �Ǵ�
    {
        return currentHealth > 0;  
    }


}
