using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class ShopHeroController : MonoBehaviour
{
    [SerializeField] private int level;
    [SerializeField] List<Hero> shopHero;
    [SerializeField] List<Hero> CostHeroOne;
    [SerializeField] List<Hero> CostHeroTwo;
    [SerializeField] List<Hero> CostHeroThree;
    [SerializeField] List<Hero> CostHeroFour;
    [SerializeField] List<Hero> CostHeroFive;


    private void OnEnable()
    {
        level = GameManager.Instance.player.Level;
    }

    private void SetLevel()
    {
        level = GameManager.Instance.player.Level;
    }
    public void SetShop()
    {
        InputHero();
        GameManager.Instance.player.OnLevelChanged += SetLevel;
    }
    // 맨 처음 시작 할 때 기물을 만듦
    private void InputHero()
    {
        foreach(Hero hero in shopHero)
        {
            for (int i = 0; i < 9; i++)
            {
                if(hero.cost == 1)
                    CostHeroOne.Add(hero);
                if (hero.cost == 2)
                    CostHeroTwo.Add(hero);
                if (hero.cost == 3)
                    CostHeroThree.Add(hero);
                if (hero.cost == 4)
                    CostHeroFour.Add(hero);
                if (hero.cost == 5)
                    CostHeroFive.Add(hero);
            }
        }
        //Debug.Log(CostHero[0].Count);
    }

    // 상점에서 구매하지 않은 영웅 기물은 다시 리스트에 돌려놓음
    public void RevertHero(Hero[] hero)
    {
        for(int i = 0; i < 5; i++)
        {
            if (hero[i] != null)
                AddHeroCost(hero[i]);

        }
        //Debug.Log(CostHero[0].Count);
    }

    // 영웅 판매 
    public void SellHero(Hero hero)
    {
        int heroNum = (int)Mathf.Pow(3f, (float)(hero.star - 1));
        hero.star = 1;
        for (int i = 0; i < heroNum; i++)
        {
            if (hero.cost == 1)
                CostHeroOne.Add(hero);
            if (hero.cost == 2)
                CostHeroTwo.Add(hero);
            if (hero.cost == 3)
                CostHeroThree.Add(hero);
            if (hero.cost == 4)
                CostHeroFour.Add(hero);
            if (hero.cost == 5)
                CostHeroFive.Add(hero);
        }
    }

    private int AllHero()
    {
        int num = CostHeroOne.Count + CostHeroTwo.Count + CostHeroThree.Count +
            CostHeroFour.Count + CostHeroFive.Count;

        return num;
    }

    private int CostHeroNum(int cost)
    {
        if (cost == 1)
            return CostHeroOne.Count;
        if (cost == 2)
            return CostHeroTwo.Count;
        if (cost == 3)
            return CostHeroThree.Count;
        if (cost == 4)
            return CostHeroFour.Count;
        if (cost == 5)
            return CostHeroFive.Count;
        return 0;
    }

    private void AddHeroCost(Hero hero)
    {
        if (hero.cost == 1)
            CostHeroOne.Add(hero);
        if (hero.cost == 2)
            CostHeroTwo.Add(hero);
        if (hero.cost == 3)
            CostHeroThree.Add(hero);
        if (hero.cost == 4)
            CostHeroFour.Add(hero);
        if (hero.cost == 5)
            CostHeroFive.Add(hero);
    }

    // 게임을 맨 처음 시작할 때 영웅 기물 2개 지급
    public Hero[] StartDrawHero()
    {
        Hero[] hero = new Hero[5];
        for(int i = 0; i< 2; i++)
        {
            hero[i] = RandomHero(1);
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
            randcost = RandomCost();
            if (AllHero() == 0)
            {
                hero[i] = null;
            }
            if (CostHeroNum(randcost) != 0)
            {
                hero[i] = RandomHero(randcost);
            }
        }
        return hero;
    }


    // 코스트 값에 따라 랜덤으로 기물을 가져옴
    private Hero RandomHero(int cost)
    {
        Random randHero = new Random();
        int randNum = 0;
        Hero hero = null;
        if (cost == 1)
        {
            randNum = randHero.Next(CostHeroOne.Count);
            hero = CostHeroOne[randNum];
            CostHeroOne.Remove(hero);
        }
        if (cost == 2)
        {
            randNum = randHero.Next(CostHeroTwo.Count);
            hero = CostHeroTwo[randNum];
            CostHeroTwo.Remove(hero);
        }
        if (cost == 3)
        {
            randNum = randHero.Next(CostHeroThree.Count);
            hero = CostHeroThree[randNum];
            CostHeroThree.Remove(hero);
        }
        if (cost == 4)
        {
            randNum = randHero.Next(CostHeroFour.Count);
            hero = CostHeroFour[randNum];
            CostHeroFour.Remove(hero);
        }
        if (cost == 5)
        {
            randNum = randHero.Next(CostHeroFive.Count);
            hero = CostHeroFive[randNum];
            CostHeroFive.Remove(hero);
        }
        return hero;
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
        return cost;
    }
    #endregion
}
