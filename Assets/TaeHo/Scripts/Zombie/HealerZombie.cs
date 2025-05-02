using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerZombie : Zombie
{
    protected override string Name { get; set; } = "HealerZombie";
    protected override int CurrentHealth { get; set; } = 5;
    protected override int MaxHealth { get; set; } = 5;
    protected override int Power { get; set; } = 1;
    protected override float AttackSpeed { get; set; } = 0.5f;
    protected override float MoveSpeed { get; set; } = 0.5f;
    protected override int Level { get; set; } = 1;
    protected override int DropGold { get; set; } = 1;
    protected override int DropExp { get; set; } = 1;
    protected override float AttackRange { get; set; } = 1f;


    public float healRange = 3f;   // 힐 범위
    public float healAmount = 1f;  // 회복 체력
    public float healCooldown = 5f; // 쿨타임

    private float lastHealTime;

    public void SpecialAbility()
    {
        // 방법 추후 선택
    }
}
