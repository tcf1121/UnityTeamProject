using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ShopHeroController : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] List<Hero> shopHero;
    private List<Hero>[] CostHero = new List<Hero>[5];

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
            CostHero[i] = new List<Hero>();
        InputHero();
        GameManager.Instance.player.OnLevelChanged += SetLevel;
    }

    private void OnEnable()
    {
        level = GameManager.Instance.player.Level;
    }

    private void SetLevel()
    {
        level = GameManager.Instance.player.Level;
    }

    // 맨 처음 시작 할 때 기물을 만듦
    private void InputHero()
    {
        foreach(Hero hero in shopHero)
        {
            for (int i = 0; i < 9; i++)
                CostHero[hero.cost - 1].Add(hero);

        }
    }

    // 상점에서 구매하지 않은 영웅 기물은 다시 리스트에 돌려놓음
    public void RevertHero(Hero[] hero)
    {
        for(int i = 0; i < 5; i++)
        {
            if (hero[i] != null)
                CostHero[hero[i].cost - 1].Add(hero[i]);

        }
    }

    // 게임을 맨 처음 시작할 때 영웅 기물 2개 지급
    public Hero[] StartDrawHero()
    {
        Hero[] hero = new Hero[5];
        for(int i = 0; i< 2; i++)
        {
            RandomHero(ref hero[i], 0);
            Debug.Log(hero[i].name);
        }

        return hero;
    }

    #region 상점에 기물 추가
    // 레벨에 따라 상점에 나올 기물을 보여줌
    public Hero[] DrawHero()
    {
        Hero[] hero = new Hero[5];
        int randcost;
        for (int i = 0; i < 5; i++)
        {
            do
            {
                randcost = RandomCost();

                if (CostHero[0].Count == 0 && CostHero[1].Count == 0
                    && CostHero[2].Count == 0 && CostHero[3].Count == 0
                    && CostHero[4].Count == 0)
                {
                    hero[i] = null;
                    break;
                }   
                if (CostHero[randcost].Count != 0)
                {
                    RandomHero(ref hero[i], randcost);
                    break;
                }
            }
            while (CostHero[randcost].Count != 0);
        }
        return hero;
    }


    // 코스트 값에 따라 랜덤으로 기물을 가져옴
    private void RandomHero(ref Hero hero, int cost)
    {
        Random randHero = new Random();
        int randNum = randHero.Next(CostHero[cost].Count);
        hero = CostHero[cost][randNum];
        CostHero[cost].Remove(hero);
    }

    // 레벨에 따른 확률에 따라 코스트를 랜덤으로 가져옴
    private int RandomCost()
    {
        Random randCost = new Random();
        int prob = randCost.Next(100);
        int cost = 0;
        switch (level)
        {
            case 1:
                cost = 1;
                break;
            case 2:
                cost = 1;
                break;
            case 3:
                if (prob < 75)
                    cost = 1;
                else
                    cost = 2;
                break;
            case 4:
                if (prob < 55)
                    cost = 1;
                else if (prob < 85)
                    cost = 2;
                else
                    cost = 3;
                break;
            case 5:
                if (prob < 45)
                    cost = 1;
                else if (prob < 78)
                    cost = 2;
                else if (prob < 98)
                    cost = 3;
                else
                    cost = 4;
                break;
            case 6:
                if (prob < 25)
                    cost = 1;
                else if (prob < 65)
                    cost = 2;
                else if (prob < 95)
                    cost = 3;
                else
                    cost = 4;
                break;
            case 7:
                if (prob < 19)
                    cost = 1;
                else if (prob < 49)
                    cost = 2;
                else if (prob < 84)
                    cost = 3;
                else if (prob < 99)
                    cost = 4;
                else
                    cost = 5;
                break;
            case 8:
                if (prob < 16)
                    cost = 1;
                else if (prob < 36)
                    cost = 2;
                else if (prob < 71)
                    cost = 3;
                else if (prob < 96)
                    cost = 4;
                else
                    cost = 5;
                break;
            case 9:
                if (prob < 9)
                    cost = 1;
                else if (prob < 24)
                    cost = 2;
                else if (prob < 54)
                    cost = 3;
                else if (prob < 84)
                    cost = 4;
                else
                    cost = 5;
                break;
            case 10:
                if (prob < 5)
                    cost = 1;
                else if (prob < 20)
                    cost = 2;
                else if (prob < 50)
                    cost = 3;
                else if (prob < 80)
                    cost = 4;
                else
                    cost = 5;
                break;
        }
        return cost-1;
    }
    #endregion
}
