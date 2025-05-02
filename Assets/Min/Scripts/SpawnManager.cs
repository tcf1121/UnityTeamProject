using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float spawnDelay;
    [SerializeField] float ObjYPos = 1.5f;

    [SerializeField] GameObject normalMonsterPrefab;
    [SerializeField] GameObject rangerMonsterPrefab;

    [SerializeField] Tilemap tilemap;

    private Vector2Int leftUpPoint = new Vector2Int(-8, 11);
    private Vector2Int middleUpPoint = new Vector2Int(-4, 11);
    private Vector2Int rightUpPoint = new Vector2Int(0, 11);
    private Vector2Int leftDownPoint = new Vector2Int(-8, 8);
    private Vector2Int middleDownPoint = new Vector2Int(-4, 8);
    private Vector2Int rightDownPoint = new Vector2Int(0, 8);


    private Coroutine SpawnCoroutine;
    private int spawnWave;

    // ���� ���� �� ȣ��
    private void Spawn()
    {
        switch (GameManager.Instance.player.Stage)
        {
            case 1:
                spawnWave = 1;
                SpawnCoroutine = StartCoroutine(SpawnRoutine());
                break;

            case 2:
                spawnWave = 2;
                break;
        }
    }

    // ���� ���� �� ȣ��
    private void EndRound()
    {
        StopCoroutine(SpawnCoroutine);
    }

    // �� ��ǥ�� ��ȯ�ϱ� ���� �޼���
    private void SpawnAt(Vector2Int gridPos, GameObject prefab)
    {
        Vector3Int cell = new Vector3Int(gridPos.x, gridPos.y, 0);
        Vector3 worldPos = tilemap.GetCellCenterWorld(cell);
        worldPos.y = ObjYPos;

        Instantiate(prefab, worldPos, Quaternion.identity);
    }

    IEnumerator SpawnRoutine()
    {
        while (spawnWave > 0)
        {
            // ���� ����Ʈ ����ȭ ����
            SpawnAt(new Vector2Int(-4, 8), normalMonsterPrefab);
            BattleManager.Instance.GetMonsterNumbers(1);
            spawnWave--;
            yield return new WaitForSeconds(spawnDelay);
        }

        yield break;
    }
}
