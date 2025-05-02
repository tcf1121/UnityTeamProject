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


    public float healRange = 3f;   // �� ����
    public float healAmount = 1f;  // ȸ�� ü��
    public float healCooldown = 5f; // ��Ÿ��

    private float lastHealTime;

    public void SpecialAbility()
    {
        // ��� ���� ����
    }
}
