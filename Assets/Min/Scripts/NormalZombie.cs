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

        Debug.Log($"현재 체력은 {currentHealth}입니다.");

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
