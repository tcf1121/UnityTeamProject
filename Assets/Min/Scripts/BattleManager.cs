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

    private void Awake()
    {
        SetSingleton();

        // 이벤트 구독
        // 라운드 시작 시 발생하는 이벤트 GetHeroNumbers 구독
        // 스폰 매니저에서 스폰될 때 GetMonsterNumbers 호출
        // 오브젝트가 죽었을 때 발생하는 이벤트 CheckVictory 구독
    }

    // 각자 싱글톤 or 게임매니저에서 본 클래스 사용?
    private void SetSingleton()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // 라운드 시작 시 호출
    public void GetHeroNumbers()
    {
        GameObject[] heroTargets = GameObject.FindGameObjectsWithTag("Hero");
        heroCounts = heroTargets.Length;
    }

    // 스폰 매니저에서 스폰될 때마다 호출
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

            shopPanel.SetActive(true);
        }

        // 승리
        else if (monsterCounts <= 0)
        {
            GameManager.Instance.player.Stage++;

            shopPanel.SetActive(true);
        }
    }
}
