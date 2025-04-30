using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TraceTest : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private float moveInterval = 1.0f;
    [SerializeField] private float moveDuration = 0.25f;
    [SerializeField] private int attackRange = 3;

    private bool isMoving = false;

    private void Start()
    {
        if (tilemap == null)
            tilemap = FindObjectOfType<Tilemap>();

        StartCoroutine(EnemyRoutine());
    }

    //TODO: �̸� �ٲٱ�
    private IEnumerator EnemyRoutine()
    {
        while (true)
        {
            if (!isMoving)
            {
                Transform target = FindNearestHero();
                if (target != null)
                {
                    Vector2Int myXY = GetCurrentCell();
                    Vector2Int targetXY = GetCellOf(target.position);
                    int dist = HexDistance(myXY, targetXY);

                    if (dist <= attackRange)
                    {
                        Debug.Log("����");
                        // TODO: ������
                    }
                    else
                    {
                        Vector2Int direction = GetStepToward(myXY, targetXY);
                        Vector2Int nextXY = myXY + direction;
                        int nextDist = HexDistance(nextXY, targetXY);


                        if (nextDist < dist)
                        {
                            Vector3 targetPos = tilemap.GetCellCenterWorld(new Vector3Int(nextXY.x, nextXY.y, 0));
                            targetPos.y = 2f; // ���� ����
                            StartCoroutine(MoveTo(targetPos));
                        }
                        else
                        {
                            Debug.Log("����");
                            // TODO: ������
                        }
                    }
                }
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }

    private IEnumerator MoveTo(Vector3 targetPos)
    {
        isMoving = true;

        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;
    }

    private Vector2Int GetCurrentCell()
    {
        Vector3Int cell = tilemap.WorldToCell(transform.position);
        return new Vector2Int(cell.x, cell.y);
    }

    private Vector2Int GetCellOf(Vector3 worldPos)
    {
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        return new Vector2Int(cell.x, cell.y);
    }

    //TODO: �Ʊ� �� �� ���� �Լ��� �ٲٱ�
    private Transform FindNearestHero()
    {
        GameObject[] heroes = GameObject.FindGameObjectsWithTag("Hero");
        Transform nearest = null;
        int minDist = int.MaxValue;
        Vector2Int myXY = GetCurrentCell();

        foreach (GameObject hero in heroes)
        {
            Vector2Int heroXY = GetCellOf(hero.transform.position);
            int dist = HexDistance(myXY, heroXY);

            if (dist < minDist)
            {
                minDist = dist;
                nearest = hero.transform;
            }
        }

        return nearest;
    }

    private int HexDistance(Vector2Int a, Vector2Int b)
    {
        int dx = a.x - b.x;
        int dy = a.y - b.y;
        int dz = -dx - dy;
        return (Mathf.Abs(dx) + Mathf.Abs(dy) + Mathf.Abs(dz)) / 2;
    }

    private Vector2Int GetStepToward(Vector2Int from, Vector2Int to)
    {
        Vector2Int[] directionsEven = {
            new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1),
            new Vector2Int(-1, 0), new Vector2Int(-1, -1), new Vector2Int(0, -1)
        };

        Vector2Int[] directionsOdd = {
            new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(0, 1),
            new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, -1)
        };

        Vector2Int[] directions = (from.y % 2 == 0) ? directionsEven : directionsOdd;

        Vector2Int best = Vector2Int.zero;
        int minDist = int.MaxValue;

        foreach (Vector2Int dir in directions)
        {
            Vector2Int candidate = from + dir;

            if (IsCellOccupied(candidate)) 
                continue;

            int dist = HexDistance(candidate, to);

            if (dist < minDist)
            {
                minDist = dist;
                best = dir;
            }
        }

        return best;
    }

    private bool IsCellOccupied(Vector2Int cellPos)
    {
        // ������ ��: �ܺο��� List�� ���� ����
        GameObject[] heros = GameObject.FindGameObjectsWithTag("Hero");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        List<GameObject> allUnits = new List<GameObject>();

        allUnits.AddRange(heros);
        allUnits.AddRange(enemies);

        foreach (GameObject unit in allUnits)
        {
            Vector2Int unitPos = GetCellOf(unit.transform.position);

            if (unitPos == cellPos)
                return true;
        }

        return false;
    }

    //TODO: DIE �޼��忡�� StopCoroutine
}
