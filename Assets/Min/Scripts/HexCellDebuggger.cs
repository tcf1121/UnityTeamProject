using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HexCellDebuggger : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;

    public Vector2Int currentCellPos;

    private void Update()
    {
        if (tilemap == null) return;

        Vector3 worldPos = transform.position;

        Vector3Int cellPos = tilemap.WorldToCell(worldPos);

        currentCellPos = new Vector2Int(cellPos.x, cellPos.y);
    }
}
