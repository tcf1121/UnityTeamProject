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

    // 라운드 시작 시 호출
    public void GetHeroNumbers()
    {
        GameObject[] heroTargets = GameObject.FindGameObjectsWithTag("Hero");
        heroCounts = heroTargets.Length;
    }

    // 스폰 매니저에서 스폰될 때마다 호출
    // 한 번에 값 전부
    public void GetMonsterNumbers(int numbers)
    {
        monsterCounts += numbers;
    }

    // 오브젝트가 죽었을 때 호출
    public void CheckVictory()
    {
        // 패배
        if (heroCounts <= 0)
        {
            GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
            GameManager.Instance.player.Health -= monsters.Length * 3;

            //GameManager.Instance.player.Gold -= GameManager.Instance.player.Gold / 10;

            // 샵 매니저로 - 롤체는 계속 켜져있기 때문에
            //shopPanel.SetActive(true);
        }

        // 승리
        else if (monsterCounts <= 0)
        {
            GameManager.Instance.player.Stage++;

            //shopPanel.SetActive(true);
        }
    }
}
