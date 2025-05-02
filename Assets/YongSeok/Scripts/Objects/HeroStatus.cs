using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus : MonoBehaviour
{

    [Header("Status")]
    public int[] hp = new int[3];          // 1성~3성 체력
    public int[] maxHp = new int[3];       // 1성~3성 최대 체력
    public int[] attack = new int[3];      // 1성~3성 공격력
    public float[] dps = new float[3];     // 1성~3성 DPS

    public int defense;                    // 방어력
    public int cost;                       // 코스트
    public int mana;                       // 현재 마나
    public int addMana = 0;               // 마나 증가량

    [Header("Etc")]
    public string job;                     // 직업 (전사, 마법사 등)
    public string heroId;                  // 전사1, 마법사2 등
    public string championName;           // 모티브 챔피언 이름
    public float attackSpeed;             // 공격 속도
    public float attackRange;             // 공격 사거리
    public int magicResist;               // 마법 저항력


}
