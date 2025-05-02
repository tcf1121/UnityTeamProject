using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDataLoader : MonoBehaviour
{
    public HeroStatus status;

    void Awake()
    {
        string prefabName = gameObject.name.Replace("(Clone)", "").Trim();
        status = HeroDatabase.GetHeroByPrefabName(prefabName);

        if (status == null)
        {
            Debug.LogError($"[HeroDataLoader] '{prefabName}'에 해당하는 HeroStatus를 찾을 수 없습니다.");
        }
        else
        {
            Debug.Log($"[HeroDataLoader] '{status.heroId}' 데이터 성공적으로 로드됨");
        }
    }

    void Start()
    {
        if (status != null)
            Debug.Log($"[HeroDataLoader] 로드된 유닛: {status.heroId} / HP: {status.hp[0]}");
    }

}
