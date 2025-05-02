using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    [SerializeField] float ObjYPos = 1.5f;

    [SerializeField] GameObject normalMonsterPrefab;
    [SerializeField] GameObject rangerMonsterPrefab;

    [SerializeField] Tilemap tilemap;

    private Coroutine SpawnCoroutine;
    private int spawnWave;

    // 라운드 시작 시 호출
    private void Spawn()
    {
        switch (GameManager.Instance.player.Stage)
        {
            case 1:
                spawnWave = 1;
                //SpawnCoroutine = StartCoroutine(SpawnRoutine);
                break;

            case 2:
                spawnWave = 2;
                break;
        }
    }

    private void EndRound()
    {

    }

    private void SpawnAt(Vector2Int gridPos, GameObject prefab)
    {
        Vector3Int cell = new Vector3Int(gridPos.x, gridPos.y, 0);
        Vector3 worldPos = tilemap.GetCellCenterWorld(cell);
        worldPos.y = ObjYPos;

        Instantiate(prefab, worldPos, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        if (spawnWave != 0)
        {
            SpawnAt(new Vector2Int(-4, 8), normalMonsterPrefab);
            spawnWave--;
            yield return new WaitForSeconds(spawnDelay);
        }
        else
        {

        }
    }
}
