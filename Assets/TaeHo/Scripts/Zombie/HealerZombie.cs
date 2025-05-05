using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealerZombie : Zombie
{
    public float healRange = 3f;         // 힐 범위
    public int healAmount = 20;          // 회복할 체력
    public float healCooldown = 5f;      // 힐 쿨타임

    private float lastHealTime;   // 힐을 마지막으로 시도했던 시간


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
        Collider[] hits = Physics.OverlapSphere(transform.position, healRange); // 범위 내에 좀비 위치 탐색

        Zombie healTarget = null;
        float lowestHealthRatio = 1f;  // 체력 비율이 가장 낮은 좀비 (비율로 계산)

        foreach (var hit in hits)
        {
            Zombie otherZombie = hit.GetComponent<Zombie>();

            if (otherZombie != null && otherZombie.IsAlive())
            {
                float healthRatio = otherZombie.CurrentHealth / (float)otherZombie.MaxHealth; // 현재 좀비 체력 계산

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
