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
        base.Move(); // 몬스터의 움직임이나 범위 내에 있다면 공격하는 부분까지 구현함
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
