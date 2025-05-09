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

    // ������ Ÿ�� �� ��� �ִ°��� ���� ����
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

    // ��� ���� ���� Ȯ�� (�ִ� �� : �÷��̾� ���� + 9(������))
    public int AllHeroNum()
    {
        return allHero.Count;
    }

    // ������ ���� ���� Ȯ�� (�ִ� �� : 9)
    public int StorageHeroNum()
    {
        return storageHero.Count;
    }

    // ���� ���� ���� Ȯ�� (�ִ� �� : �÷��̾� ����)
    public int BattleHeroNum()
    {
        return battleHero.Count;
    }

    // Ư�� ���� ���� Ȯ��
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

    // ���� �� ���ִ� �� Ȯ��
    public bool FullHero()
    {
        // �� �� ������ true ��ȯ
        return AllHeroNum() == GameManager.Instance.player.Level + 9 ? true : false;
    }



    // �÷��̾� ���� �⹰ �߰�
    public void NewHero(Hero hero, bool rankUp = false)
    {
        // �׸��� ���� ��� �ִ� ���� Ȯ��
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
        
        // ������Ʈ�� ����� �߰�
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
        // �������� �� ���� �ʾҴٸ� �����Կ� �߰�
        if (StorageHeroNum() < 9)
        {
            heroObj.GetComponent<Hero>().SetBattle();
            storageHero.Add(heroObj);
            HeroInStorage[heroGrid] = heroObj;
        }

        // �������� �� á�ٸ� ���忡 �߰�
        else
        {
            battleHero.Add(heroObj);
            HeroOnBattle[heroGrid] = heroObj;
            battleManager.GetComponent<Synergy>().OnBattle();
        }
        
        //���� �⹰�� 3����
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

    // ���� �� ���׷��̵� �� �� �⹰ ���׷��̵�
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


    // �÷��̾� ���� ���׷��̵�
    private Hero UpgradeHero(Hero hero)
    {
        hero.star++;
        hero.enabled = true;
        return hero;
    }
    // �÷��̾� ���� �⹰ ����
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

    // �÷��̾� ���� �⹰ �Ǹ�
    public void SellHero(GameObject hero)
    {
        // �Ǹ��� ����� ������ �߰�
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

    // �����Ϸ��� ĭ�� ������ �ִ��� ������ Ȯ��
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

    // ���� �����̴� �Լ�
    public void MoveHero(Vector3Int before, Vector3Int after, GameObject hero)
    {
        // ���� �ִ� ��ġ ����
        BeforeHero(before, hero);
        // ���ο� ��ġ�� �߰�
        AfterHero(after, hero);
        battleManager.GetComponent<Synergy>().OnBattle();
    }
    
    public void ChangeHero(GameObject firstHero, Vector3Int firstHeroVec, Vector3Int secondHeroVec, Vector3 firstHeroPos)
    {
        GameObject secondHero = secondHeroVec.y > 3 ? HeroOnBattle[secondHeroVec] : HeroInStorage[secondHeroVec];
        Vector3 secondHeroPos = secondHero.transform.position;
        // ���� �ִ� ��ġ ����
        BeforeHero(firstHeroVec, firstHero);
        BeforeHero(secondHeroVec, secondHero);

        // �ٸ� ������ �ִ� ��ġ�� �߰�
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


    // �̵� ���� �ִ� ��ġ Ȯ�� �� �ش� ĭ�� ����
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

    // �̵� �Ŀ� �ִ� ��ġ Ȯ�� �� �ش� ĭ�� �߰�
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
                    //Debug.Log("��Ʋ ĭ ��");
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
