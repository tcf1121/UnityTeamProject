using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

namespace YongSeok
{
    public class GameManager : MonoBehaviour
    {
        [Header("Hero Prefabs")]
        [SerializeField] private List<HeroPrefabData> heroPrefabs;
        private Dictionary<HeroType, GameObject> heroPrefabDict;

        [Header("Grid Info")]
        [SerializeField] private Dictionary<int, Vector2Int> gridPositions = new Dictionary<int, Vector2Int>();
        [SerializeField] private Tilemap tilemap; // 타일맵 연결 필요

        // 변경됨: Vector2Int를 키로 사용
        private Dictionary<Vector3Int, HeroBase> occupiedPositions = new Dictionary<Vector3Int, HeroBase>();
        private List<Vector3Int> heroSpawnPositions = new List<Vector3Int>();

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

            // 고정된 스폰 좌표 초기화
            for (int x = -8; x <= 0; x++)
            {
                Vector3Int cell = new Vector3Int(x, 3, 0);
                heroSpawnPositions.Add(cell);
                occupiedPositions[cell] = null;
            }


        }

       


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                TrySpawnHero(HeroType.warriorHero);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                TrySpawnHero(HeroType.tankerHero);
            }
           


        }

        private void TrySpawnHero(HeroType heroType)
        {
            foreach (var cell in heroSpawnPositions)
            {
                if (occupiedPositions[cell] == null)
                {
                    Vector3 worldPos = GridToWorldPosition(cell);
                    GameObject heroObj = Instantiate(heroPrefabDict[heroType], worldPos, Quaternion.identity);
                    HeroBase hero = heroObj.GetComponent<HeroBase>();
                    hero.Init(cell, worldPos); //  Init에 Vector3Int 전달

                    if (heroManager != null)
                        heroManager.RegisterHero(hero);

                    occupiedPositions[cell] = hero;

                    Debug.Log($"[GameManager] {heroType} 유닛 생성 완료 at {cell}");
                    return;
                }
            }

            Debug.LogWarning("[GameManager] 유닛을 배치할 빈 셀이 없습니다.");
        }




        public void SpawnTestHero(HeroType heroType, Vector3Int gridPos)
        {
            if (!heroPrefabDict.ContainsKey(heroType))
            {
                Debug.LogError($"[GameManager] {heroType} 타입을 찾을 수 없습니다!");
                return;
            }

            Vector3 spawnPos = GridToWorldPosition(gridPos);

            GameObject heroObj = Instantiate(heroPrefabDict[heroType], spawnPos, Quaternion.identity);
            HeroBase hero = heroObj.GetComponent<HeroBase>();
            hero.Init(gridPos, spawnPos); //  Vector3Int

            if (heroManager != null)
            {
                heroManager.RegisterHero(hero);
            }

            Debug.Log($"[GameManager] {heroType} 생성 완료 at {gridPos}");
        }

        private Vector3 GridToWorldPosition(Vector3Int cellPos)
        {
            return tilemap.GetCellCenterWorld(cellPos);
        }
    }
}
