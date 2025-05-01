using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    // ���Ͱ� �׾��� �� ��带 ����߸��� Manager ����

    public int currentGold = 0; // ���� ���

    public void OnEnable()
    {
        NormalZombie1.OnZombieDied += AddGold;
    }

    public void OnDisable()
    {
        NormalZombie1.OnZombieDied -= AddGold;
    }

    public void AddGold(int goldAmount)
    {
        currentGold += goldAmount;
        Debug.Log($"{goldAmount}��带 ȹ���ϼ̽��ϴ�!");
        Debug.Log($"���� ���� {currentGold}�Դϴ�.");
    }
}
