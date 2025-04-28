using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager : MonoBehaviour
{
    private List<HeroBase> allHeroes = new List<HeroBase>();

    /// <summary>
    /// 히어로가 생성될 때 HeroManager에 등록
    /// </summary>
    public void RegisterHero(HeroBase hero)
    {
        allHeroes.Add(hero);
        CheckRankUp(hero);
    }

    /// <summary>
    /// 등록된 히어로를 기준으로 랭크업 검사
    /// </summary>
    private void CheckRankUp(HeroBase newHero)
    {
        var matchingHeroes = allHeroes
            .Where(h => h.HeroType == newHero.HeroType && h.CurrentRank == newHero.CurrentRank)
            .ToList();

        if (matchingHeroes.Count >= 3)
        {
            Debug.Log($"[랭크업 발생] 타입: {newHero.HeroType}, 현재랭크: {newHero.CurrentRank}");

            Vector3 spawnPosition = GetCenterPosition(matchingHeroes);

            // 3개 삭제
            foreach (var hero in matchingHeroes.Take(3))
            {
                allHeroes.Remove(hero);
                Destroy(hero.gameObject);
            }

            // 새 히어로 생성 (랭크+1)
            SpawnUpgradedHero(newHero, spawnPosition);
        }
    }

    /// <summary>
    /// 랭크업된 히어로 생성
    /// </summary>
    private void SpawnUpgradedHero(HeroBase baseHero, Vector3 spawnPosition)
    {
        if (baseHero.HeroObject == null)
        {
            Debug.LogError($"[HeroManager] {baseHero.HeroType}의 heroObject가 연결되지 않았습니다!");
            return;
        }

        GameObject newHeroObj = Instantiate(baseHero.HeroObject, spawnPosition, Quaternion.identity);
        HeroBase newHero = newHeroObj.GetComponent<HeroBase>();

        // 새 히어로 세팅
        newHero.IncreaseRank(); // currentRank + 1

        allHeroes.Add(newHero);

        Debug.Log($"새로운 {newHero.HeroType} 생성 완료! 랭크 {newHero.CurrentRank}");
    }

    /// <summary>
    /// 여러 히어로들의 중앙 좌표 계산
    /// </summary>
    private Vector3 GetCenterPosition(List<HeroBase> heroes)
    {
        Vector3 center = Vector3.zero;
        foreach (var hero in heroes)
        {
            center += hero.transform.position;
        }
        center /= heroes.Count;
        return center;
    }

}
