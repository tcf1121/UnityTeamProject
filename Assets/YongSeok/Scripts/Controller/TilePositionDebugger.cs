using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilePositionDebugger : MonoBehaviour
{
    [Header("�ʼ� ����")]
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Transform tileRoot;

    [Header("��Ŀ ������ (����)")]
    [SerializeField] private GameObject markerPrefab;

    [Header("���� ������ �� ��ǥ��")]
    public List<Vector3Int> manuallySelectedTiles = new();

    [Header("�߽� ��� �ڵ� ���� (����)")]
    public bool useRadiusSelection = false;
    public Vector3Int center = new Vector3Int(4, 6, 0);
    public int radius = 1;

    public List<Vector3Int> foundTilePositions = new();

    private void Start()
    {
        if (tilemap == null || tileRoot == null)
        {
            Debug.LogError("tilemap �Ǵ� tileRoot�� ������� �ʾҽ��ϴ�.");
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

        Debug.Log($"�� Ž���� ������ Ÿ�� ��: {foundTilePositions.Count}");
    }

    public void PrintAllTilePositions()
    {
        foreach (var pos in foundTilePositions)
        {
            Debug.Log($"Ÿ�� ��ǥ: {pos}");
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
