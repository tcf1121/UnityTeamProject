using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    // 몬스터가 죽었을 때 골드를 떨어뜨리는 로직 생성

    public int currentGold = 0; // 현재 골드
    public int currentExp = 0; // 현재 경험치
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
        Debug.Log($"{DropGold}골드를 획득하셨습니다!");
        Debug.Log($"{DropExp}경험치를 획득하셨습니다!");
        Debug.Log($"현재 골드는 {currentGold}이고, 경험치는 {currentExp}입니다");
    }
}
