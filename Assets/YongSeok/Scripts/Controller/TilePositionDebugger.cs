using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilePositionDebugger : MonoBehaviour
{
    [Header("필수 연결")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Transform tileRoot;

    [Header("마커 프리팹 (선택)")]
    [SerializeField] private GameObject markerPrefab;

    [Header("직접 지정한 셀 좌표들")]
    public List<Vector3Int> manuallySelectedTiles = new();

    [Header("중심 기반 자동 선택 (선택)")]
    public bool useRadiusSelection = false;
    public Vector3Int center = new Vector3Int(4, 6, 0);
    public int radius = 1;

    public List<Vector3Int> foundTilePositions = new();

    private void Start()
    {
        if (tilemap == null || tileRoot == null)
        {
            Debug.LogError("tilemap 또는 tileRoot가 연결되지 않았습니다.");
            return;
        }

        ScanTilesFromPrefabs();
        PrintAllTilePositions();

        if (manuallySelectedTiles != null && manuallySelectedTiles.Count > 0)
        {
            MarkSpecificTiles(manuallySelectedTiles);
        }

        if (useRadiusSelection)
        {
            var filtered = foundTilePositions
                .Where(p => Mathf.Abs(p.x - center.x) <= radius && Mathf.Abs(p.y - center.y) <= radius)
                .ToList();
            MarkSpecificTiles(filtered);
        }
    }

    public void ScanTilesFromPrefabs()
    {
        foundTilePositions.Clear();

        foreach (Transform child in tileRoot)
        {
            Vector3 worldPos = child.position;
            Vector3Int cell = tilemap.WorldToCell(worldPos);
            foundTilePositions.Add(cell);
        }

        Debug.Log($"총 탐색된 프리팹 타일 수: {foundTilePositions.Count}");
    }

    public void PrintAllTilePositions()
    {
        foreach (var pos in foundTilePositions)
        {
            Debug.Log($"타일 좌표: {pos}");
        }
    }

    public void MarkSpecificTiles(List<Vector3Int> cells)
    {
        if (markerPrefab == null) return;

        foreach (var cell in cells)
        {
            Vector3 worldPos = tilemap.GetCellCenterWorld(cell);
            Instantiate(markerPrefab, worldPos, Quaternion.identity);
        }
    }
}
