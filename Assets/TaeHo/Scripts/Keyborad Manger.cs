using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboradManger : MonoBehaviour
{
    public Shop Shop;
    [SerializeField] private BattleManager_ battleManager_;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) // EXP ����
        {
            Shop.BuyExp();
        }
        if (Input.GetKeyUp(KeyCode.R)) // ����
        {
            Shop.Reroll();
        }
        if (Input.GetKeyUp(KeyCode.L)) // ���� ���
        {
            Shop.LockShop();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            battleManager_.ShowExitPanel();
        }
    }
}
