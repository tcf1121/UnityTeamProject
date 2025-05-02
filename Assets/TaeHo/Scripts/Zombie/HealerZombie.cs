using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerZombie : Zombie
{
    public float healRange = 3f;         // ġ�� ����
    public int healAmount = 20;          // �ѹ��� ȸ���� ü��
    public float healCooldown = 5f;      // ġ�� ��Ÿ��

    private float lastHealTime;


    private void Awake()
    {
        maxHealth = 5;
        currentHealth = maxHealth;

        power = 1;
        attackSpeed = 0.5f;
        moveSpeed = 1f;
        level = 1;
        dropGold = 1;
    }


    protected override void Update()
    {
        base.Move();

        if (Time.time >= lastHealTime + healCooldown)
        {
            HealNearbyZombies();
            lastHealTime = Time.time;
        }
    }

    private void HealNearbyZombies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, healRange); // ���� ���� ���� ��ġ Ž��

        Zombie healTarget = null;
        float lowestHealthRatio = 1f;  // ü�� ������ ���� ���� ���� (������ ���)

        foreach (var hit in hits)
        {
            Zombie otherZombie = hit.GetComponent<Zombie>();

            if (otherZombie != null  && otherZombie.IsAlive())
            {
                float healthRatio = otherZombie.CurrentHealth / (float)otherZombie.MaxHealth; // ���� ���� ü�� ���

                if (healthRatio < lowestHealthRatio)
                {
                    lowestHealthRatio = healthRatio;
                    healTarget = otherZombie;
                }
            }
        }

        if (healTarget != null)
        {
            healTarget.Heal(healAmount);
        }
    }

}
