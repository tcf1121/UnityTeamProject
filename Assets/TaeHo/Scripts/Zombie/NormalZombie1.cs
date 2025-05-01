using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NormalZombie1 : Zombie
{
    [SerializeField] protected override string Name { get; set; } = "NomralZombie1";  // ���� �̸�
    protected override int CurrentHealth { get; set; } = 100;  // ���� ü��
    protected override int MaxHealth { get; set; } // �ƽ� ü��
    protected override int Power { get; set; } = 10; // ���ݷ�
    protected override float AttackSpeed { get; set; } = 1f; // ���ݼӵ�
    protected override float MoveSpeed { get; set; } = 1f; // �̵��ӵ�
    protected override int Level { get; set; } = 1; // ����
    protected override int DropGold { get; set; } 
    protected override int DropExp { get; set; }
    protected override float AttackRange { get; set; } // ���ݹ���

    // ���� ���� �Ŀ� �̺�Ʈ
    public static event Action<int> OnZombieDied; // ���� ���� ��쿡 �̺�Ʈ �߻�
    public int goldReward = 1;
    [SerializeField] GameObject goldDropEffectPrefab; 


    private void Start()
    {
        CurrentHealth = MaxHealth;
    }


    private void Update()
    {
        base.Move(); // ������ �������̳� ���� ���� �ִٸ� �����ϴ� �κб��� ������
    }

    public override void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth < 0)
        {
            Die();
        }
    }

    public override void Die()
    {
        OnZombieDied?.Invoke(goldReward);
        // Instantiate(goldDropEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

}
