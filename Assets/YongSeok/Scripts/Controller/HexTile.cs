using UnityEngine;

public class HexTile
{
    public Vector2Int hexCoord; // (x, y) ���� ��ǥ
    public bool walkable; // ������ �� �ִ���
    public Vector3 worldPos; // ���� ������� ��ġ

    // A*��
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
