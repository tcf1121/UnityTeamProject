using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus_ : MonoBehaviour
{
    [System.Serializable]
    public struct Status
    {
        public int[] maxHp;      // 1성~3성 최대 체력
        public int[] attack;     // 1성~3성 공격력
        public int defense;                   // 방어력
        public int magicResist;               // 마법 저항력
        public int range;               // 공격 사거리
        public float attackSpeed;             // 공격 속도
        public int maxMp;                     // 최대 마나
        public int addMana;              // 마나 증가량
        public int critical;             // 치명타 확률
        public float criticalDamage;  // 치명타 데미지
    }

    [Header("Set Status")]
    [SerializeField] public string job;                     // 직업
    [SerializeField] public string synergy;                 // 소속 단체
    [SerializeField] private Status status;  // 스텟

    [Header("Synergy Effect")]
    [SerializeField] private Status s_Status;


    [Header("Battle Status")]
    [SerializeField] public Status b_Status;
    public int CurHp;
    public int CurMana;


    // 현재 시너지 상태에 따라 
    public void SetSynergy(Synergy synergy)
    {
        s_Status.maxHp = new int[1];
        s_Status.attack = new int[1];
        s_Status.maxHp[0] = 0;
        s_Status.attack[0] = 0;
        s_Status.defense = 0;
        s_Status.magicResist = 0;
        s_Status.addMana = 0;
        s_Status.attackSpeed = 0;
        s_Status.range = 0;
        s_Status.critical = 0;
        s_Status.criticalDamage = 0;

        switch (job)
        {
            case "warrior":
                if (synergy.warrior == 2)
                    s_Status.maxHp[0] = 200;
                else if (synergy.warrior == 3)
                    s_Status.maxHp[0] = 400;
                break;
            case "wizard":
                if (synergy.wizard == 2)
                    s_Status.addMana = 5;
                else if (synergy.wizard == 3)
                    s_Status.addMana = 15;
                break;
            case "thief":
                if (synergy.thief == 2)
                    s_Status.critical = 20;
                else if (synergy.thief == 3)
                    s_Status.critical = 40;
                break;
            case "archer":
                if (synergy.archer == 2)
                    s_Status.range = 1;
                else if (synergy.archer == 3)
                    s_Status.range = 2;
                break;
            case "pirate":
                if (synergy.pirate == 2)
                    s_Status.attack[0] = 15;
                else if (synergy.pirate == 3)
                    s_Status.attack[0] = 30;
                break;
        }

        switch (this.synergy)
        {
            case "adventurer":
                if (synergy.adventurer == 5)
                    s_Status.attackSpeed = 0.2f;
                else if (synergy.adventurer >= 2)
                    s_Status.attackSpeed = 0.1f;
                break;
            case "hero":
                if (synergy.hero == 5)
                    s_Status.criticalDamage = 0.75f;
                else if (synergy.hero >= 3)
                    s_Status.criticalDamage = 0.25f;
                break;
            case "cygnus":
                if (synergy.cygnus == 2)
                {
                    s_Status.defense = 5;
                    s_Status.defense = 5;
                }
                else if (synergy.cygnus == 3)
                {
                    s_Status.defense = 15;
                    s_Status.defense = 15;
                }
                else if (synergy.cygnus == 4)
                {
                    s_Status.defense = 25;
                    s_Status.defense = 25;
                }
                else if (synergy.cygnus == 5)
                {
                    s_Status.defense = 40;
                    s_Status.defense = 40;
                }
                break;
        }
    }
    public void SetBattleStatus()
    {
        b_Status.maxHp = new int[1];
        b_Status.attack = new int[1];
        b_Status.maxHp[0] = status.maxHp[GetComponent<Hero>().star-1] + s_Status.maxHp[0];
        b_Status.attack[0] = status.attack[GetComponent<Hero>().star-1] + s_Status.attack[0];
        b_Status.defense = status.defense + s_Status.defense;
        b_Status.magicResist = status.magicResist + s_Status.magicResist;
        b_Status.range = status.range + s_Status.range;
        b_Status.attackSpeed = status.attackSpeed + s_Status.attackSpeed;
        b_Status.maxMp = status.maxMp;
        b_Status.addMana = status.addMana + s_Status.addMana;
        b_Status.critical = status.critical + s_Status.critical;
        b_Status.criticalDamage = status.criticalDamage + s_Status.criticalDamage;
        CurHp = b_Status.maxHp[0];
        CurMana = 0;
    }

    public void addMana()
    {
        CurMana += b_Status.addMana;
        if(CurMana > b_Status.maxMp)
        {
            // 스킬을 쓴다
            // mana 0으로
        }

    }
}