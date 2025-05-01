using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BlockedCellManager
{
    private static HashSet<Vector2Int> blockedCells = new HashSet<Vector2Int>();

    static BlockedCellManager()
    {
        for (int x = -9; x <= 1; x++)
            blockedCells.Add(new Vector2Int(x, 3));

        for (int x = -9; x <= 2; x++)
            blockedCells.Add(new Vector2Int(x, 12));

        for (int y = 4; y <= 11; y++)
            blockedCells.Add(new Vector2Int(1, y));

        for (int y = 4; y <= 11; y++)
            blockedCells.Add(new Vector2Int(-9, y));
    }

    public static bool IsBlocked(Vector2Int cellPos)
    {
        return blockedCells.Contains(cellPos);
    }
}
