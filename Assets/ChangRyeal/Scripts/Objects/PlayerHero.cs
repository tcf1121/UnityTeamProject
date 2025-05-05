using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

public class PlayerHero : MonoBehaviour
{
    [SerializeField] private List<Hero> allHero;
    [SerializeField] private List<Hero> storageHero;
    [SerializeField] private List<Hero> battleHero;
     public List<Hero> BattleHero { get { return battleHero; } }
    [SerializeField] private Tilemap tileMap;

    // 영웅이 타일 위 어디에 있는가에 대한 정보
    [SerializeField] public Dictionary<Vector3Int, Hero> HeroOnBattle = new Dictionary<Vector3Int, Hero>(); 
    [SerializeField] public Dictionary<Vector3Int, Hero> HeroInStorage = new Dictionary<Vector3Int, Hero>();
    [SerializeField] public GameObject battleManager;

    [Header("Propertis")]
    [SerializeField] GameObject RankUpEffetPrefab;


    public void SetPlayerHero()
    {
        for (int x = -8; x <= 0; x++)
        {
            for (int y = 4; y <= 7; y++)
            {
                HeroOnBattle.Add(new Vector3Int(x, y, 0), null);
            }
        }
        for (int x = -8; x <= 0; x++)
        {
            HeroInStorage.Add(new Vector3Int(x, 3, 0), null);
        }
        tileMap = GameManager.Instance.player.tileMap.tileMap;
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //    int layerMask = 1 << LayerMask.NameToLayer("Ground");
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    RaycastHit hitInfo;
        //    Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

        //    if (hitInfo.collider != null)
        //    {

                
        //        if ((tileMap.WorldToCell(hitInfo.transform.position).x >= -8 &&
        //            tileMap.WorldToCell(hitInfo.transform.position).x <= 0))
        //        {
        //            if ((tileMap.WorldToCell(hitInfo.transform.position).y >= 4 &&
        //            tileMap.WorldToCell(hitInfo.transform.position).y <= 11))
        //                Debug.Log(HeroOnBattle[tileMap.WorldToCell(hitInfo.transform.position)]);
        //            else if (tileMap.WorldToCell(hitInfo.transform.position).y == 3)
        //                Debug.Log(HeroInStorage[tileMap.WorldToCell(hitInfo.transform.position)]);
        //        }


        //    }
        //}
    }
    // 모든 영웅 개수 확인 (최대 값 : 플레이어 레벨 + 9(보관함))
    public int AllHeroNum()
    {
        return allHero.Count;
    }

    // 보관함 영웅 개수 확인 (최대 값 : 9)
    public int StorageHeroNum()
    {
        return storageHero.Count;
    }

    // 전장 영웅 개수 확인 (최대 값 : 플레이어 레벨)
    public int BattleHeroNum()
    {
        return battleHero.Count;
    }

    // 특정 영웅 개수 확인
    public int SpecificHeroNum(Hero hero)
    {
        List<Hero> matchingHeroes = allHero.Where(h => h.name == hero.name && h.star == hero.star).ToList();
        
        return matchingHeroes.Count;
    }

    public List<Hero> SpecificHero(Hero hero)
    {
        List<Hero> matchingHeroes = allHero.Where(h => h.name == hero.name && h.star == hero.star).ToList();
        return matchingHeroes;
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject obj;
        
    }

    // 영웅 다 차있는 지 확인
    public bool FullHero()
    {
        // 다 차 있으면 true 반환
        return AllHeroNum() == GameManager.Instance.player.Level + 9 ? true : false;
    }



    // 플레이어 영웅 기물 추가
    public void NewHero(Hero hero, bool rankUp = false)
    {
        // 그리드 내의 비어 있는 공간 확인
        Vector3Int heroGrid = new Vector3Int(0, 0, 0);
        if (StorageHeroNum() < 9)
        {
            foreach (var storage in HeroInStorage)
            {
                if (storage.Value == null)
                {
                    heroGrid = storage.Key;
                    break;
                }
            }
        }
        else
        {
            
            foreach (var storage in HeroOnBattle)
            {
                if (storage.Value == null)
                {
                    heroGrid = storage.Key;
                    break;
                }
            }
        }
        
        // 오브젝트로 만들어 추가
        hero.GetComponent<Unit>().startPoint = heroGrid;
        Vector3 heroPos = new Vector3(tileMap.CellToWorld(heroGrid).x, 0, tileMap.CellToWorld(heroGrid).z);
        GameObject heroObj = Instantiate(hero.heroObject, heroPos, Quaternion.Euler(new Vector3(0, 180, 0)));
        heroObj.GetComponent<HeroUnitAnimator>().Wait(true);
        if (rankUp)
        {
            Vector3 effctPos = new Vector3(heroPos.x, 1, heroPos.z);
            Instantiate(RankUpEffetPrefab, effctPos, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
            
        Hero getHero = heroObj.GetComponent<Hero>();
        allHero.Add(getHero);
        // 보관함이 꽉 차지 않았다면 보관함에 추가
        if (StorageHeroNum() < 9)
        {
            getHero.SetBattle();
            storageHero.Add(getHero);
            HeroInStorage[heroGrid] = getHero;
        }

        // 보관함이 꽉 찼다면 전장에 추가
        else
        {
            battleHero.Add(getHero);
            HeroOnBattle[heroGrid] = getHero;
        }
        
        //같은 기물이 3개면
        if(SpecificHeroNum(getHero) == 3)
        {
            List<Hero> specitcHeroList = SpecificHero(getHero);
            foreach(Hero specitcHero in specitcHeroList)
            {
                DeleteHero(specitcHero.heroObject);
            }
            NewHero(UpgradeHero(getHero), true);
            
        }
    }

    // 플레이어 영웅 업그레이드
    private Hero UpgradeHero(Hero hero)
    {
        hero.star++;
        hero.enabled = true;
        return hero;
    }
    // 플레이어 영웅 기물 삭제
    public void DeleteHero(GameObject hero)
    {
        Destroy(hero);
        allHero.Remove(hero.GetComponent<Hero>());
        if (hero.GetComponent<Hero>().battle)
        {
            battleHero.Remove(hero.GetComponent<Hero>());
            HeroOnBattle[hero.GetComponent<Unit>().startPoint] = null;
        }
        else
        {
            storageHero.Remove(hero.GetComponent<Hero>());
            HeroInStorage[hero.GetComponent<Unit>().startPoint] = null;
        }

    }

    // 플레이어 영웅 기물 판매
    public void SellHero(GameObject hero)
    {
        // 판매한 히어로 상점에 추가
        GameManager.Instance.player.shop.GetComponent<ShopHeroController>().SellHero(hero.GetComponent<Hero>());
        allHero.Remove(hero.GetComponent<Hero>());
        if (hero.GetComponent<Hero>().battle)
        {
            battleHero.Remove(hero.GetComponent<Hero>());
            HeroOnBattle[hero.GetComponent<Unit>().startPoint] = null;
        }
        else
        {
            storageHero.Remove(hero.GetComponent<Hero>());
            HeroInStorage[hero.GetComponent<Unit>().startPoint] = null;
        }
        Destroy(hero);
    }

    // 움직일려는 칸에 영웅이 있는지 없는지 확인
    public bool CanMove(Vector3Int pos)
    {
        if (pos.y == 3)
            return HeroInStorage[pos] == null ? true : false;
        else
            return HeroOnBattle[pos] == null ? true : false;
    }

    // 영웅 움직이는 함수
    public void MoveHero(Vector3Int before, Vector3Int after, Hero hero)
    {
        // 전에 있던 위치 삭제
        BeforeHero(before, hero);
        // 새로운 위치에 추가
        AfterHero(after, hero);
    }

    // 이동 전에 있는 위치 확인 후 해당 칸에 삭제
    private void BeforeHero(Vector3Int before, Hero hero)
    {
        if (before.y ==3)
        {
            storageHero.Remove(hero);
            HeroInStorage[before] = null;
        }
        else
        {
            battleHero.Remove(hero);
            HeroOnBattle[before] = null;
        }
    }

    // 이동 후에 있는 위치 확인 후 해당 칸에 추가
    private void AfterHero(Vector3Int after, Hero hero)
    {
        if (after.y == 3)
        {
            storageHero.Add(hero);
            HeroInStorage[after] = hero;
        }
        else
        {
            battleHero.Add(hero);
            HeroOnBattle[after] = hero;
        }
    }

    public void SetBattle()
    {
        Vector3Int heroGrid = new();
        GameObject moveHero;
        if (AllHeroNum() != 0 &&
            BattleHeroNum() < GameManager.Instance.player.Level &&
            StorageHeroNum() != 0)
        {
            while(storageHero.Count > 0)
            {
                if (storageHero[0] != null)
                {
                    Debug.Log("배틀 칸 빔");
                    moveHero = storageHero[0].gameObject;
                    foreach (var storage in HeroOnBattle)
                    {
                        if (storage.Value == null)
                        {
                            heroGrid = storage.Key;
                            break;
                        }
                    }
                    MoveHero(moveHero.GetComponent<Unit>().startPoint, heroGrid, storageHero[0]);
                    moveHero.GetComponent<Unit>().startPoint = heroGrid;
                    moveHero.transform.position = tileMap.CellToWorld(heroGrid);
                    moveHero.GetComponent<Hero>().SetBattle();
                }
                if (BattleHeroNum() == GameManager.Instance.player.Level)
                    break;
            }
        }
        battleManager.GetComponent<BattleManager_>().OnBattle();
        battleManager.GetComponent<Synergy>().OnBattle();
    }
}
