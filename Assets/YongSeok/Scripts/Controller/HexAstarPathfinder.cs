using System.Collections.Generic;
using UnityEngine;

public class HexAStarPathfinder : MonoBehaviour
{
    public static HexAStarPathfinder Instance;

    private void Awake()
    {
        Instance = this;
    }

    public List<HexTile> FindPath(Vector2Int startCoord, Vector2Int targetCoord)
    {
        HexTile startTile = HexGrid.Instance.GetTileAt(startCoord);
        HexTile targetTile = HexGrid.Instance.GetTileAt(targetCoord);

        if (startTile == null || targetTile == null)
            return null;

        List<HexTile> openSet = new List<HexTile>();
        HashSet<HexTile> closedSet = new HashSet<HexTile>();

        openSet.Add(startTile);

        while (openSet.Count > 0)
        {
            HexTile current = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < current.fCost ||
                    (openSet[i].fCost == current.fCost && openSet[i].hCost < current.hCost))
                {
                    current = openSet[i];
                }
            }

            openSet.Remove(current);
            closedSet.Add(current);

            if (current == targetTile)
            {
                return RetracePath(startTile, targetTile);
            }

            foreach (HexTile neighbor in HexGrid.Instance.GetNeighbors(current))
            {
                if (!neighbor.walkable || closedSet.Contains(neighbor))
                    continue;

                int newGCost = current.gCost + GetHexDistance(current, neighbor);
                if (newGCost < neighbor.gCost || !openSet.Contains(neighbor))
                {
                    neighbor.gCost = newGCost;
                    neighbor.hCost = GetHexDistance(neighbor, targetTile);
                    neighbor.parent = current;

                    if (!openSet.Contains(neighbor))
                        openSet.Add(neighbor);
                }
            }
        }

        return null;
    }

    private List<HexTile> RetracePath(HexTile start, HexTile end)
    {
        List<HexTile> path = new List<HexTile>();
        HexTile current = end;

        while (current != start)
        {
            path.Add(current);
            current = current.parent;
        }

        path.Reverse();
        return path;
    }

    private int GetHexDistance(HexTile a, HexTile b)
    {
        int dx = a.hexCoord.x - b.hexCoord.x;
        int dy = a.hexCoord.y - b.hexCoord.y;
        return Mathf.Max(Mathf.Abs(dx), Mathf.Abs(dy), Mathf.Abs(dx + dy));
    }
}
