using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnManager_ : MonoBehaviour
{
    public struct setMon
    {
        public int monster;      // 몬스터 종류
        public int num;          // 몬스터 수
    }

    [SerializeField] public BattleManager_ battleManager;
    [SerializeField] List<int> centerSpawnList;
    [SerializeField] List<int> leftSpawnList;
    [SerializeField] List<int> rightSpawnList;
    [SerializeField] List<setMon> centerSpawnMon;
    [SerializeField] List<setMon> elseSpawnMon;
   
    [SerializeField] GameObject centerSpawner;
    [SerializeField] GameObject leftSpawner;
    [SerializeField] GameObject rightSpawner;

    void Start()
    {
        centerSpawnMon = new List<setMon>();
        elseSpawnMon = new List<setMon>();
        setCenterMon();
        setElseMon();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.player.Battling)
        {
            SpawnAll();
        }
            
    }


    public void SpawnAll()
    {
        Spawn(centerSpawner, centerSpawnList);
        Spawn(leftSpawner, leftSpawnList);
        Spawn(rightSpawner, rightSpawnList);
    }


    private void Spawn(GameObject spawnerObj, List<int> spawnList)
    {
        Spawner spawner = spawnerObj.GetComponent<Spawner>();

        if (spawnList.Count != 0 && battleManager.BattleObject[spawner.spawnPos] == null)
        {
            spawner.Respawn(spawnList[0]);
            spawnList.RemoveAt(0);
        }

    }
    private void setCenterMon()
    {
        Random rand = new();
        setMon setMon;
        for(int i = 0; i < 20; i++)
        {
            setMon.monster = rand.Next(5, 8);
            setMon.num = i / 4 + 2;
            centerSpawnMon.Add(setMon);
        }
    }
    private void setElseMon()
    {
        Random rand = new();
        setMon setMon;
        for (int i = 0; i < 20; i++)
        {
            setMon.monster = rand.Next(0, 5);
            setMon.num = i / 4 + 3;
            elseSpawnMon.Add(setMon);
        }
    }


    public void StageSpawnList()
    {
        int stage = GameManager.Instance.player.Stage;
        StageCenterSpawnList(stage);
        StageElseSpwanList(stage);
    }

    private void StageCenterSpawnList(int stage)
    {
        centerSpawnList.Clear();
        for (int i = 0; i < centerSpawnMon[stage - 1].num; i++)
            centerSpawnList.Add(centerSpawnMon[stage - 1].monster);
        switch (stage)
        {
            case 5:
                centerSpawnList.Add(8);
                break;
            case 10:
                centerSpawnList.Add(9);
                break;
            case 15:
                centerSpawnList.Add(10);
                break;
            case 20:
                centerSpawnList.Add(11);
                break;
        }
    }
    private void StageElseSpwanList(int stage)
    {
        leftSpawnList.Clear();
        rightSpawnList.Clear();
        for (int i = 0; i < elseSpawnMon[stage - 1].num; i++)
        {
            leftSpawnList.Add(elseSpawnMon[stage - 1].monster);
            rightSpawnList.Add(elseSpawnMon[stage - 1].monster);
        }
    }
}
