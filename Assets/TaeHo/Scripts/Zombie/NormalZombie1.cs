using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalZombie1 : Zombie
{
   
    private void Start()
    {
        currentHealth = MaxHealth;
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
