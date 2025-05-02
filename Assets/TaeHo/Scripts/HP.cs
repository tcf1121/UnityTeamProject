using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour, ITarget
{
    // 적군 아군 공통 HP

    public int maxHealth;
    private int currentHealth;

    public event Action OnDied; // 죽었을 때 외부에서 구독 가능

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name}이(가) {damage} 피해를 받음. 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied?.Invoke(); // 구독된 죽음 이벤트 실행
        Destroy(gameObject);
    }


}
