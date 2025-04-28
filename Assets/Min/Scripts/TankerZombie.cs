using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankerZombie : Zombie
{
    protected override string Name { get; set; } = "TankerZombie";
    protected override int CurrentHealth { get; set; } = 20;
    protected override int MaxHealth { get; set; } = 20;
    protected override int Power { get; set; } = 2;
    protected override float AttackSpeed { get; set; } = 0.5f;
    protected override float MoveSpeed { get; set; } = 0.5f;
    protected override int Level { get; set; } = 1;
    protected override int DropGold { get; set; } = 1;
    protected override int DropExp { get; set; } = 1;
    protected override float AttackRange { get; set; } = 1f;

}
