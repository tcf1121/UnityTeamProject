using System.Collections.Generic;
using UnityEngine;

namespace YongSeok
{
    public class GameManager : MonoBehaviour
    {
        [Header("Hero Prefabs")]
        [SerializeField] private List<HeroPrefabData> heroPrefabs;

        private Dictionary<HeroType, GameObject> heroPrefabDict;

        [SerializeField] private HeroManager heroManager;

        private void Awake()
        {
            heroPrefabDict = new Dictionary<HeroType, GameObject>();

            foreach (var prefabData in heroPrefabs)
            {
                HeroBase heroBase = prefabData.prefab.GetComponent<HeroBase>();
                if (heroBase == null)
                {
                    Debug.LogError($"[GameManager] {prefabData.prefab.name}에 HeroBase 컴포넌트가 없습니다!");
                    continue;
                }

                HeroType heroType = heroBase.HeroType;

                if (!heroPrefabDict.ContainsKey(heroType))
                {
                    heroPrefabDict.Add(heroType, prefabData.prefab);
                }
                else
                {
                    Debug.LogWarning($"[GameManager] 중복 HeroType: {heroType}");
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SpawnTestHero(HeroType.normalHero, new Vector2Int(2, 3));
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SpawnTestHero(HeroType.tankerHero, new Vector2Int(5, 2));
            }
        }

        public void SpawnTestHero(HeroType heroType, Vector2Int gridPos)
        {
            if (!heroPrefabDict.ContainsKey(heroType))
            {
                Debug.LogError($"[GameManager] {heroType} 타입을 찾을 수 없습니다!");
                return;
            }

            Vector3 spawnPos = GridToWorldPosition(gridPos);

            GameObject heroObj = Instantiate(heroPrefabDict[heroType], spawnPos, Quaternion.identity);
            HeroBase hero = heroObj.GetComponent<HeroBase>();
            hero.Init(gridPos, spawnPos);

            if (heroManager != null)
            {
                heroManager.RegisterHero(hero);
            }

            Debug.Log($"[GameManager] {heroType} 생성 완료 at {gridPos}");
        }

        private Vector3 GridToWorldPosition(Vector2Int gridPos)
        {
            float fixedHeight = 1f;
            return new Vector3(gridPos.x, fixedHeight, gridPos.y);
        }
    }
}
