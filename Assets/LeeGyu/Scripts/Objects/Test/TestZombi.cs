using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestZombie : Zombie
{
    protected override string Name { get; set; } = "TestZombie";
    
    // 프로퍼티 대신 Start에서 설정할 수도
    protected override int CurrentHealth { get; set; } = 1000;
    protected override int MaxHealth { get; set; } = 1000;
    protected override int Power { get; set; } = 1;
    protected override float AttackSpeed { get; set; } = 1f;
    protected override float MoveSpeed { get; set; } = 1f;
    protected override int Level { get; set; } = 1;
    protected override int DropGold { get; set; } = 1;
    protected override int DropExp { get; set; } = 1;
    protected override float AttackRange { get; set; } = 1f;

}
