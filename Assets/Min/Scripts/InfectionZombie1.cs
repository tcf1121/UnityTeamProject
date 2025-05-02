using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectionZombie1 : Zombie
{
    protected override string Name { get ; set; }
    protected override int CurrentHealth { get; set; }
    protected override int MaxHealth { get; set; }
    protected override int Power { get; set; }
    protected override float AttackSpeed { get; set; }
    protected override float MoveSpeed { get; set; }
    protected override int Level { get; set; }
    protected override int DropGold { get; set; }
    protected override int DropExp { get; set; } // 삭제
    protected override float AttackRange { get; set; }


    protected void InfectionAttack(GameObject target)
    {
        //IDamagable damagable = target.GetComponent<IDamagable>();

        //if (damagable != null)
        //    damagable.TakeDamage(damagable.MaxHealth / 3);

        //if (damagable.CurrentHealth <= 0)
        //{
        //    Vector3 spawnPos = damagable.Position;

        //    Instantiate(좀비 프리팹, spawnPos, Quaternion.identity);
        //}
    }
}
