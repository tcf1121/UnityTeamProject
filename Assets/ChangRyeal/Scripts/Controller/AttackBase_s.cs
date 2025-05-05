using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase_s : MonoBehaviour // 공통적인 공격
{
    public int damage;
    protected Transform target;
    public virtual void TryAttack()
    {
        target = GetComponent<TraceS>().Target;
        //Debug.Log($"테스트 {target.name}");
        if (target != null)
        {
            Attack();
            ManaRecovery();
        }
    }

    // 공격 시 조금씩 마나를 회복함
    protected void ManaRecovery()
    {
        if (GetComponent<HeroStatus_>() != null)
            GetComponent<HeroStatus_>().addMana();
    }

    protected abstract void Attack(); // 원거리, 근거리 마다 공격 모션 다르게 구성
    public void SetDamage()
    {
        if (GetComponent<HeroStatus_>() != null)
            damage = GetComponent<HeroStatus_>().b_Status.attack[0];
        else
            damage = GetComponent<MonsterStatus>().battleStatus.attack;
    }


    public void TakeDamage()
    {
        if (target.GetComponent<HeroStatus_>() != null)
            target.GetComponent<HeroStatus_>().TakeDamage(damage);
        else
            target.GetComponent<MonsterStatus>().TakeDamage(damage);
    }
}
