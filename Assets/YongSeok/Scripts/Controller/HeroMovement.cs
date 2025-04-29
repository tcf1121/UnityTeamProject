using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovement : MonoBehaviour
{
    [SerializeField] private float detectRange = 30f;
    [SerializeField] private LayerMask targetLayer;
    public float moveSpeed = 3f;
    public float rotateSpeed = 5f; 
    public float attackRange = 3f;

    private Transform targetTransform;
    private bool isMoving = false;
    private Queue<Vector3> movePositions = new Queue<Vector3>();

    private void Update()
    {
        if (!isMoving)
        {
            DetectAndMoveToTarget();
        }

        // 추가: 스페이스바로 테스트 경로 이동
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TestMove();
        }
    }

    private void DetectAndMoveToTarget()
    {
        Collider[] detected = Physics.OverlapSphere(transform.position, detectRange, targetLayer);

        if (detected.Length > 0)
        {
            Transform nearest = FindNearest(detected);
            if (nearest != null)
            {
                targetTransform = nearest;

                //TODO: 추후 HexGrid / A * 연동 시 아래 코드로 교체 예정
                //Vector2Int startCoord = HexGrid.Instance.GetCoordFromWorld(transform.position);
                //Vector2Int targetCoord = HexGrid.Instance.GetCoordFromWorld(targetTransform.position);
                //
                //List<HexTile> hexPath = HexAStarPathfinder.Instance.FindPath(startCoord, targetCoord);
                //if (hexPath == null || hexPath.Count == 0) return;
                //
                //List<Vector3> path = hexPath.ConvertAll(tile => tile.worldPos);
                //MoveTowardTarget(path);
            }
        }
    }

    private Transform FindNearest(Collider[] colliders)
    {
        float minDist = Mathf.Infinity;
        Transform nearest = null;

        foreach (Collider col in colliders)
        {
            float dist = Vector3.Distance(transform.position, col.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = col.transform;
            }
        }

        return nearest;
    }

    public void MoveTowardTarget(List<Vector3> fullPath)
    {
        if (isMoving || fullPath == null || fullPath.Count < 2 || targetTransform == null)
            return;

        movePositions.Clear();

        for (int i = 1; i < fullPath.Count; i++)
        {
            float distToTarget = Vector3.Distance(fullPath[i], targetTransform.position);
            if (distToTarget <= attackRange)
            {
                break;
            }

            movePositions.Enqueue(fullPath[i]);
        }

        if (movePositions.Count > 0)
            StartCoroutine(MoveCoroutine());
    }

    private IEnumerator MoveCoroutine()
    {
        isMoving = true;

        while (movePositions.Count > 0)
        {
            Vector3 targetPos = movePositions.Dequeue();

            Vector3 dir = (targetPos - transform.position).normalized;
            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Slerp(transform.rotation, rot, rotateSpeed * Time.deltaTime);
            }

            while (Vector3.Distance(transform.position, targetPos) > 0.05f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
                yield return null;
            }

            transform.position = targetPos;
        }

        isMoving = false;

        FaceTarget();
    }

    private void FaceTarget()
    {
        if (targetTransform == null) return;

        Vector3 lookPos = new Vector3(targetTransform.position.x, transform.position.y, targetTransform.position.z);
        transform.LookAt(lookPos);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }


    // 추가: 테스트 이동용 함수
    private void TestMove()
    {
        if (targetTransform == null)
        {
            Debug.LogWarning("타겟이 없습니다. targetTransform을 먼저 찾으세요.");
            return;
        }

        Debug.Log($"[테스트 시작] 타겟 위치: {targetTransform.position}");

        // ★★ 이후 교체 예정: A*로 경로 생성 (HexGrid 좌표 기반으로)
        // TODO: 추후 targetHexCoord, startHexCoord 기반으로 변경
        // Vector2Int startCoord = HexGrid.Instance.GetCoordFromWorld(transform.position);
        // Vector2Int targetCoord = HexGrid.Instance.GetCoordFromWorld(targetTransform.position);
        // List<HexTile> hexPath = HexAStarPathfinder.Instance.FindPath(startCoord, targetCoord);
        // List<Vector3> path = hexPath.ConvertAll(tile => tile.worldPos);

        // ★★ 임시 경로 생성 (1.5m 간격 3칸 이동)
        List<Vector3> testPath = new List<Vector3>();
        Vector3 start = transform.position;
        Vector3 end = targetTransform.position;

        Vector3 dir = (end - start).normalized;
        float stepDistance = 1.5f; // 헥사 타일 크기 기준 거리
        int stepCount = 3;

        for (int i = 1; i <= stepCount; i++)
        {
            Vector3 point = start + dir * (stepDistance * i);
            testPath.Add(point);
            Debug.Log($"[경로 {i}] {point}");
        }

        MoveTowardTarget(testPath);
    }

}
