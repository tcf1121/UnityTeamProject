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

    protected abstract string Name { get; set; }

    protected abstract int CurrentHealth { get; set; }
    public int currentHealth => CurrentHealth;
    protected abstract int MaxHealth { get; set; }

    protected abstract int Power { get; set; }

    protected abstract float AttackSpeed { get; set; }

    protected abstract float MoveSpeed { get; set; }

    protected abstract int Level { get; set; }

    protected abstract int DropGold { get; set; }

    // 좀비가 죽은 후에 이벤트
    public static event Action<int> OnZombieDied; // 좀비가 죽을 경우에 이벤트 발생
    public int goldReward = 1;
    [SerializeField] GameObject goldDropEffectPrefab;


    private void Update()
    {
        if (!isAttacking)
            Move();
    }


    public virtual void SpawnPoint()
    {
        // 몬스터가 스폰하는 함수 구현
    }

    public virtual void Move()
    {
        // 추후 수정할 로직
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"현재 체력: {CurrentHealth}");
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
