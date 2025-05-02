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

    protected abstract string Name { get; set; }

    protected abstract int CurrentHealth { get; set; }
    public int currentHealth => CurrentHealth;
    protected abstract int MaxHealth { get; set; }

    protected abstract int Power { get; set; }

    protected abstract float AttackSpeed { get; set; }

    protected abstract float MoveSpeed { get; set; }

    protected abstract int Level { get; set; }

    protected abstract int DropGold { get; set; }

    // ���� ���� �Ŀ� �̺�Ʈ
    public static event Action<int> OnZombieDied; // ���� ���� ��쿡 �̺�Ʈ �߻�
    public int goldReward = 1;
    [SerializeField] GameObject goldDropEffectPrefab;


    private void Update()
    {
        if (!isAttacking)
            Move();
    }


    public virtual void SpawnPoint()
    {
        // ���Ͱ� �����ϴ� �Լ� ����
    }

    public virtual void Move()
    {
        // ���� ������ ����
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"���� ü��: {CurrentHealth}");
        if (CurrentHealth <= 0)
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

    
}
