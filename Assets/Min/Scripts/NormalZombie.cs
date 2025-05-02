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
        base.Move(); // ������ �������̳� ���� ���� �ִٸ� �����ϴ� �κб��� ������
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"{gameObject.name}�� {damage} ���ظ� ����. ���� ü��: {currentHealth}");

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
