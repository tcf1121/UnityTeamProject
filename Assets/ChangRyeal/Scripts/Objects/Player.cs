using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;




public class Player : MonoBehaviour
{
    [SerializeField] GameObject info;
    [SerializeField] public GameObject shop;

    private readonly int[] ExpRequired = new int[] { 0, 0, 2, 6, 10, 20, 36, 56, 80, 100 };
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int gold;
    [SerializeField] private int exp;
    [SerializeField] private int maxExp;
    [SerializeField] private int level;
    [SerializeField] private int stage;
    [SerializeField] private bool battling;
    [SerializeField] public PlayerHero playerHero;
    [SerializeField] public TileMapManager tileMap;
    [SerializeField] private Button readyBtn;

    // ������
    // ����

    // ��Ʋ ���� �� Ȯ�� ����

    public bool Battling { get { return battling; } set { battling = value; OnBattlingChanged?.Invoke(); } }
    public event Action OnBattlingChanged;
    public int Health { get { return health; } set { health = value; OnHelthChanged?.Invoke(); } }
    public event Action OnHelthChanged;
    public int Gold { get { return gold; } set { gold = value; OnGoldChanged?.Invoke(); } }
    public event Action OnGoldChanged;
    public int Exp { get { return exp; } set { exp = value; OnExpChanged?.Invoke(); } }
    public event Action OnExpChanged;
    public int Level { get { return level; } set { level = value; OnLevelChanged?.Invoke(); } }
    public event Action OnLevelChanged;
    public int Stage { get { return stage; } set { stage = value; OnStageChanged?.Invoke(); } }
    public event Action OnStageChanged;
    public int MaxHealth { get { return maxHealth; } }
    public int MaxExp { get { return maxExp; } set { maxExp = value; OnLevelChanged?.Invoke(); } }

    private void OnEnable()
    {
        //�׽�Ʈ �����θ� �߰�
        //StartGame();
    }

    public void GameStart()
    {
        setPlayer();
        shop = GameObject.Find("ShopManager");
        //Debug.Log(shop.name);
        info = GameObject.Find("PlayerInfo");
        tileMap = GameObject.Find("TileMapManager").GetComponent<TileMapManager>();
        readyBtn = GameObject.Find("ReadyBtn").GetComponent<Button>();
        readyBtn.onClick.AddListener(playerHero.SetBattle);
        info.SetActive(false);
        shop.SetActive(false);

        playerHero.battleManager = GameObject.Find("BattleManager");
        playerHero.SetPlayerHero();
        info.SetActive(true);
        shop.SetActive(true);
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
        if(Level == 10)
        {
            Exp = 0;
            MaxExp = 0;
        }
        else
            MaxExp = ExpRequired[level];
    }

    // ����ġ�� ��� ������ ���� �� ���� �Լ�
    public void BuyExp()
    {
        if (level < 10)
        {
            Exp += 4;
            if (exp >= maxExp)
                LevelUp();
        }
    }
    public void Expplus()
    {
        if (level < 10)
        {
            Exp += 2;
            if (exp >= maxExp)
                LevelUp();
        }
    }
    // ���� ���������� �Ѿ�� �� ���� �Լ�
    public void NextStage()
    {
        if (level < 10)
        {
            Exp += 2;
            if (exp >= maxExp)
                LevelUp();
        }
    }

    // �� �� �ִ��� ���θ� ��ȯ �����ִ� �Լ�
    public bool CanBuy(int cost)
    {
        return gold >= cost ? true : false;
    }

    // �Ʊ� �⹰�� ���� �� �Լ�
    public void BuyHero(Hero hero,int cost)
    {
        Gold -= cost;
        playerHero.NewHero(hero);
        // �����Կ� �Ʊ� �⹰ �߰�
    }

    // �Ʊ� �⹰�� �Ǹ� ���� �� �Լ�
    public void SellHero(GameObject heroObj)
    {
        Hero hero = heroObj.GetComponent<Hero>();
        int heroNum = (int)Mathf.Pow(3f, (float)(hero.star - 1));
        if (hero.cost == 1)
            Gold += hero.cost * heroNum;
        else
            Gold += hero.cost * heroNum - 1;
        playerHero.SellHero(heroObj);
    }
}
