using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastZombie : Zombie
{
    protected override string Name { get; set; } = "FastZombie";
    protected override int CurrentHealth { get; set; } = 5;
    protected override int MaxHealth { get; set; } = 5;
    protected override int Power { get; set; } = 2;
    protected override float AttackSpeed { get; set; } = 2f;
    protected override float MoveSpeed { get; set; } = 2f;
    protected override int Level { get; set; } = 1;
    protected override int DropGold { get; set; } = 1;
}

