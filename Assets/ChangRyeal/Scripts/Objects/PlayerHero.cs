using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerHero : MonoBehaviour
{
    [SerializeField] private List<Hero> allHero;
    [SerializeField] private List<Hero> storageHero;
    [SerializeField] private List<Hero> battleHero;
    [SerializeField] private List<Hero> rankUpHero;
    public List<Hero> BattleHero { get { return battleHero; } }
    [SerializeField] private Tilemap tileMap;

    // ������ Ÿ�� �� ��� �ִ°��� ���� ����
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
        if (rankUp)
        {
            Vector3 effctPos = new Vector3(heroPos.x, 1, heroPos.z);
            Instantiate(RankUpEffetPrefab, effctPos, Quaternion.Euler(new Vector3(0, 180, 0)));
        }
            
        Hero getHero = heroObj.GetComponent<Hero>();
        allHero.Add(getHero);
        // �������� �� ���� �ʾҴٸ� �����Կ� �߰�
        if (StorageHeroNum() < 9)
        {
            getHero.SetBattle();
            storageHero.Add(getHero);
            HeroInStorage[heroGrid] = getHero;
        }

        // �������� �� á�ٸ� ���忡 �߰�
        else
        {
            battleHero.Add(getHero);
            HeroOnBattle[heroGrid] = getHero;
            battleManager.GetComponent<Synergy>().OnBattle();
        }
        
        //���� �⹰�� 3����
        if(SpecificHeroNum(getHero) == 3)
        {
            if (!GameManager.Instance.player.Battling)
            {
                List<Hero> specitcHeroList = SpecificHero(getHero);
                DeleteHero(specitcHeroList[0].heroObject);
                DeleteHero(specitcHeroList[1].heroObject);
                DeleteHero(specitcHeroList[2].heroObject);
                NewHero(UpgradeHero(getHero), true);
            }
            else
            {
                rankUpHero.Add(getHero);
            }
            
        }
    }

    // ���� �� ���׷��̵� �� �� �⹰ ���׷��̵�
    public void UpgradeBattleHero()
    {
        if(rankUpHero.Count > 0)
        {
            foreach(Hero rankUp in rankUpHero)
            {
                List<Hero> specitcHeroList = SpecificHero(rankUp);
                DeleteHero(specitcHeroList[0].heroObject);
                DeleteHero(specitcHeroList[1].heroObject);
                DeleteHero(specitcHeroList[2].heroObject);
                NewHero(UpgradeHero(rankUp), true);
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

    // �÷��̾� ���� �⹰ �Ǹ�
    public void SellHero(GameObject hero)
    {
        // �Ǹ��� ����� ������ �߰�
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

    // �����Ϸ��� ĭ�� ������ �ִ��� ������ Ȯ��
    public bool CanMove(Vector3Int pos)
    {
        if (pos.y == 3)
            return HeroInStorage[pos] == null ? true : false;
        else
            return HeroOnBattle[pos] == null ? true : false;
    }

    // ���� �����̴� �Լ�
    public void MoveHero(Vector3Int before, Vector3Int after, Hero hero)
    {
        // ���� �ִ� ��ġ ����
        BeforeHero(before, hero);
        // ���ο� ��ġ�� �߰�
        AfterHero(after, hero);
        battleManager.GetComponent<Synergy>().OnBattle();
    }
    
    public void ChangeHero(Hero firstHero, Vector3Int firstHeroVec, Vector3Int secondHeroVec, Vector3 firstHeroPos)
    {
        Hero secondHero = secondHeroVec.y > 3 ? HeroOnBattle[secondHeroVec] : HeroInStorage[secondHeroVec];
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
        firstHero.SetBattle();
        secondHero.gameObject.GetComponent<Unit>().startPoint = firstHeroVec;
        secondHero.SetBattle();
        battleManager.GetComponent<Synergy>().OnBattle();
    }


    // �̵� ���� �ִ� ��ġ Ȯ�� �� �ش� ĭ�� ����
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

    // �̵� �Ŀ� �ִ� ��ġ Ȯ�� �� �ش� ĭ�� �߰�
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
