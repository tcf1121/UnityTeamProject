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
            Debug.LogError($"[HeroDataLoader] '{prefabName}'�� �ش��ϴ� HeroStatus�� ã�� �� �����ϴ�.");
        }
        else
        {
            Debug.Log($"[HeroDataLoader] '{status.heroId}' ������ ���������� �ε��");
        }
    }

    void Start()
    {
        if (status != null)
            Debug.Log($"[HeroDataLoader] �ε�� ����: {status.heroId} / HP: {status.hp[0]}");
    }

}
