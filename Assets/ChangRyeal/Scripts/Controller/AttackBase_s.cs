using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase_s : MonoBehaviour // �������� ����
{
    public int damage;
    protected Transform target;
    public virtual void TryAttack()
    {
        target = GetComponent<TraceS>().Target;
        //Debug.Log($"�׽�Ʈ {target.name}");
        if (target != null)
        {
            Attack();
            ManaRecovery();
        }
    }

    // ���� �� ���ݾ� ������ ȸ����
    protected void ManaRecovery()
    {
        if (GetComponent<HeroStatus_>() != null)
            GetComponent<HeroStatus_>().addMana();
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
        if (target.GetComponent<HeroStatus_>() != null)
            target.GetComponent<HeroStatus_>().TakeDamage(damage);
        else
            target.GetComponent<MonsterStatus>().TakeDamage(damage);
    }
}
