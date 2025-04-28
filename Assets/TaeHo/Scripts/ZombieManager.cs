using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public static ZombieManager instance;

    [Header("Spawn Points")]
    [SerializeField]
    public Transform[] spawnPoints; // 5�� ���� ���� ����

    [Header("Zombie Prefabs")]
    [SerializeField]
    public GameObject normalZombiePrefab;

    [Header("Spawn Settings")]
    [SerializeField]
    public float spawnInterval = 2f; // �� �ʸ��� ����?


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(AutoSpawnZombies());
    }

    private IEnumerator AutoSpawnZombies()
    {
        while (true)
        {
            SpawnNormalZombie();
            yield return new WaitForSeconds(spawnInterval);
        }
    }


    public void SpawnNormalZombie()
    {
        int randomLine = Random.Range(0, spawnPoints.Length);
        Vector3 spawnPosition = spawnPoints[randomLine].position;

        Instantiate(normalZombiePrefab, spawnPosition, Quaternion.identity);
    }


}
