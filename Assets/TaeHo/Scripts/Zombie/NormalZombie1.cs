using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalZombie1 : Zombie
{
    [SerializeField] protected override string Name { get; set; } = "NomralZombie1";  // 적군 이름
    protected override int CurrentHealth { get; set; } = 100;  // 현재 체력
    protected override int MaxHealth { get; set; } // 맥스 체력
    protected override int Power { get; set; } = 10; // 공격력
    protected override float AttackSpeed { get; set; } = 1f; // 공격속도
    protected override float MoveSpeed { get; set; } = 1f; // 이동속도
    protected override int Level { get; set; } = 1; // 레벨
    protected override int DropGold { get; set; } 
    protected override int DropExp { get; set; }
    protected override float AttackRange { get; set; } // 공격범위

    // 좀비가 죽은 후에 이벤트
    public static event Action<int> OnZombieDied; // 좀비가 죽을 경우에 이벤트 발생
    public int goldReward = 1;
    [SerializeField] GameObject goldDropEffectPrefab; 


    private void Start()
    {
        CurrentHealth = MaxHealth;
    }


    private void Update()
    {
        base.Move(); // 몬스터의 움직임이나 범위 내에 있다면 공격하는 부분까지 구현함
    }

    public override void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth < 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        OnZombieDied?.Invoke(goldReward);
        // Instantiate(goldDropEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
