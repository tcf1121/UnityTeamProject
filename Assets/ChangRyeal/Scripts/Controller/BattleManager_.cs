using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BattleManager_ : MonoBehaviour
{
    [SerializeField] public Dictionary<Vector3Int, GameObject> BattleObject = new Dictionary<Vector3Int, GameObject>();
    [SerializeField] public Synergy synergy;
    // Start is called before the first frame update

    private void OnEnable()
    {

    }

    public void OnBattle()
    {
        SetBattle();
        SetHero(GameManager.Instance.player.playerHero.HeroOnBattle);
    }

    // √ ±‚»≠
    public void SetBattle()
    {
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
        foreach(KeyValuePair<Vector3Int, Hero> hero in HeroOnBattle)
        {
            if (hero.Value != null)
            {
                BattleObject[hero.Key] = hero.Value.gameObject;
                hero.Value.gameObject.GetComponent<HeroStatus_>().SetSynergy(synergy);
                hero.Value.gameObject.GetComponent<HeroStatus_>().SetBattleStatus();
                
            }
                
        }
    }

    public void SetMonster(Vector3Int pos, GameObject monster)
    {
        BattleObject[pos] = monster;
    }

    public void Move(GameObject monster, Vector3Int beforepos, Vector3Int afterpos)
    {
        BattleObject[beforepos] = null;
        BattleObject[afterpos] = monster;
    }
}
