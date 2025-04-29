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

    // ������
    // ����

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
    // ���� �� �� �� �� �Լ�
    private void LevelUp()
    {
        Exp -= maxExp;
        Level++;
        MaxExp = ExpRequired[level];
    }

    // ����ġ�� ��� ������ ���� �� ���� �Լ�
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
    // ���� ���������� �Ѿ�� �� ���� �Լ�
    public void NextStage()
    {
        Exp += 2;
        if (level < 10)
            if (exp >= maxExp)
                LevelUp();
    }

    // �� �� �ִ��� ���θ� ��ȯ �����ִ� �Լ�
    public bool CanBuy(int cost)
    {
        return gold >= cost ? true : false;
    }

    // �Ʊ� �⹰�� ���� �� �Լ�
    public void BuyHero(int cost)
    {
        Gold -= cost;
        // �����Կ� �Ʊ� �⹰ �߰�
    }

    // �Ʊ� �⹰�� �Ǹ� ���� �� �Լ�
    public void SellHero(/*�⹰*/)
    {
        /*
         * if(�⹰ �ڽ�Ʈ == 1)
         *      gold += �⹰ �ڽ�Ʈ * �⹰ �ܰ�;
         * else
         *      gold += (�⹰ �ڽ�Ʈ * �⹰ �ܰ�) - 1;
         */
    }
}
