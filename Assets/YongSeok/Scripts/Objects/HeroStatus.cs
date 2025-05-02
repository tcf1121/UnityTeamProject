using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStatus : MonoBehaviour
{

    [Header("Status")]
    public int[] hp = new int[3];          // 1��~3�� ü��
    public int[] maxHp = new int[3];       // 1��~3�� �ִ� ü��
    public int[] attack = new int[3];      // 1��~3�� ���ݷ�
    public float[] dps = new float[3];     // 1��~3�� DPS

    public int defense;                    // ����
    public int cost;                       // �ڽ�Ʈ
    public int mana;                       // ���� ����
    public int addMana = 0;               // ���� ������

    [Header("Etc")]
    public string job;                     // ���� (����, ������ ��)
    public string heroId;                  // ����1, ������2 ��
    public string championName;           // ��Ƽ�� è�Ǿ� �̸�
    public float attackSpeed;             // ���� �ӵ�
    public float attackRange;             // ���� ��Ÿ�
    public int magicResist;               // ���� ���׷�


}
