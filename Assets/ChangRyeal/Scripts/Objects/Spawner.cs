using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> Monsters;
    [SerializeField] List<GameObject> monsterBullets;
    [SerializeField] public Vector3Int spawnPos;
    [SerializeField] private TileMapManager tile;

    public void Start()
    {
        spawnPos = tile.tileMap.WorldToCell(gameObject.transform.position);
    }

    public void OnEnable()
    {
    }

    public void Respawn(int monNum)
    {
        int shape = 0;
        if (Monsters[monNum].GetComponent<MonsterStatus>().boss) shape = 0;
        else
        {
            if (GameManager.Instance.player.Stage < 7)
                shape = 0;
            else if (GameManager.Instance.player.Stage < 14)
                shape = 1;
            else
                shape = 2;
        }
        GameObject monster = Instantiate(Monsters[monNum].GetComponent<MonsterStatus>().prefab[shape], tile.tileMap.CellToWorld(spawnPos), Quaternion.Euler(new Vector3(0, 180, 0)));
        monster.AddComponent<MonsterStatus>();
        monster.GetComponent<MonsterStatus>().SetStatus(Monsters[monNum].GetComponent<MonsterStatus>());
        monster.GetComponent<MonsterStatus>().SetBattleStatus();
        GetComponentInParent<SpawnManager_>().battleManager.SetMonster(spawnPos, monster);
        monster.AddComponent<TraceS>();
        if (monster.GetComponent<MonsterStatus>().battleStatus.range == 1)
        {
            Debug.Log("몬스터 사거리 1");
            monster.AddComponent<MeleeAttack_s>();
        }

        else
        {
            Debug.Log("몬스터 사거리 1보다 큼");
            monster.AddComponent<RangedAttack_s>();

            switch (monNum)
            {
                case 5:
                    monster.GetComponent < RangedAttack_s>().projectilePrefab = monsterBullets[0];
                    break;
                case 6:
                    monster.GetComponent < RangedAttack_s>().projectilePrefab = monsterBullets[1];
                    break;
                case 7:
                    monster.GetComponent < RangedAttack_s>().projectilePrefab = monsterBullets[2];
                    break;
                case 11:
                    monster.GetComponent < RangedAttack_s>().projectilePrefab = monsterBullets[3];
                    break;
            }



        }
        monster.GetComponent<TraceS>().targetTag = "Hero";
        monster.GetComponent<TraceS>().SetAttck();

        monster.name = Monsters[monNum].name;
    }
}
