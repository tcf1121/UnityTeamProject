using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHero : MonoBehaviour
{
    [SerializeField] private List<GameObject> allHero;
    [SerializeField] private List<GameObject> storageHero;
    [SerializeField] private List<GameObject> battleHero;
    [SerializeField] private List<GameObject> rankUpHero;
    public List<GameObject> BattleHero { get { return battleHero; } }
    [SerializeField] private Tilemap tileMap;

    // 영웅이 타일 위 어디에 있는가에 대한 정보
    [SerializeField] public Dictionary<Vector3Int, GameObject> HeroOnBattle = new Dictionary<Vector3Int, GameObject>(); 
    [SerializeField] public Dictionary<Vector3Int, GameObject> HeroInStorage = new Dictionary<Vector3Int, GameObject>();
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
    public int SpecificHeroNum(GameObject hero)
    {
        List<GameObject> matchingHeroes = allHero.Where(h => h.name == hero.name &&
        h.GetComponent<Hero>().star == hero.GetComponent<Hero>().star).ToList();
        
        return matchingHeroes.Count;
    }

    public List<GameObject> SpecificHero(GameObject hero)
    {
        List<GameObject> matchingHeroes = allHero.Where(h => h.name == hero.name &&
        h.GetComponent<Hero>().star == hero.GetComponent<Hero>().star).ToList();
        return matchingHeroes;
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
        heroObj.GetComponent<HeroAnimator>().Wait(true);
        heroObj.GetComponent<Hero>().SetHero();
        if (rankUp)
        {
            Vector3 effctPos = new Vector3(heroPos.x, 1, heroPos.z);
            Instantiate(RankUpEffetPrefab, effctPos, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
            
        allHero.Add(heroObj);
        // 보관함이 꽉 차지 않았다면 보관함에 추가
        if (StorageHeroNum() < 9)
        {
            heroObj.GetComponent<Hero>().SetBattle();
            storageHero.Add(heroObj);
            HeroInStorage[heroGrid] = heroObj;
        }

        // 보관함이 꽉 찼다면 전장에 추가
        else
        {
            battleHero.Add(heroObj);
            HeroOnBattle[heroGrid] = heroObj;
            battleManager.GetComponent<Synergy>().OnBattle();
        }
        
        //같은 기물이 3개면
        if(SpecificHeroNum(heroObj) == 3)
        {
            if (!GameManager.Instance.player.Battling)
            {
                List<GameObject> specitcHeroList = SpecificHero(heroObj);
                DeleteHero(specitcHeroList[0]);
                DeleteHero(specitcHeroList[1]);
                DeleteHero(specitcHeroList[2]);
                NewHero(UpgradeHero(heroObj.GetComponent<Hero>()), true);
            }
            else
            {
                rankUpHero.Add(heroObj);
            }
            
        }
    }

    // 전투 중 업그레이드 안 된 기물 업그레이드
    public void UpgradeBattleHero()
    {
        if(rankUpHero.Count > 0)
        {
            foreach(GameObject rankUp in rankUpHero)
            {
                List<GameObject> specitcHeroList = SpecificHero(rankUp);
                DeleteHero(specitcHeroList[0]);
                DeleteHero(specitcHeroList[1]);
                DeleteHero(specitcHeroList[2]);
                NewHero(UpgradeHero(rankUp.GetComponent<Hero>()), true);
            }
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
        
        allHero.Remove(hero);
        if (hero.GetComponent<Hero>().battle)
        {
            battleHero.Remove(hero);
            HeroOnBattle[hero.GetComponent<Unit>().startPoint] = null;
        }
        else
        {
            storageHero.Remove(hero);
            HeroInStorage[hero.GetComponent<Unit>().startPoint] = null;
        }
        Destroy(hero.GetComponent<UI_ObjBar>().objBar.gameObject);
        Destroy(hero);
    }

    // 플레이어 영웅 기물 판매
    public void SellHero(GameObject hero)
    {
        // 판매한 히어로 상점에 추가
        GameManager.Instance.player.shop.GetComponent<ShopHeroController>().SellHero(hero.GetComponent<Hero>());
        allHero.Remove(hero);
        if (hero.GetComponent<Hero>().battle)
        {
            battleHero.Remove(hero);
            HeroOnBattle[hero.GetComponent<Unit>().startPoint] = null;
        }
        else
        {
            storageHero.Remove(hero);
            HeroInStorage[hero.GetComponent<Unit>().startPoint] = null;
        }
        Destroy(hero.GetComponent<UI_ObjBar>().objBar.gameObject);
        Destroy(hero);
    }

    // 움직일려는 칸에 영웅이 있는지 없는지 확인
    public bool CanMove(Vector3Int pos)
    {
        if (pos.y == 3)
            return HeroInStorage[pos] == null ? true : false;
        else
        {

            return HeroOnBattle[pos] == null ? true : false;
        }
            
    }

    public bool CheckBattle(Vector3Int pos)
    {
        if (pos.y == 3)
            return battleHero.Count < GameManager.Instance.player.Level ? true : false;
        else return true;
    }

    // 영웅 움직이는 함수
    public void MoveHero(Vector3Int before, Vector3Int after, GameObject hero)
    {
        // 전에 있던 위치 삭제
        BeforeHero(before, hero);
        // 새로운 위치에 추가
        AfterHero(after, hero);
        battleManager.GetComponent<Synergy>().OnBattle();
    }
    
    public void ChangeHero(GameObject firstHero, Vector3Int firstHeroVec, Vector3Int secondHeroVec, Vector3 firstHeroPos)
    {
        GameObject secondHero = secondHeroVec.y > 3 ? HeroOnBattle[secondHeroVec] : HeroInStorage[secondHeroVec];
        Vector3 secondHeroPos = secondHero.transform.position;
        // 전에 있던 위치 삭제
        BeforeHero(firstHeroVec, firstHero);
        BeforeHero(secondHeroVec, secondHero);

        // 다른 영웅이 있는 위치에 추가
        AfterHero(secondHeroVec, firstHero);
        AfterHero(firstHeroVec, secondHero);

        secondHero.transform.position = firstHeroPos;
        firstHero.transform.position = secondHeroPos;

        firstHero.gameObject.GetComponent<Unit>().startPoint = secondHeroVec;
        firstHero.GetComponent<Hero>().SetBattle();
        secondHero.gameObject.GetComponent<Unit>().startPoint = firstHeroVec;
        secondHero.GetComponent<Hero>().SetBattle();
        battleManager.GetComponent<Synergy>().OnBattle();
    }


    // 이동 전에 있는 위치 확인 후 해당 칸에 삭제
    private void BeforeHero(Vector3Int before, GameObject hero)
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
    private void AfterHero(Vector3Int after, GameObject hero)
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
                    //Debug.Log("배틀 칸 빔");
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
    }   
}
