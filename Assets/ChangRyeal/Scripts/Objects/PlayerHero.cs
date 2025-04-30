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

    // ������ Ÿ�� �� ��� �ִ°��� ���� ����
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

    // ���� �� ���ִ� �� Ȯ��
    public bool FullHero()
    {
        // �� �� ������ true ��ȯ
        return AllHeroNum() == GameManager.Instance.player.Level + 9 ? true : false;
    }

    public void NewHero(Hero hero)
    {
        // ��� �ִ� ���� Ȯ��
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
        Vector3 heroPos = new Vector3(tileMap.CellToWorld(heroGrid).x, 1, tileMap.CellToWorld(heroGrid).z);
        GameObject getHero = Instantiate(hero.heroObject, heroPos, Quaternion.identity);
        
        allHero.Add(getHero.GetComponent<Hero>());
        // �������� �� ���� �ʾҴٸ� �����Կ� �߰�
        if (StorageHeroNum() < 9)
        {
            getHero.GetComponent<Hero>().SetBattle();
            storageHero.Add(getHero.GetComponent<Hero>());
            HeroInStorage[heroGrid] = getHero.GetComponent<Hero>();
        }

        // �������� �� á�ٸ� ���忡 �߰�
        else
        {
            battleHero.Add(getHero.GetComponent<Hero>());
            HeroOnBattle[heroGrid] = getHero.GetComponent<Hero>();
        }
        
        //���� �⹰�� 3����
        //�� ������ 2�� �⹰�� �߰�
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
        // ���� �ִ� ��ġ ����
        BeforeHero(before, hero);
        // ���ο� ��ġ�� �߰�
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
