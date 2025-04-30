using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHero : MonoBehaviour
{
    [SerializeField] private List<Hero> allHero;
    [SerializeField] private List<Hero> storageHero;
    [SerializeField] private List<Hero> battleHero;
    [SerializeField] private Tilemap tileMap;

    // 영웅이 타일 위 어디에 있는가에 대한 정보
    [SerializeField] public Dictionary<Vector3Int, Hero> HeroOnBattle = new Dictionary<Vector3Int, Hero>(); 
    [SerializeField] public Dictionary<Vector3Int, Hero> HeroInStorage = new Dictionary<Vector3Int, Hero>();


    private void Start()
    {
        for (int x = -8; x <= 0; x++)
        {
            for (int y = 4; y <= 11; y++)
            {
                HeroOnBattle.Add(new Vector3Int(x, y, 0), null);
            }
        }
        for (int x = -8; x <= 0; x++)
        {
            HeroInStorage.Add(new Vector3Int(x, 3, 0), null);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

            if (hitInfo.collider != null)
            {
                Debug.Log(tileMap.WorldToCell(hitInfo.transform.position));

                
                if ((tileMap.WorldToCell(hitInfo.transform.position).x >= -8 &&
                    tileMap.WorldToCell(hitInfo.transform.position).x <= 0))
                {
                    if ((tileMap.WorldToCell(hitInfo.transform.position).y >= 4 &&
                    tileMap.WorldToCell(hitInfo.transform.position).y <= 11))
                        Debug.Log(HeroOnBattle[tileMap.WorldToCell(hitInfo.transform.position)]);
                    else if (tileMap.WorldToCell(hitInfo.transform.position).y == 3)
                        Debug.Log(HeroInStorage[tileMap.WorldToCell(hitInfo.transform.position)]);
                }


            }
        }
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

    // 영웅 다 차있는 지 확인
    public bool FullHero()
    {
        // 다 차 있으면 true 반환
        return AllHeroNum() == GameManager.Instance.player.Level + 9 ? true : false;
    }

    public void NewHero(Hero hero)
    {
        // 비어 있는 공간 확인
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
        Vector3 heroPos = new Vector3(tileMap.CellToWorld(heroGrid).x, 1, tileMap.CellToWorld(heroGrid).z);
        GameObject getHero = Instantiate(hero.heroObject, heroPos, Quaternion.identity);
        
        allHero.Add(getHero.GetComponent<Hero>());
        // 보관함이 꽉 차지 않았다면 보관함에 추가
        if (StorageHeroNum() < 9)
        {
            getHero.GetComponent<Hero>().SetBattle();
            storageHero.Add(getHero.GetComponent<Hero>());
            HeroInStorage[heroGrid] = getHero.GetComponent<Hero>();
        }

        // 보관함이 꽉 찼다면 전장에 추가
        else
        {
            battleHero.Add(getHero.GetComponent<Hero>());
            HeroOnBattle[heroGrid] = getHero.GetComponent<Hero>();
        }
        
        //같은 기물이 3개면
        //다 버리고 2성 기물을 추가
    }

    public void SellHero(GameObject hero)
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

    public bool CanMove(Vector3Int pos)
    {
        if (pos.y == 3)
            return HeroInStorage[pos] == null ? true : false;
        else
            return HeroOnBattle[pos] == null ? true : false;
    }

    public void MoveHero(Vector3Int before, Vector3Int after, Hero hero)
    {
        // 전에 있던 위치 삭제
        BeforeHero(before, hero);
        // 새로운 위치에 추가
        AfterHero(after, hero);
    }

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
}
