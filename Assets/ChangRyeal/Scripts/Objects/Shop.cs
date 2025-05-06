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
    
    

    // ���������� �Ѿ���� ����
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

    // �ʿ� ������
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


    #region ���� �⹰ ����
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
                    Debug.Log("�� ����");
                }
            }
            else
            {
                Debug.Log("�ڸ� ����");
            }
        }

        // 
        //if(GameManager.Instance.player.CanBuy(Hero[index].cost))
    }
    #endregion

    #region Ư�� �ൿ
    // ���� : 2 ��� �ʿ�
    public void Reroll()
    {
        if (!lockHero)
        {
            if (GameManager.Instance.player.CanBuy(2))
            {
                GameManager.Instance.player.Gold -= 2;
                Debug.Log("����");
                DrawShopHero();
            }
            else
                Debug.Log("�� ����");
        }
        else
        {
            Debug.Log("���");
        }
    }

    // ����ġ ���� : 4���� 4�� ����ġ ȹ��
    public void BuyExp()
    {
        if(GameManager.Instance.player.Level == 10)
        {
            Debug.Log("���� �ִ�");
        }
        else
        {
            if (GameManager.Instance.player.CanBuy(4))
            {
                GameManager.Instance.player.Gold -= 4;
                GameManager.Instance.player.BuyExp();
            }
            else
                Debug.Log("�� ����");
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
