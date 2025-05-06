using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
public class Shop : MonoBehaviour
{
    [SerializeField] GameObject shopManager;
    [SerializeField] private bool lockHero = false;
    [SerializeField] private ShopHeroController sHctrl;
    [Header("UI")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject readyBtn;
    [SerializeField] private Image[] heroImage;
    [SerializeField] private TMP_Text[] costTxt;
    [SerializeField] private TMP_Text[] nameTxt;
    [SerializeField] private Button[] heroBtn;

    [SerializeField] private Hero[] hero = new Hero[5];
    
    

    // 스테이지가 넘어갈때로 변경
    private void OnEnable()
    {
        shopPanel.SetActive(true);
        readyBtn.SetActive(true);
        Hero[] firstHero = sHctrl.StartDrawHero();
        for (int i = 0; i < 2; i++)
        {
            if (i < 2)
                GameManager.Instance.player.BuyHero(firstHero[i], 0);
        }
        GameManager.Instance.player.OnBattlingChanged += EndBattle;

        DrawShopHero();
    }

    // 필요 없을듯
    private void OnDisable()
    {
        readyBtn.SetActive(false);
        shopPanel.SetActive(false);
        if (!lockHero)
        {
            
        }
    }

    public void EndBattle()
    {
        if (!GameManager.Instance.player.Battling)
        {
            readyBtn.SetActive(true);
            if (!lockHero)
            {
                DrawShopHero();
            }
        }

    }

    public void DrawShopHero()
    {
        sHctrl.RevertHero(hero);
        hero = sHctrl.DrawHero();
        SetHeroBtn();
    }

    public void SetHeroBtn()
    {
        for(int i = 0; i < 5; i++)
        {
            heroBtn[i].interactable = true;
            if (hero[i] != null)
            {
                heroImage[i].sprite = hero[i].sprite;
                nameTxt[i].text = hero[i].heroname;
                costTxt[i].text = hero[i].cost.ToString();
            }

        }
    }


    #region 상점 기물 구매
    public void BuyHero(int index)
    {
        if (hero[index] != null)
        {
            if (!GameManager.Instance.player.playerHero.FullHero())
            {
                if (GameManager.Instance.player.CanBuy(hero[index].cost))
                {
                    GameManager.Instance.player.BuyHero(hero[index], hero[index].cost);
                    heroBtn[index].interactable = false;
                    hero[index] = null;
                }
                else
                {
                    Debug.Log("돈 부족");
                }
            }
            else
            {
                Debug.Log("자리 부족");
            }
        }

        // 
        //if(GameManager.Instance.player.CanBuy(Hero[index].cost))
    }
    #endregion

    #region 특수 행동
    // 리롤 : 2 골드 필요
    public void Reroll()
    {
        if (!lockHero)
        {
            if (GameManager.Instance.player.CanBuy(2))
            {
                GameManager.Instance.player.Gold -= 2;
                Debug.Log("리롤");
                DrawShopHero();
            }
            else
                Debug.Log("돈 부족");
        }
        else
        {
            Debug.Log("잠김");
        }
    }

    // 경험치 구매 : 4골드로 4의 경험치 획득
    public void BuyExp()
    {
        if(GameManager.Instance.player.Level == 10)
        {
            Debug.Log("레벨 최대");
        }
        else
        {
            if (GameManager.Instance.player.CanBuy(4))
            {
                GameManager.Instance.player.Gold -= 4;
                GameManager.Instance.player.BuyExp();
            }
            else
                Debug.Log("돈 부족");
        }

    }

    #endregion

    public void Ready()
    {
        readyBtn.SetActive(false);
        GameManager.Instance.player.Battling = true;
    }

    public void LockShop()
    {
        lockHero = !lockHero;
    }
}
