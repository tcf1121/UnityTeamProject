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
        monsterNum = GameObject.Find("SpawnManager").GetComponent<SpawnManager_>().GetSpawnNum();
        SetHero(GameManager.Instance.player.playerHero.BattleHero);
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

    public void SetHero(List<GameObject> battleHero)
    {
        foreach (GameObject hero in battleHero)
        {
            BattleObject[hero.GetComponent<Unit>().startPoint] = hero;
            hero.GetComponent<HeroAnimator>().Wait(false);
            hero.GetComponent<HeroStatus_>().SetSynergy(synergy);
            hero.GetComponent<HeroStatus_>().SetBattleStatus();
            hero.GetComponent<TraceS>().SetAttck();
            hero.GetComponent<TraceS>().Battling();
            heroNum++;
        }
    }

    public void SetMonsterNum(int num)
    {
        monsterNum = num;
    }

    public void SetMonster(Vector3Int pos, GameObject monster)
    {
        BattleObject[pos] = monster;
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
            if(GameManager.Instance.player.Health <= 0)
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
        SetEndHero(GameManager.Instance.player.playerHero.BattleHero);
        
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

    public void SetEndHero(List<GameObject> BattleHero)
    {
        foreach (GameObject hero in BattleHero)
        {
            hero.GetComponent<TraceS>().EndBattling();
            hero.GetComponent<HeroAnimator>().Wait(true);
            if (hero.activeSelf == false)
            {
                hero.GetComponent<UI_ObjBar>().objBar.gameObject.SetActive(true);
                hero.SetActive(true);
            }
            hero.transform.position = tilemap.CellToWorld(hero.GetComponent<Unit>().startPoint);
            hero.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            hero.GetComponent<UI_ObjBar>().hpBar.value = 1f;
            hero.GetComponent<UI_ObjBar>().MpBar.value = 0f;
        }
        GameManager.Instance.player.playerHero.UpgradeBattleHero();
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
