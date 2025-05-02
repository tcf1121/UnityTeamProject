using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSTestHero : MonoBehaviour
{
    public int star = 1; // ����: 1, 2, 3
    private HeroDataLoader dataLoader;

    void Awake()
    {
        dataLoader = GetComponent<HeroDataLoader>();
    }

    // ���� ���� �ε���
    private int StarIndex => Mathf.Clamp(star - 1, 0, 2);

    // ü��
    public int Hp
    {
        get => dataLoader.status.hp[StarIndex];
        set => dataLoader.status.hp[StarIndex] = value;
    }

    // �ִ� ü��
    public int MaxHp => dataLoader.status.maxHp[StarIndex];

    // ����
    public int Mana
    {
        get => dataLoader.status.mana;
        set => dataLoader.status.mana = Mathf.Clamp(value, 0, 100); // ����: 0~100 ���� ����
    }

    // ���ݷ�
    public int Attack => dataLoader.status.attack[StarIndex];

    // DPS
    public float Dps => dataLoader.status.dps[StarIndex];

    // ���� �߰� ��ġ
    public int AddMana => dataLoader.status.addMana;

    // ü�� ���� ó��
    public void TakeDamage(int amount)
    {
        Hp -= amount;

        Debug.Log($"{dataLoader.status.heroId} ���� {amount} �� ���� HP: {Hp}");

        if (Hp <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"{dataLoader.status.heroId} ���");
        Destroy(gameObject);
    }
}
