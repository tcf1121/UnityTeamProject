using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NormalZombie : Zombie
{
    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;

        Debug.Log($"���� ü���� {currentHealth}�Դϴ�.");

    }


    protected override void Update()
    {
        base.Move(); // ������ �������̳� ���� ���� �ִٸ� �����ϴ� �κб��� ������
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (CurrentHealth < 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        base.Die();
    }

}
