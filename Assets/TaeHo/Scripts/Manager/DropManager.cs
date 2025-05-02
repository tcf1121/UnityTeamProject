using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    // ���Ͱ� �׾��� �� ��带 ����߸��� Manager ����

    public static DropManager instance { get; private set; }

    public int currentGold = 0; // ���� ���

    private void Awake()
    {
        if  (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void AddGold(int goldAmount)
    {
        currentGold += goldAmount;
        Debug.Log($"{goldAmount}��带 ȹ���ϼ̽��ϴ�!");
        Debug.Log($"���� ���� {currentGold}�Դϴ�.");
    }
}
