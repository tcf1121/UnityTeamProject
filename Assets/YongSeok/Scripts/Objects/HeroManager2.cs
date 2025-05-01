using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HeroManager2 : MonoBehaviour
{
    private List<Hero> trackedHeroes = new();

    private PlayerHero playerHero; // PlayerHero 참조 추가

    private void Start()
    {
        playerHero = FindObjectOfType<PlayerHero>();
        if (playerHero == null)
        {
            Debug.LogError("[HeroManager2] PlayerHero를 찾을 수 없습니다.");
            return;
        }

        InvokeRepeating(nameof(ScanHeroes), 0f, 0.2f);
    }
    private void ScanHeroes()
    {
        Hero[] heroesInScene = FindObjectsOfType<Hero>();

        foreach (var hero in heroesInScene)
        {
            if (!trackedHeroes.Contains(hero))
            {
                trackedHeroes.Add(hero);
                Debug.Log($"[HeroManager2] 히어로 감지: {hero.heroname} {hero.star}성");
            }
        }

        foreach (var hero in trackedHeroes.ToList())
        {
            TryRankUp(hero);
        }
    }

    private void TryRankUp(Hero baseHero)
    {
        int targetStar = baseHero.star;


        string baseKey = baseHero.heroObject.name;

        var matches = trackedHeroes
            .Where(h => h.heroObject.name == baseKey && h.star == targetStar)
            .ToList();

        if (matches.Count >= 3)
        {
            Debug.Log($"[HeroManager2] 합성 조건 충족 → {baseHero.heroname} {targetStar + 1}성");

            var mergeTargets = matches.Take(3).ToList();
            Hero last = mergeTargets[2];
            Vector3 spawnPos = last.transform.position;
            Vector3Int spawnGrid = last.GetComponent<Unit>().startPoint;

            foreach (var h in mergeTargets)
            {
                Vector3Int grid = h.GetComponent<Unit>().startPoint;

                if (grid.y == 3)
                    playerHero.HeroInStorage[grid] = null;
                else
                    playerHero.HeroOnBattle[grid] = null;

                trackedHeroes.Remove(h);
                Destroy(h.gameObject);
            }

            SpawnUpgradedHero(baseHero, spawnPos, spawnGrid, last.heroObject);
        }
    }

    private void SpawnUpgradedHero(Hero baseHero, Vector3 pos, Vector3Int grid, GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("[HeroManager2] 합성용 프리팹이 null입니다.");
            return;
        }

        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        Hero newHero = go.GetComponent<Hero>();

        newHero.heroname = baseHero.heroname;
        newHero.star = baseHero.star + 1;
        newHero.GetComponent<Unit>().startPoint = grid;
        newHero.SetBattle();

        trackedHeroes.Add(newHero);

        if (grid.y == 3)
            playerHero.HeroInStorage[grid] = newHero;
        else
            playerHero.HeroOnBattle[grid] = newHero;

        Debug.Log($"[HeroManager2] 합성 완료: {newHero.heroname} {newHero.star}성 at {grid}");
    }
}