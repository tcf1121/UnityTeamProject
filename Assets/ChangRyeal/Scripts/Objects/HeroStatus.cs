using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus_ : MonoBehaviour
{

    [Header("Status")]
    [SerializeField] public int[] maxHp = new int[3];       // 1��~3�� �ִ� ü��
    [SerializeField] public int[] attack = new int[3];      // 1��~3�� ���ݷ�
    [SerializeField] public int defense;                    // ����
    [SerializeField] public int mana = 100;                       // ���� ����
    [SerializeField] public int addMana = 10;               // ���� ������

    [Header("Etc")]
    public string job;                    // ����
    public string synergy;                // ����
    public float attackSpeed;             // ���� �ӵ�
    public float attackRange;             // ���� ��Ÿ�
    public int magicResist;               // ���� ���׷�


}
