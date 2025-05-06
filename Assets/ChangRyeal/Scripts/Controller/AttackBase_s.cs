using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class AttackBase_s : MonoBehaviour // 공통적인 공격
{
    public int damage;
    protected Transform target;
    public Skills skills;
    [SerializeField] public ObjectAnimator animator;
    private bool critical;
    private Random rand;

    public virtual void TryAttack()
    {
        target = GetComponent<TraceS>().Target;
        //Debug.Log($"테스트 {target.name}");
        GetCritical();
        if (target != null)
        {
            if (critical) animator.CriticalAttack();
            else animator.Attack();
            Attack();
            ManaRecovery();
        }
    }

    // 공격 시 조금씩 마나를 회복함
    protected void ManaRecovery()
    {
        if (GetComponent<HeroStatus_>() != null)
        {
            GetComponent<HeroStatus_>().addMana();
            //if (GetComponent<HeroStatus_>().FullMana())
            //{
            //    skills.Skill(이름, 사거리, 공격력, 계수, traget);
            //}
        }
            
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
        if (critical) damage = (int)(damage *GetComponent<HeroStatus_>().b_Status.criticalDamage);
        if (target.GetComponent<HeroStatus_>() != null)
            target.GetComponent<HeroStatus_>().TakeDamage(damage);
        else
            target.GetComponent<MonsterStatus>().TakeDamage(damage);
    }

    private bool GetCritical()
    {
        if (GetComponent<HeroStatus_>() != null)
        {
            rand = new();
            if (rand.Next(0, 100) < GetComponent<HeroStatus_>().b_Status.critical)
            {
                return true;
            }
        }
        return false;
    }
}
