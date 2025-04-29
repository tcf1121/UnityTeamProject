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
    [SerializeField] GameObject Player;
    [SerializeField] private int level;
    [SerializeField] private bool lockHero = false;
    [SerializeField] private ShopHeroController sHctrl;
    [Header("UI")]
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private Image[] heroImage;
    [SerializeField] private TMP_Text[] costTxt;
    [SerializeField] private TMP_Text[] nameTxt;
    [SerializeField] private Button[] heroBtn;
    [SerializeField] private GameObject[] sellPanel;

    private Hero[] hero = new Hero[5];
    
    

    private void OnEnable()
    {
        shopPanel.SetActive(true);
        if (!lockHero)
        {
            DrawShopHero();
        }
        //heroBtn[0] = GameObject.Find("HeroBtn1").GetComponent<Button>();
        //heroBtn[1] = GameObject.Find("HeroBtn2").GetComponent<Button>();
        //heroBtn[2] = GameObject.Find("HeroBtn3").GetComponent<Button>();
        //heroBtn[3] = GameObject.Find("HeroBtn4").GetComponent<Button>();
        //heroBtn[4] = GameObject.Find("HeroBtn5").GetComponent<Button>();
    }
    private void OnDisable()
    {
        shopPanel.SetActive(false);
        if (!lockHero)
        {
            sHctrl.RevertHero(hero);
        }
    }
    public void DrawShopHero()
    {
        hero = sHctrl.DrawHero();
        SetHeroBtn();
    }

    public void SetHeroBtn()
    {
        for(int i = 0; i < 5; i++)
        {
            sellPanel[i].SetActive(false);
            heroBtn[i].interactable = true;
            if (hero[i] != null)
            {
                heroImage[i].sprite = hero[i].sprite;
                nameTxt[i].text = hero[i].name;
                costTxt[i].text = hero[i].cost.ToString();
            }

        }
    }


    #region 상점 기물 구매
    public void BuyHero(int index)
    {
        if (GameManager.Instance.player.CanBuy(hero[index].cost))
        {
            GameManager.Instance.player.BuyHero(hero[index].cost);
            sellPanel[index].SetActive(true);
            heroBtn[index].interactable = false;
            hero[index] = null;
        }
        else
        {
            Debug.Log("돈 부족");
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
            if (GameManager.Instance.player.Gold >= 2)
            {
                GameManager.Instance.player.Gold -= 2;
                Debug.Log("리롤");
                sHctrl.RevertHero(hero);
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
        if (GameManager.Instance.player.Gold >= 4)
        {
            GameManager.Instance.player.Gold -= 4;
            GameManager.Instance.player.BuyExp();
        }
        else
            Debug.Log("돈 부족");
    }

    #endregion

    public void Ready()
    {
        if (!lockHero)
        {
            sHctrl.RevertHero(hero);
        }
        shopPanel.SetActive(false);
    }

    public void LockShop()
    {
        lockHero = !lockHero;
    }
}
