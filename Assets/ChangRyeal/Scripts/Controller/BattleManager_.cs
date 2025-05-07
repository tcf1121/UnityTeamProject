using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
//using static UnityEditor.PlayerSettings;

public class BattleManager_ : MonoBehaviour
{
    [SerializeField] public Dictionary<Vector3Int, GameObject> BattleObject = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] public Synergy synergy;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private int heroNum;
    [SerializeField] private int monsterNum;
    [SerializeField] private GameObject readyBtn;

    [SerializeField] private GameObject exitPanel;
    [SerializeField] private Button exitButton;

    public event Action OnBattleEnd;
    // Start is called before the first frame update

    private void OnEnable()
    {
        OnBattleEnd += EndBattle;
    }
    
    public void OnBattle()
    {
        SetBattle();
        SetHero(GameManager.Instance.player.playerHero.HeroOnBattle);
    }

    // ì´?ê¸°??
    public void SetBattle()
    {
        heroNum = 0;
        monsterNum = 0;
        BattleObject.Clear();
        for (int x = -8; x <= 0; x++)
        {
            for (int y = 4; y <= 11; y++)
            {
                BattleObject.Add(new Vector3Int(x, y, 0), null);
            }
        }
    }

    public void SetHero(Dictionary<Vector3Int, Hero> HeroOnBattle)
    {
        foreach (KeyValuePair<Vector3Int, Hero> hero in HeroOnBattle)
        {
            if (hero.Value != null)
            {
                BattleObject[hero.Key] = hero.Value.gameObject;
                hero.Value.gameObject.GetComponent<HeroAnimator>().Wait(false);
                hero.Value.gameObject.GetComponent<HeroStatus_>().SetSynergy(synergy);
                hero.Value.gameObject.GetComponent<HeroStatus_>().SetBattleStatus();
                hero.Value.gameObject.GetComponent<TraceS>().SetAttck();
                hero.Value.gameObject.GetComponent<TraceS>().Battling();
                heroNum++;
            }

        }
    }

    public void SetMonster(Vector3Int pos, GameObject monster)
    {
        BattleObject[pos] = monster;
        monsterNum++;
    }

    public void Move(GameObject obj, Vector3Int beforepos, Vector3Int afterpos)
    {
        BattleObject[beforepos] = null;
        BattleObject[afterpos] = obj;
    }

    public void DieBattleObj(GameObject obj)
    {
        BattleObject[tilemap.WorldToCell(obj.transform.position)] = null;
        if (obj.GetComponent<Hero>() != null)
        {
            heroNum--;
            if (heroNum == 0)
                OnBattleEnd?.Invoke();
        }
        else
        {
            monsterNum--;
            if (monsterNum == 0)
                OnBattleEnd?.Invoke();
        }
    }

    private void EndBattle()
    {
        // ì¡Œì????
        if (heroNum == 0)
        {
            GameManager.Instance.player.Health -= MonterDamage();
            ShowExitPanel();
        }
        // ?´ê²¼????
        else
        {
            GameManager.Instance.player.Stage++;
            if (GameManager.Instance.player.Stage > 20)
            {
                GameManager.Instance.player.Stage = 20;
                ShowExitPanel();
            }
        }
        TileReservation.Clear();
        GameManager.Instance.player.Battling = false;
        GameManager.Instance.player.Expplus();
        GameManager.Instance.player.playerHero.UpgradeBattleHero();
        SetEndHero(GameManager.Instance.player.playerHero.HeroOnBattle);
        readyBtn.SetActive(true);
    }


    private int MonterDamage()
    {
        int damage = 0;
        foreach (var s in BattleObject)
        {
            if (s.Value != null)
            {
                if (s.Value.GetComponent<MonsterStatus>() != null)
                {
                    damage += s.Value.GetComponent<MonsterStatus>().battleStatus.damage;
                    Destroy(s.Value);
                }
            }
        }
        return damage;
    }

    public void SetEndHero(Dictionary<Vector3Int, Hero> HeroOnBattle)
    {
        foreach (KeyValuePair<Vector3Int, Hero> hero in HeroOnBattle)
        {
            if (hero.Value != null)
            {
                //Debug.Log(hero.Key);
                hero.Value.gameObject.transform.position = tilemap.CellToWorld(hero.Key);
                hero.Value.gameObject.GetComponent<HeroAnimator>().Wait(true);
                hero.Value.gameObject.GetComponent<TraceS>().EndBattling();
                if (hero.Value.gameObject.activeSelf == false)
                    hero.Value.gameObject.SetActive(true);
                hero.Value.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

        }
    }

    [ContextMenu("ShowExitPanel")]
    public void ShowExitPanel()
    {
        exitPanel.SetActive(!exitPanel.activeSelf);
    }

    public void ExitButton()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                                Application.Quit();
        #endif
    }
}
