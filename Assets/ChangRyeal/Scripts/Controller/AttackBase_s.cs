using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public abstract class AttackBase_s : MonoBehaviour // �������� ����
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
        //Debug.Log($"�׽�Ʈ {target.name}");
        GetCritical();
        if (target != null)
        {
            if (critical) animator.CriticalAttack();
            else animator.Attack();
            Attack();
            ManaRecovery();
        }
    }

    // ���� �� ���ݾ� ������ ȸ����
    protected void ManaRecovery()
    {
        if (GetComponent<HeroStatus_>() != null)
        {
            GetComponent<HeroStatus_>().addMana();
            //if (GetComponent<HeroStatus_>().FullMana())
            //{
            //    skills.Skill(�̸�, ��Ÿ�, ���ݷ�, ���, traget);
            //}
        }
            
    }

    protected abstract void Attack(); // ���Ÿ�, �ٰŸ� ���� ���� ��� �ٸ��� ����
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
