using UnityEngine;

public class NormalZombie : Zombie, ITarget
{
    private void Start()
    {
        maxHealth = 100;
        currentHealth = maxHealth;
    }

    protected override void Update()
    {
        base.Move(); // 몬스터의 움직임이나 범위 내에 있다면 공격하는 부분까지 구현함
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name}이 {damage} 피해를 입음. 남은 체력: {currentHealth}");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
