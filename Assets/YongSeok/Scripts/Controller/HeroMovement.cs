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

        // �߰�: �����̽��ٷ� �׽�Ʈ ��� �̵�
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


                // ����: Detect�� �Ѵ� (��� �̵��� TestMove���� ����)
                Debug.Log($"[Ÿ�� Ž�� �Ϸ�] Ÿ�� ��ġ: {targetTransform.position}");

                // ��� Ž�� �ʿ�: AStar.FindPath(transform.position, targetTransform.position);
                // ���⼭�� ���ÿ� ���� ��� ���
                //List<Vector3> dummyPath = new List<Vector3>
                //{
                //    transform.position,
                //    targetTransform.position
                //};
                //
                //MoveTowardTarget(dummyPath);
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


    // �߰�: �׽�Ʈ �̵��� �Լ�
    private void TestMove()
    {
        if (targetTransform == null)
        {
            Debug.LogWarning("Ÿ���� �����ϴ�. targetTransform�� ���� ã������.");
            return;
        }

        Debug.Log($"[�׽�Ʈ ����] Ÿ�� ��ġ: {targetTransform.position}");

        List<Vector3> testPath = new List<Vector3>();
        Vector3 start = transform.position;
        Vector3 end = targetTransform.position;

        Vector3 dir = (end - start).normalized;
        float stepDistance = 1.5f; // 1.5m ����
        int stepCount = 3; // �� 3����

        for (int i = 1; i <= stepCount; i++)
        {
            Vector3 point = start + dir * (stepDistance * i);
            testPath.Add(point);
            Debug.Log($"[��� {i}] {point}");
        }

        MoveTowardTarget(testPath);
    }

}
