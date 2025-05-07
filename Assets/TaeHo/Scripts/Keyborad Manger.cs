using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboradManger : MonoBehaviour
{
    public Shop Shop;

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.E)) // EXP 备概
        {
            Shop.BuyExp();
        }
        if (Input.GetKeyUp(KeyCode.R)) // 府费
        {
            Shop.Reroll();
        }
        if (Input.GetKeyUp(KeyCode.L)) // 府费 泪陛
        {
            Shop.LockShop();
        }
    }
}
