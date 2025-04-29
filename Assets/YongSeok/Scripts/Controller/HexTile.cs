using UnityEngine;

public class HexTile
{
    public Vector2Int hexCoord; // (x, y) 정수 좌표
    public bool walkable; // 지나갈 수 있는지
    public Vector3 worldPos; // 실제 월드상의 위치

    // A*용
    public int gCost;
    public int hCost;
    public int fCost => gCost + hCost;
    public HexTile parent;

    public HexTile(Vector2Int hexCoord, bool walkable, Vector3 worldPos)
    {
        this.hexCoord = hexCoord;
        this.walkable = walkable;
        this.worldPos = worldPos;
    }
}
