using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    // ���Ͱ� �׾��� �� ��带 ����߸��� ���� ����

    public int currentGold = 0; // ���� ���
    public int currentExp = 0; // ���� ����ġ
    public int DropGold;
    public int DropExp;

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

    public void AddGold()
    {
        currentGold += DropGold;
        currentExp += DropExp;
        Debug.Log($"{DropGold}��带 ȹ���ϼ̽��ϴ�!");
        Debug.Log($"{DropExp}����ġ�� ȹ���ϼ̽��ϴ�!");
        Debug.Log($"���� ���� {currentGold}�̰�, ����ġ�� {currentExp}�Դϴ�");
    }
}
