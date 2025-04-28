using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
public class Shop : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] private int level;

    [Header("UI")]
    [SerializeField] private Button[] HeroBtn;

    private object[] Hero = new object[5];

    private List<object>[] CostHero = new List<object>[5];


    #region 상점에 기물 추가
    // 레벨에 따라 상점에 나올 기물을 보여줌
    private void ShopLevel()
    {
        int randcost;
        for(int i = 0; i < 5; i++)
        {
            do
            {
                randcost = RandomCost();
                if (CostHero[randcost].Count != 0)
                    RandomHero(ref Hero[i], randcost);
            }
            while (CostHero[randcost].Count == 0);
        }
    }

    // 코스트 값에 따라 랜덤으로 기물을 가져옴
    private void RandomHero(ref object hero, int cost)
    {
        Random randHero = new Random();
        int randNum = randHero.Next(CostHero[cost - 1].Count);
        hero = CostHero[cost - 1][randNum];
        CostHero[cost - 1].Remove(hero);
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

    #region 상점 기물 구매
    public void BuyHero(int index)
    {
        Debug.Log(index);
        // 
        //if(GameManager.Instance.player.CanBuy(Hero[index].cost))
    }
    #endregion

    #region 특수 행동
    // 리롤 : 2 골드 필요
    public void Reroll()
    {
        if(GameManager.Instance.player.Gold >= 2)
        {
            GameManager.Instance.player.Gold -= 2;
            Debug.Log("리롤");
            ShopLevel();
        }
        else
            Debug.Log("돈 부족");
    }

    // 경험치 구매 : 4골드로 4의 경험치 획득
    public void BuyExp()
    {
        if (GameManager.Instance.player.Gold >= 4)
        {
            GameManager.Instance.player.Gold -= 4;
            GameManager.Instance.player.BuyExp();
        }
        else
            Debug.Log("돈 부족");
    }

    #endregion
}
