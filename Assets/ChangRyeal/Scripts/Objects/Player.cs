using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class Player : MonoBehaviour
{
    //private enum ExpRequired
    //{
    //    level1 = 0,
    //    level2 = 2,
    //    level3 = 6,
    //    level4 = 10,
    //    level5 = 20,
    //    level6 = 36,
    //    level7 = 56,
    //    level8 = 80,
    //    level9 =100
    //}
    private int[] ExpRequired = new int[] { 0, 0, 2, 6, 10, 20, 36, 56, 80, 100 };
    [SerializeField] private int health;
    [SerializeField] private int gold;
    [SerializeField] private int exp;
    [SerializeField] private int level;
    [SerializeField] private int stage;

    public int Health { get { return health; } }
    public int Gold { get { return gold; } }
    public int Exp { get { return exp; } }
    public int Level { get { return level; } }
    public int Stage { get { return stage; } }

    void Start()
    {
        health = 100;
        gold = 0;
        exp = 0;
        level = 2;
        stage = 1;
    }

    // ���� �� �� �� �� �Լ�
    private void LevelUp()
    {
        exp -= ExpRequired[level];
        level++;
    }

    // ����ġ�� ��� ������ ���� �� ���� �Լ�
    public void BuyExp()
    {
        exp += 4;
        if (level < 10)
            if (exp > ExpRequired[level])
                LevelUp();
    }

    // ���� ���������� �Ѿ�� �� ���� �Լ�
    public void NextStage()
    {
        exp += 2;
        if (level < 10)
            if (exp > ExpRequired[level])
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
        gold -= cost;
    }

}
