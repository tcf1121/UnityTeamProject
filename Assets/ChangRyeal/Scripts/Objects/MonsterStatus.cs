using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : MonoBehaviour
{
    [System.Serializable]
    public struct Status
    {
        public int maxHp;                   // �ִ� ü��
        public int attack;                  // ���ݷ�
        public int defense;                 // ����
        public int magicResist;             // ���� ���׷�
        public int range;                   // ���� ��Ÿ�
        public float attackSpeed;           // ���� �ӵ�
        public int damage;                  // �÷��̾�� �ִ� ���ط�
    }

    [Header("Set Status")]
    [SerializeField] public bool boss;             // ���� ���� ����
    [SerializeField] private Status status;         // ����

    [Header("Battle Status")]
    [SerializeField] public Status battleStatus;  // ����

    [Header("Look")]
    [SerializeField] public GameObject[] prefab;    // ����
    public void SetStatus(MonsterStatus ms)
    {
        boss = ms.boss;
        status.maxHp = ms.status.maxHp;
        status.attack = ms.status.attack;
        status.defense = ms.status.defense;
        status.magicResist = ms.status.magicResist;
        status.range = ms.status.range;
        status.attackSpeed = ms.status.attackSpeed;
        status.damage = ms.status.damage;
    }

    public void SetBattleStatus()
    {
        float stage;
        if (boss)
        {
            stage = 1f;
            battleStatus.damage = status.damage;
        }
        else
        {
            stage = 1f + (0.2f * GameManager.Instance.player.Stage);
            if(GameManager.Instance.player.Stage < 7)
                battleStatus.damage = status.damage;
            else if(GameManager.Instance.player.Stage < 14)
                battleStatus.damage = status.damage * 2;
            else
                battleStatus.damage = status.damage * 3;
        }
        battleStatus.maxHp = (int)(status.maxHp * stage);
        battleStatus.attack = (int)(status.attack * stage);
        battleStatus.defense = status.defense;
        battleStatus.magicResist = status.magicResist;
        battleStatus.range = status.range;
        battleStatus.attackSpeed = status.attackSpeed;

    }
}
