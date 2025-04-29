using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;




public class Player : MonoBehaviour
{
    private readonly int[] ExpRequired = new int[] { 0, 0, 2, 6, 10, 20, 36, 56, 80, 100 };
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int gold;
    [SerializeField] private int exp;
    [SerializeField] private int maxExp;
    [SerializeField] private int level;
    [SerializeField] private int stage;

    // 보관함
    // 전장

    public int Health { get { return health; } set { health = value; OnHelthChanged?.Invoke(); } }
    public event Action OnHelthChanged;
    public int Gold { get { return gold; } set { gold = value; OnGoldChanged?.Invoke(); } }
    public event Action OnGoldChanged;
    public int Exp { get { return exp; } set { exp = value; OnExpChanged?.Invoke(); } }
    public event Action OnExpChanged;
    public int Level { get { return level; } set { level = value; OnLevelChanged?.Invoke(); } }
    public event Action OnLevelChanged;
    public int Stage { get { return stage; } }
    public int MaxHealth { get { return maxHealth; } }
    public int MaxExp { get { return maxExp; } set { maxExp = value; OnLevelChanged?.Invoke(); } }

    private void OnEnable()
    {
        setPlayer();
    }

    public void setPlayer()
    {
        maxHealth = 100;
        Health = maxHealth;
        Gold = 0;
        maxExp = 0;
        Exp = 0;
        Level = 2;
        stage = 1;
    }
    // 레벨 업 할 때 쓸 함수
    private void LevelUp()
    {
        Exp -= maxExp;
        Level++;
        MaxExp = ExpRequired[level];
    }

    // 경험치를 사는 행위를 했을 때 사용될 함수
    public void BuyExp()
    {
        Exp += 4;
        if (level < 10)
            if (exp >= maxExp)
                LevelUp();
    }
    public void Expplus()
    {
        Exp++;
        if (level < 10)
            if (exp >= maxExp)
                LevelUp();
    }
    // 다음 스테이지로 넘어갔을 때 사용될 함수
    public void NextStage()
    {
        Exp += 2;
        if (level < 10)
            if (exp >= maxExp)
                LevelUp();
    }

    // 살 수 있는지 여부를 반환 시켜주는 함수
    public bool CanBuy(int cost)
    {
        return gold >= cost ? true : false;
    }

    // 아군 기물을 샀을 때 함수
    public void BuyHero(int cost)
    {
        Gold -= cost;
        // 보관함에 아군 기물 추가
    }

    // 아군 기물을 판매 했을 때 함수
    public void SellHero(/*기물*/)
    {
        /*
         * if(기물 코스트 == 1)
         *      gold += 기물 코스트 * 기물 단계;
         * else
         *      gold += (기물 코스트 * 기물 단계) - 1;
         */
    }
}
