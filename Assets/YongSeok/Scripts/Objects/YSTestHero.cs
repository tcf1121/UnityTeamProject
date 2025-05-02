using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSTestHero : MonoBehaviour
{
    public int star = 1; // 성급: 1, 2, 3
    private HeroDataLoader dataLoader;

    void Awake()
    {
        dataLoader = GetComponent<HeroDataLoader>();
    }

    // 현재 성급 인덱스
    private int StarIndex => Mathf.Clamp(star - 1, 0, 2);

    // 체력
    public int Hp
    {
        get => dataLoader.status.hp[StarIndex];
        set => dataLoader.status.hp[StarIndex] = value;
    }

    // 최대 체력
    public int MaxHp => dataLoader.status.maxHp[StarIndex];

    // 마나
    public int Mana
    {
        get => dataLoader.status.mana;
        set => dataLoader.status.mana = Mathf.Clamp(value, 0, 100); // 예시: 0~100 사이 제한
    }

    // 공격력
    public int Attack => dataLoader.status.attack[StarIndex];

    // DPS
    public float Dps => dataLoader.status.dps[StarIndex];

    // 마나 추가 수치
    public int AddMana => dataLoader.status.addMana;

    // 체력 감소 처리
    public void TakeDamage(int amount)
    {
        Hp -= amount;

        Debug.Log($"{dataLoader.status.heroId} 피해 {amount} → 현재 HP: {Hp}");

        if (Hp <= 0)
            Die();
    }

    private void Die()
    {
        Debug.Log($"{dataLoader.status.heroId} 사망");
        Destroy(gameObject);
    }
}
