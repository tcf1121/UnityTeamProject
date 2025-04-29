using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class TileMap : MonoBehaviour
{
    public Camera _mainCamera;
    public Tilemap _tileMap;
    public Grid grid;
    [SerializeField] public Dictionary<Vector3Int, Unit> objectOnTile = new Dictionary<Vector3Int, Unit>(); // 오브젝트가 타일 위에 있는가?에 대한 정보
    
    private void Start()
    {
        for(int x = -8; x <= 0; x++)
        {
            for (int y = 4; y <= 11; y++)
            {
                objectOnTile.Add(new Vector3Int(x, y, 0), null);
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            int layerMask = 1 << LayerMask.NameToLayer("Ground");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

            if (hitInfo.collider != null)
            {
                Debug.Log(_tileMap.WorldToCell(hitInfo.transform.position));
                if((_tileMap.WorldToCell(hitInfo.transform.position).x >=-8 &&
                    _tileMap.WorldToCell(hitInfo.transform.position).x <= 0) &&
                    (_tileMap.WorldToCell(hitInfo.transform.position).y >= 4 &&
                    _tileMap.WorldToCell(hitInfo.transform.position).y <= 11))
                Debug.Log(objectOnTile[_tileMap.WorldToCell(hitInfo.transform.position)]);
            }
        }
    }
}
