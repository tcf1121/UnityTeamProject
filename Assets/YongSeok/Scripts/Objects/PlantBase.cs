using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class PlantBase : MonoBehaviour
{
    /// <summary>
    /// �Ĺ� �⺻ Status
    /// </summary>
    // �⹰�� HP
    [SerializeField] private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    [SerializeField] private float defense;
    // �⹰�� �����ϱ� ���� �ʿ��� �ݾ�
    [SerializeField] private int cost;
    public int Cost
    {
        get { return cost; }
    }
    // �⹰�� ����
    [SerializeField] private int mana;
    public int Mana { get { return mana; } set { mana = value; } }

    ///�߰��� �ʿ��� ���̶� �����Ǵ� �ʵ�
    
    [SerializeField] GameObject PlantObject;
    //���� �߰��� ���� �����ѹ� ���ſ�.
    private int addMana;
    //��ũ �� �̺�Ʈ
    public event Action OnRankUp;
    //�⹰ ����Ʈ ���� �÷��̾�� ó���� ������ ����.
    //private List<GameObject> Units = new List<GameObject>();
    // ��ũ�� ���� �⹰ ���� �ľǿ�
    private int pieceCount;
    // ��ũ �ܰ�
    private int currentRank = 1;
    // Maxhp �ʿ�
    [SerializeField] private int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
    }
    // shop ���Ÿ� ���� �÷��̾� ������
    

    //������ ������ ���� ���� ���� �� ü�� 0 ������Ʈ ��Ȱ��ȭ
    private void OnDamageTaken(int damage)
    {
        if (hp - damage < 0)
        {
            hp = 0;
        }
        else
        {
            hp -= damage;
        }

            mana += addMana;

    if (hp <= 0)
        {
            
            PlantObject.SetActive(false);
            Debug.Log("ä���� 0���� ���� �׾����ϴ�.");
        }
    }

    //�½� ��ũ�� (�̺�Ʈ)
    private void RankUp()
    {
        if(pieceCount >= 3)
        {
            Debug.Log("3�� �̻� �⹰�� �ֽ��ϴ�. ��ũ�� �̺�Ʈ �߻�!");
            OnRankUp?.Invoke();
            currentRank += 1;
            Debug.Log($"���� ��ũ�� : {currentRank} �Դϴ�.");
        }
    }

    // �⹰ ���� (�̺�Ʈ)
    private void SpawmPiece()
    {
        //�ݾ� ���� �÷��̾�� ��ȣ�ۿ� �ʿ�.
        if(cost == 1000)
        {
            //���� ��ġ�� ���� ������ �ʿ���.
            Instantiate(PlantObject, Vector3.zero, Quaternion.identity);

        }
    }

    //���� ���������� �̵��� (�̺�Ʈ)
    private void StageChange()
    {
        //���� ���
        if (hp == 0)
        {
            PlantObject.SetActive(true);
            Debug.Log($"�������� �̵����� �׾��� {PlantObject.name}��/�� �ǻ�Ƴ����ϴ�.");

        }
        // ü���� ������ ���
        else
        {
            hp = maxHp;
        Debug.Log($"�������� �̵����� ü���� ������ {PlantObject.name}��/�� ȸ���߽��ϴ�.")
        }

    }

}
