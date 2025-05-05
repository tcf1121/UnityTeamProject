using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trace : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private string targetTag;

    [SerializeField] private float moveInterval = 1.0f; // 행동 결정 시간
    [SerializeField] private float moveDuration = 1.0f; // 이동 시간
    [SerializeField] private int attackRange = 3; // 감지 범위, 공격 사거리랑 다름
    [SerializeField] private float attackSpeed = 1.0f;

    [SerializeField] private float ObjYPos = 1.5f;

    //private HeroUnitAnimator animator;

    private bool isMoving = false;
    private Coroutine unitCoroutine;
    private Coroutine attackCoroutine;

    private Transform target;
    public Transform Target { get; set; }

    private void Awake()
    {
        //animator = GetComponent<HeroUnitAnimator>();
    }

    private void Start()
    {
        if (tilemap == null)
            tilemap = FindObjectOfType<Tilemap>();


        // TODO: 이벤트 구독 시 아래 코드 2줄 삭제
        if (gameObject.CompareTag("Monster") || gameObject.CompareTag("Hero"))
            unitCoroutine = StartCoroutine(UnitRoutine());

        // TODO: 게임이 끝났을 때, TileReservation.Clear();

        // 클래스이름.OnBattlingChanged += BattleOnOff;
    }

    // 구독용 함수
    private void BattleOnOff()
    {
        //if (클래스이름.battling) // 클래스가 static이 아니면, 필드에 클래스 추가
        //{
        //    if (gameObject.CompareTag("Monster") || gameObject.CompareTag("Hero"))
        //        unitCoroutine = StartCoroutine(UnitRoutine());
        //}
        //else
        //{
        //    StopCoroutine(unitCoroutine);
        //}
    }


    private void OnDestroy()
    {
        StopCoroutine(unitCoroutine);
    }

    private IEnumerator UnitRoutine()
    {
        target = FindNearestTarget(); // 도발 스킬 위해 필드로 올림

        while (true)
        {
            if (!isMoving)
            {
                if (target == null)
                {
                    target = FindNearestTarget();
                    yield return new WaitForSeconds(moveInterval);
                    continue;
                }

                Vector2Int myXY = GetCurrentCell();
                Vector2Int targetXY = GetCellOf(target.position);
                int dist = HexDistance(myXY, targetXY);

                Debug.Log($"{name} 위치: {myXY} / 타겟 위치: {targetXY} / 거리: {dist} / 사거리: {attackRange}");


                // while (dist <= attackRange) 일 때, TryAttack 호출
                // 공격 속도 및 moveInterval 삭제
                if (dist <= attackRange)
                {
                    // TODO: 이동 방향 부드럽게 바라보기
                    transform.LookAt(target.position);

                    Debug.Log($"{gameObject.name}공격, {target.name}피격");
                    // TODO: 데미지

                    //PlayAttackAnimation(attackSpeed);

                    yield return new WaitForSeconds(attackSpeed);
                    // 공격 속도용 변수 추가 moveInterval 대신 넣어서 구현 가능
                    continue;
                }

                Vector2Int direction = GetStepToward(myXY, targetXY);
                Vector2Int nextXY = myXY + direction;

                if (direction == Vector2Int.zero || !TileReservation.Reserve(nextXY, gameObject))
                {
                    yield return new WaitForSeconds(moveInterval);
                    continue;
                }

                Vector3 targetPos = tilemap.GetCellCenterWorld(new Vector3Int(nextXY.x, nextXY.y, 0));
                targetPos.y = ObjYPos;
                StartCoroutine(MoveTo(targetPos));
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }


    private IEnumerator MoveTo(Vector3 targetPos)
    {
        isMoving = true;

        // TODO: 이동 방향 부드럽게 바라보기
        transform.LookAt(targetPos);

        //animator.Move(true);

        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {

            t += Time.deltaTime / moveDuration;
            transform.position = Vector3.Lerp(start, targetPos, t);


            yield return null;
        }

        //animator.Move(false);

        transform.position = targetPos;
        Vector3Int cellPos = tilemap.WorldToCell(transform.position);
        TileReservation.RemoveReserve(new Vector2Int(cellPos.x, cellPos.y));

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

    private Transform FindNearestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        Transform nearest = null;
        int minDist = int.MaxValue;
        Vector2Int myXY = GetCurrentCell();

        foreach (GameObject target in targets)
        {
            Vector2Int targetXY = GetCellOf(target.transform.position);
            int dist = HexDistance(myXY, targetXY);

            if (dist < minDist)
            {
                minDist = dist;
                nearest = target.transform;
            }
        }

        return nearest;
    }

    int HexDistance(Vector2Int a, Vector2Int b)
    {
        Vector3Int ac = OffsetToCube(a);
        Vector3Int bc = OffsetToCube(b);

        return (Mathf.Abs(ac.x - bc.x) + Mathf.Abs(ac.y - bc.y) + Mathf.Abs(ac.z - bc.z)) / 2;
    }

    Vector3Int OffsetToCube(Vector2Int offset)
    {
        int x = offset.x - (offset.y - (offset.y & 1)) / 2;
        int z = offset.y;
        int y = -x - z;

        return new Vector3Int(x, y, z);
    }

    // BFS

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

        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(from);
        cameFrom[from] = from;

        Vector2Int reached = from;

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();

            // 목표 위치에 인접하면 그 셀까지 도달한 것으로 간주
            if (HexDistance(current, to) <= 1)
            {
                reached = current;
                break;
            }

            Vector2Int[] directions = (current.y % 2 == 0) ? directionsEven : directionsOdd;

            foreach (Vector2Int dir in directions)
            {
                Vector2Int neighbor = current + dir;

                if (cameFrom.ContainsKey(neighbor))
                    continue;

                if (IsCellOccupied(neighbor)
                    || TileReservation.IsReserved(neighbor)
                    || BlockedCellManager.IsBlocked(neighbor))
                    continue;

                queue.Enqueue(neighbor);
                cameFrom[neighbor] = current;
            }
        }

        if (!cameFrom.ContainsKey(reached) || reached == from)
        {
            return Vector2Int.zero;
        }

        Vector2Int step = reached;
        while (cameFrom[step] != from)
        {
            step = cameFrom[step];
        }

        Vector2Int direction = step - from;
        return direction;
    }

    private bool IsCellOccupied(Vector2Int cellPos)
    {
        // 개선할 점: 외부에서 List로 따로 관리
        GameObject[] heros = GameObject.FindGameObjectsWithTag("Hero");
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");

        List<GameObject> allUnits = new List<GameObject>();

        allUnits.AddRange(heros);
        allUnits.AddRange(enemies);

        foreach (GameObject unit in allUnits)
        {
            Vector2Int unitPos = GetCellOf(unit.transform.position);

            if (unit != gameObject && unitPos == cellPos)
                return true;
        }

        return false;
    }

    //private void PlayAttackAnimation(float dur)
    //{
    //    attackCoroutine = StartCoroutine(AttackRoutine(dur));
    //}

    //private IEnumerator AttackRoutine(float dur)
    //{
    //    //animator.Attack();

    //    yield return new WaitForSeconds(dur);

    //    //foreach (Animator ani in animators)
    //    //    ani.SetBool("Run", false);
    //}
}
