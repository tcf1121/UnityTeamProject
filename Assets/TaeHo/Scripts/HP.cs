using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour, ITarget
{
    // ���� �Ʊ� ���� HP

    public int maxHealth;
    private int currentHealth;

    public event Action OnDied; // �׾��� �� �ܺο��� ���� ����

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name}��(��) {damage} ���ظ� ����. ���� ü��: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        OnDied?.Invoke(); // ������ ���� �̺�Ʈ ����
        Destroy(gameObject);
    }


}
