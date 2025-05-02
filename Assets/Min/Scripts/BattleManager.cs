using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] GameObject shopPanel;

    private static BattleManager instance;
    public static BattleManager Instance { get { return instance; } }

    private int heroCounts;
    private int monsterCounts;

    // ���� ���� �� ȣ��
    public void GetHeroNumbers()
    {
        GameObject[] heroTargets = GameObject.FindGameObjectsWithTag("Hero");
        heroCounts = heroTargets.Length;
    }

    // ���� �Ŵ������� ������ ������ ȣ��
    // �� ���� �� ����
    public void GetMonsterNumbers(int numbers)
    {
        monsterCounts += numbers;
    }

    // ������Ʈ�� �׾��� �� ȣ��
    public void CheckVictory()
    {
        // �й�
        if (heroCounts <= 0)
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            GameManager.Instance.player.Health -= monsters.Length * 3;

            //GameManager.Instance.player.Gold -= GameManager.Instance.player.Gold / 10;

            // �� �Ŵ����� - ��ü�� ��� �����ֱ� ������
            //shopPanel.SetActive(true);
        }

        // �¸�
        else if (monsterCounts <= 0)
        {
            GameManager.Instance.player.Stage++;

            //shopPanel.SetActive(true);
        }
    }
}
