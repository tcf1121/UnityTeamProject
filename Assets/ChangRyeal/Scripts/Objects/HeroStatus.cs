using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus_ : MonoBehaviour
{

    [Header("Status")]
    [SerializeField] public int[] maxHp = new int[3];       // 1성~3성 최대 체력
    [SerializeField] public int[] attack = new int[3];      // 1성~3성 공격력
    [SerializeField] public int defense;                    // 방어력
    [SerializeField] public int mana = 100;                       // 현재 마나
    [SerializeField] public int addMana = 10;               // 마나 증가량

    [Header("Etc")]
    public string job;                    // 직업
    public string synergy;                // 종족
    public float attackSpeed;             // 공격 속도
    public float attackRange;             // 공격 사거리
    public int magicResist;               // 마법 저항력


}
