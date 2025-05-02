using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] float ObjYPos = 1.5f;

    // TODO: 기물 종류 추가
    [SerializeField] GameObject normalMonsterPrefab;
    [SerializeField] GameObject rangerMonsterPrefab;

    [SerializeField] Tilemap tilemap;

    // 스폰 포인트
    private Vector2Int leftUpPoint = new Vector2Int(-8, 11);
    private Vector2Int middleUpPoint = new Vector2Int(-4, 11);
    private Vector2Int rightUpPoint = new Vector2Int(0, 11);
    private Vector2Int leftDownPoint = new Vector2Int(-8, 8);
    private Vector2Int middleDownPoint = new Vector2Int(-4, 8);
    private Vector2Int rightDownPoint = new Vector2Int(0, 8);

    private Coroutine SpawnCoroutine;
    private int spawnCount;

    // 라운드 시작 시 호출
    private void Spawn()
    {
        switch (GameManager.Instance.player.Stage)
        {
            case 1:
                spawnCount = 2;
                SpawnCoroutine = StartCoroutine(SpawnRoutine());
                break;

            case 2:
                spawnCount = 3;
                break;
        }
    }

    // 라운드 종료 시 호출
    private void EndRound()
    {
        StopCoroutine(SpawnCoroutine);
    }

    // 셀 좌표로 소환하기 위한 메서드
    private void SpawnAt(Vector2Int gridPos, GameObject prefab)
    {
        Vector3Int cell = new Vector3Int(gridPos.x, gridPos.y, 0);
        Vector3 worldPos = tilemap.GetCellCenterWorld(cell);
        worldPos.y = ObjYPos;

        Instantiate(prefab, worldPos, Quaternion.identity);
        // TODO: z-를 보게 소환
    }

    IEnumerator SpawnRoutine()
    {
        while (spawnCount <= 0)
        {
            if (IsCellEmpty(leftUpPoint))
            {
                SpawnAt(leftUpPoint, returnRandomMonsterType());
                spawnCount--;
            }
            if (IsCellEmpty(middleUpPoint))
            {
                SpawnAt(middleUpPoint, returnRandomMonsterType());
                spawnCount--;
            }
            if (IsCellEmpty(rightUpPoint))
            {
                SpawnAt(rightUpPoint, returnRandomMonsterType());
                spawnCount--;
            }
            // TODO: 아래쪽에도 같은 방식으로 소환
        }

        yield break;
    }

    // 셀이 비어있는지 확인
    private bool IsCellEmpty(Vector2Int cellPos)
    {
        GameObject[] heros = GameObject.FindGameObjectsWithTag("Hero");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");

        List<GameObject> allUnits = new List<GameObject>();

        allUnits.AddRange(heros);
        allUnits.AddRange(enemies);

        foreach (GameObject unit in allUnits)
        {
            Vector3 world = unit.transform.position;
            Vector3Int cell = tilemap.WorldToCell(world);
            Vector2Int unitPos = new Vector2Int(cell.x, cell.y);

            if (unitPos == cellPos)
                return false;
        }

        return true;
    }

    // 랜덤으로 기물을 반환하는 메서드
    // TODO: 만약 앞쪽 행에 원거리 금지, 뒤쪽 행에 근거리 금지면 조건문 추가
    // TODO: 확률 조절은 Range를 늘리고 if문에서 조건식 수정
    private GameObject returnRandomMonsterType()
    {
        if (GameManager.Instance.player.Stage == 1)
        {
            return normalMonsterPrefab;
        }

        // 2 ~ 5
        else if (GameManager.Instance.player.Stage >= 2 && GameManager.Instance.player.Stage <= 5)
        {
            int randomNum = Random.Range(0, 1);

            if (randomNum == 0)
            {
                return normalMonsterPrefab;
            }
            else
            {
                return rangerMonsterPrefab;
            }
        }

        // 6 ~ 10
        else if (GameManager.Instance.player.Stage >= 6 && GameManager.Instance.player.Stage <= 10)
        {
            int randomNum = Random.Range(0, 1);

            if (randomNum == 0)
            {
                return normalMonsterPrefab;
            }
            else
            {
                return rangerMonsterPrefab;
            }
        }

        // 11 ~ 15
        else if (GameManager.Instance.player.Stage >= 11 && GameManager.Instance.player.Stage <= 15)
        {
            int randomNum = Random.Range(0, 1);

            if (randomNum == 0)
            {
                return normalMonsterPrefab;
            }
            else
            {
                return rangerMonsterPrefab;
            }
        }

        // 16 ~ 20
        else
        {
            int randomNum = Random.Range(0, 1);

            if (randomNum == 0)
            {
                return normalMonsterPrefab;
            }
            else
            {
                return rangerMonsterPrefab;
            }
        }
    }
}
