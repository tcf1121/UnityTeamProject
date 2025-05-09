using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TraceS : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] public string targetTag;

    [SerializeField] private float moveInterval = 1.0f; // ?âÎèô Í≤∞Ï†ï ?úÍ∞Ñ
    [SerializeField] private float moveDuration = 1.0f; // ?¥Îèô ?úÍ∞Ñ
    [SerializeField] private int attackRange = 3; // Í∞êÏ? Î≤îÏúÑ, Í≥µÍ≤© ?¨Í±∞Î¶¨Îûë ?§Î¶Ñ
    [SerializeField] private float attackSpeed = 1.0f;

    [SerializeField] private float ObjYPos = 0f;

    [SerializeField] public ObjectAnimator animator;

    private bool isbattle = false;
    private bool isMoving = false;
    private Coroutine unitCoroutine;
    private Coroutine attackCoroutine;
    private Coroutine moveCoroutine;
    private Coroutine targetCoroutine;
    private Coroutine stopCoroutine;
    private AttackBase_s attackBase;
    private Transform target;
    public Transform Target { get { return target; } set {target = value; } }

    private Vector2Int targetXY;
    private Vector2Int nextXY;

    public void SetAttck()
    {
        attackBase = GetComponent<AttackBase_s>();
        attackBase.SetDamage();
        if (gameObject.GetComponent<HeroStatus_>() != null)
        {
            attackRange = gameObject.GetComponent<HeroStatus_>().b_Status.range;
            attackSpeed = 1f / gameObject.GetComponent<HeroStatus_>().b_Status.attackSpeed;
        }
        else
        {
            attackRange = gameObject.GetComponent<MonsterStatus>().battleStatus.range;
            attackSpeed = 1f / gameObject.GetComponent<MonsterStatus>().battleStatus.attackSpeed;
        }
    }

    public void Battling()
    {
        if (tilemap == null)
            tilemap = FindObjectOfType<Tilemap>();
        //// TODO: ?¥Î≤§??Íµ¨ÎèÖ ???ÑÎûò ÏΩîÎìú 2Ï§???†ú
        //if (gameObject.CompareTag("Monster") || gameObject.CompareTag("Hero"))
        //    unitCoroutine = StartCoroutine(UnitRoutine());

        isbattle = true;
    }

    // Íµ¨ÎèÖ???®Ïàò
    public void EndBattling()
    {
        if(unitCoroutine != null)
        {
            StopCoroutine(unitCoroutine);
            unitCoroutine = null;
        }
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
        isbattle = false;
    }

    public void Update()
    {
        if (isbattle)
        {
            if (target == null || target.gameObject.activeSelf == false)
                target = FindNearestTarget();

            Vector2Int myXY = GetCurrentCell();
            if (targetCoroutine == null)
                targetCoroutine = StartCoroutine(TargetXYCor());
            int dist = HexDistance(myXY, targetXY);

            if (attackCoroutine == null && moveCoroutine == null)
            {
                // ∞¯∞› ªÁ∞≈∏Æ ¿Ã≥ªø° ¿˚¿Ã ¿÷¿ª ∂ß
                if (dist <= attackRange)
                {
                    attackCoroutine = StartCoroutine(AttackCor());
                }
                // æ¯¿∏∏È ¿Ãµø
                else
                {
                    Vector2Int direction = GetStepToward(myXY, targetXY);
                    nextXY = myXY + direction;
                    Vector3 targetPos = targetPos = tilemap.GetCellCenterWorld(new Vector3Int(nextXY.x, nextXY.y, 0));
                    if(direction == Vector2Int.zero || !TileReservation.Reserve(nextXY, gameObject))
                        stopCoroutine = StartCoroutine(StopCor());
                    else
                        moveCoroutine = StartCoroutine(MoveTo(targetPos));
                }
            }
        }
    }

    private IEnumerator UnitRoutine()
    {
        target = FindNearestTarget(); // ?ÑÎ∞ú ?§ÌÇ¨ ?ÑÌï¥ ?ÑÎìúÎ°??¨Î¶º

        while (true)
        {
            if (!isMoving)
            {
                if (target == null || target.gameObject.activeSelf == false)
                {
                    target = FindNearestTarget();
                    yield return new WaitForSeconds(moveInterval);
                    continue;
                }

                Vector2Int myXY = GetCurrentCell();
                Vector2Int targetXY = GetCellOf(target.position);
                int dist = HexDistance(myXY, targetXY);

                if (attackCoroutine == null && moveCoroutine == null)
                {
                    // ∞¯∞› ªÁ∞≈∏Æ ¿Ã≥ªø° ¿˚¿Ã ¿÷¿ª ∂ß
                    if (dist <= attackRange)
                    {
                        attackCoroutine = StartCoroutine(AttackCor());
                    }
                    // æ¯¿∏∏È ¿Ãµø
                    else
                    {
                        Vector2Int direction = GetStepToward(myXY, targetXY);
                        nextXY = myXY + direction;
                        Vector3 targetPos = targetPos = tilemap.GetCellCenterWorld(new Vector3Int(nextXY.x, nextXY.y, 0));

                        moveCoroutine = StartCoroutine(MoveTo(targetPos));
                    }
                }
            }

            yield return new WaitForSeconds(moveInterval);
        }
    }

    private IEnumerator StopCor()
    {
        yield return new WaitForSeconds(moveInterval);
        if (stopCoroutine != null)
        {
            StopCoroutine(stopCoroutine);
            stopCoroutine = null;
        }
    }
    private IEnumerator AttackCor()
    {
        transform.LookAt(target.position);
        attackBase.TryAttack();
        yield return new WaitForSeconds(attackSpeed);
        if (attackCoroutine != null)
        {
            StopCoroutine(attackCoroutine);
            attackCoroutine = null;
        }
            
    }
    private IEnumerator MoveTo(Vector3 targetPos)
    {
        isMoving = true;
        animator.Move(true);
        // TODO: ?¥Îèô Î∞©Ìñ• Î∂Ä?úÎüΩÍ≤?Î∞îÎùºÎ≥¥Í∏∞
        transform.LookAt(targetPos);
        Vector3 start = transform.position;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration;
            transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }
        transform.position = targetPos;
        Vector3Int cellPos = tilemap.WorldToCell(transform.position);
        GameManager.Instance.player.playerHero.battleManager.GetComponent<BattleManager_>()
            .Move(gameObject, tilemap.WorldToCell(start), cellPos);
        TileReservation.RemoveReserve(new Vector2Int(cellPos.x, cellPos.y));
            
        
        isMoving = false;
        animator.Move(false);
        yield return new WaitForSeconds(moveInterval);
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            moveCoroutine = null;
        }
            
    }

    private IEnumerator TargetXYCor()
    {
        targetXY = GetCellOf(target.position);
        yield return new WaitForSeconds(moveInterval);
        if (targetCoroutine != null)
        {
            StopCoroutine(targetCoroutine);
            targetCoroutine = null;
        }
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

            // Î™©Ìëú ?ÑÏπò???∏Ï†ë?òÎ©¥ Í∑??ÄÍπåÏ? ?ÑÎã¨??Í≤ÉÏúºÎ°?Í∞ÑÏ£º
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
        // Í∞úÏÑ†???? ?∏Î??êÏÑú ListÎ°??∞Î°ú Í¥ÄÎ¶?
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
}
