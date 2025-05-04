using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour // �������� ����
{
    public int damage;  // ���� ������
    public float attackCoolDown; // ���� ��Ÿ�� (�ð� ������� ������ �ȵǱ� ������)
    private float lastAttackTime; // ���������� ������ ���� (��Ÿ�� ���߱� ���ؼ� �ʿ�)


    public virtual void TryAttack()
    {
        if (Time.time > lastAttackTime + attackCoolDown) 
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    // ���� �� ���ݾ� ������ ȸ����
    protected void ManaRecovery(GameObject attacker)
    {
        ManaManager manaManager = attacker.GetComponent<ManaManager>();

        if (manaManager != null) 
        {
            manaManager.AddMana(10);
        }
    }

    protected abstract void Attack(); // ���Ÿ�, �ٰŸ� ���� ���� ��� �ٸ��� ����

}
