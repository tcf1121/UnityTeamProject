using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public static HexGrid Instance;

    private Dictionary<Vector2Int, HexTile> hexTiles = new Dictionary<Vector2Int, HexTile>();

    private void Awake()
    {
        Instance = this;
        // 초기 타일 등록은 수동 또는 패키지에서 자동으로 채워 넣기
    }

    public void RegisterTile(Vector2Int coord, HexTile tile)
    {
        if (!hexTiles.ContainsKey(coord))
            hexTiles.Add(coord, tile);
    }

    public HexTile GetTileAt(Vector2Int coord)
    {
        hexTiles.TryGetValue(coord, out var tile);
        return tile;
    }

    public List<HexTile> GetNeighbors(HexTile tile)
    {
        List<HexTile> neighbors = new List<HexTile>();
        Vector2Int[] directions = new Vector2Int[]
        {
            new Vector2Int(1, 0), new Vector2Int(0, 1), new Vector2Int(-1, 1),
            new Vector2Int(-1, 0), new Vector2Int(0, -1), new Vector2Int(1, -1)
        };

        foreach (var dir in directions)
        {
            var neighborCoord = tile.hexCoord + dir;
            var neighbor = GetTileAt(neighborCoord);
            if (neighbor != null)
                neighbors.Add(neighbor);
        }

        return neighbors;
    }
}
