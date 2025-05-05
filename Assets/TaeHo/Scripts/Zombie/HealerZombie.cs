using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerZombie : Zombie
{
    public float healRange = 3f;         // �� ����
    public int healAmount = 20;          // ȸ���� ü��
    public float healCooldown = 5f;      // �� ��Ÿ��

    private float lastHealTime;   // ���� ���������� �õ��ߴ� �ð�


    protected override void Awake()
    {
        maxHealth = 5;
        currentHealth = maxHealth;

        power = 1;
        attackSpeed = 0.5f;
        moveSpeed = 0;
        level = 1;
        dropGold = 1;
    }

    protected override void Update()
    {
        base.Move();

        float cooldownRemaining = (lastHealTime + healCooldown) - Time.time;

        if (cooldownRemaining <= 0f)
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

            if (otherZombie != null && otherZombie.IsAlive())
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


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }

}
