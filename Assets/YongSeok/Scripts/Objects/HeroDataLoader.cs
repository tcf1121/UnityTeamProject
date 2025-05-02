using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataLoader : MonoBehaviour
{
    public HeroStatus status;

    void Awake()
    {
        string prefabName = gameObject.name.Replace("(Clone)", "").Trim();
        HeroStatus original = HeroDatabase.GetHeroByPrefabName(prefabName);

        if (original == null)
        {
            Debug.LogError($"[HeroDataLoader] '{prefabName}'에 해당하는 HeroStatus를 찾을 수 없습니다.");
        }
        else
        {
            status = CloneStatus(original);
            Debug.Log($"[HeroDataLoader] '{status.heroId}' 데이터 성공적으로 로드됨 (복제본)");
        }
    }

    void Start()
    {
        if (status != null)
            Debug.Log($"[HeroDataLoader] 로드된 유닛: {status.heroId} / HP: {status.hp[0]}");
 
    }

    private HeroStatus CloneStatus(HeroStatus original)
    {
        return new HeroStatus
        {
            job = original.job,
            heroId = original.heroId,
            prefabName = original.prefabName,
            championName = original.championName,
            cost = original.cost,
            hp = (int[])original.hp.Clone(),
            maxHp = (int[])original.maxHp.Clone(),
            attack = (int[])original.attack.Clone(),
            dps = (float[])original.dps.Clone(),
            attackRange = original.attackRange,
            attackSpeed = original.attackSpeed,
            defense = original.defense,
            magicResist = original.magicResist,
            mana = original.mana,
            addMana = original.addMana
        };
    }

}
