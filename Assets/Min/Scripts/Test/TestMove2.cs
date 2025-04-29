using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TestMove2 : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    private Vector2Int currentXZPos;


    private void Start()
    {
        if (tilemap == null)
            tilemap = FindObjectOfType<Tilemap>();

        Vector3Int cell = tilemap.WorldToCell(transform.position);
        currentXZPos = new Vector2Int(cell.x, cell.z);

        transform.position = tilemap.GetCellCenterWorld(new Vector3Int(currentXZPos.x, 0, currentXZPos.y));
    }

    private void Update()
    {
        foreach (KeyCode key in new[]
        {
        KeyCode.Keypad7, KeyCode.Keypad9,
        KeyCode.Keypad1, KeyCode.Keypad3,
        KeyCode.Keypad4, KeyCode.Keypad6
        })

        {
            if (Input.GetKeyDown(key))
            {
                Vector3Int cell = tilemap.WorldToCell(transform.position);
                Vector2Int currentXY = new Vector2Int(cell.x, cell.y);

                Vector2Int direction = GetDirectionBasedOnY(key, currentXY.y);

                Vector2Int nextXY = currentXY + direction;

                transform.position = tilemap.GetCellCenterWorld(new Vector3Int(nextXY.x, nextXY.y, 0));
                break;
            }
        }
    }

    private Vector2Int GetDirectionBasedOnY(KeyCode key, int currentY)
    {
        bool isEvenRow = (currentY % 2 == 0);

        if (isEvenRow)
        {
            if (key == KeyCode.Keypad6) return new Vector2Int(1, 0);
            if (key == KeyCode.Keypad4) return new Vector2Int(-1, 0);
            if (key == KeyCode.Keypad9) return new Vector2Int(0, 1);
            if (key == KeyCode.Keypad7) return new Vector2Int(-1, 1);
            if (key == KeyCode.Keypad3) return new Vector2Int(0, -1);
            if (key == KeyCode.Keypad1) return new Vector2Int(-1, -1);
        }
        else
        {
            if (key == KeyCode.Keypad6) return new Vector2Int(1, 0);
            if (key == KeyCode.Keypad4) return new Vector2Int(-1, 0);
            if (key == KeyCode.Keypad9) return new Vector2Int(1, 1);
            if (key == KeyCode.Keypad7) return new Vector2Int(0, 1);
            if (key == KeyCode.Keypad3) return new Vector2Int(1, -1);
            if (key == KeyCode.Keypad1) return new Vector2Int(0, -1);
        }

        return Vector2Int.zero;
    }
}
