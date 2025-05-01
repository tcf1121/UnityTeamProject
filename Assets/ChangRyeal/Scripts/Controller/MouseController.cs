using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    private Vector2 lastTouchPos = Vector2.zero;
    private Vector2 currentTouchPos = Vector2.zero;
    private Vector3 prePos = Vector3.zero;

    private Vector3 beforePosition;
    private Vector3 offSet = new Vector3(0.0f, 0.0f, 0.0f);
    [SerializeField] private TileMapManager tile;
    [SerializeField] private PlayerHero playerHero;
    [SerializeField] private GameObject moveAbleObject;

    private const string moveAbleTag1 = "Hero";
    private const string moveAbleTag2 = "Storage";

    
    [Header("UI")]
    [SerializeField] private GameObject heroInfo;
   

    private void Update()
    {
        lastTouchPos = currentTouchPos;
        currentTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            TouchBeganEvent();
            prePos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition != prePos) { TouchMovedEvent(); }
            else { TouchStayEvent(); }
        }
        if (Input.GetMouseButtonUp(0))
        {
            TouchEndedEvent();
        }

        if (Input.GetMouseButtonDown(1))
        {
            HeroInfoEvent();
        }
    }

    // 클릭 시작 이벤트
    private void TouchBeganEvent()
    {
        moveAbleObject = OnClickObjUsingTag(moveAbleTag1, moveAbleTag2);
        if (moveAbleObject != null)
        {
            beforePosition = moveAbleObject.transform.position;

        }
    }

    private void HeroInfoEvent()
    {
        moveAbleObject = OnClickObjUsingTag(moveAbleTag1, moveAbleTag2);

        if(moveAbleObject != null)
        {
            if (heroInfo.activeSelf == false)
                heroInfo.SetActive(true);
            heroInfo.GetComponent<HeroInfo>().hero = moveAbleObject;
            heroInfo.GetComponent<HeroInfo>().SetInfo();
        }
        else
        {
            if (heroInfo.activeSelf == true)
                heroInfo.SetActive(false);
        }
    }

    // 드래그 이벤트
    private void TouchMovedEvent()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        if (moveAbleObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);



            if (hitInfo.collider != null)
            {
                moveAbleObject.transform.position = new Vector3(hitInfo.collider.gameObject.transform.position.x,
                    2, hitInfo.collider.gameObject.transform.position.z);
            }
            else
            {
                moveAbleObject.transform.position = beforePosition;
            }
        }
    }

    private void TouchStayEvent()
    {

    }

    private void TouchEndedEvent()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        if (moveAbleObject != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

            if (hitInfo.collider != null)
            {   if((tile.tileMap.WorldToCell(hitInfo.transform.position).y < 8 &&
                    tile.tileMap.WorldToCell(hitInfo.transform.position).y > 2) &&
                    (tile.tileMap.WorldToCell(hitInfo.transform.position).x < 1 &&
                    tile.tileMap.WorldToCell(hitInfo.transform.position).x > -9))
                {
                    if (playerHero.CanMove(tile.tileMap.WorldToCell(hitInfo.transform.position)))
                    {
                        moveAbleObject.transform.position = hitInfo.collider.gameObject.transform.position;
                        moveAbleObject.transform.position += offSet;
                        playerHero.MoveHero(moveAbleObject.GetComponent<Unit>().startPoint,
                            tile.tileMap.WorldToCell(hitInfo.transform.position),
                            moveAbleObject.GetComponent<Hero>());
                        moveAbleObject.GetComponent<Unit>().startPoint = tile.tileMap.WorldToCell(hitInfo.transform.position);
                        moveAbleObject.GetComponent<Hero>().SetBattle();
                    }
                    else
                    {
                        Debug.Log("다른 사람 차 있음");
                        moveAbleObject.transform.position = beforePosition;
                    }
                }
                else
                {
                    moveAbleObject.transform.position = beforePosition;
                }
            }
            else
            {
                moveAbleObject.transform.position = beforePosition;
            }
        }

        //_movableObj = null;
    }

    private GameObject OnClickObjUsingTag(string tag1, string tag2)
    {
        Vector3 touchPos = new Vector3(0, 0, 0);

#if UNITY_EDITOR
        touchPos = Input.mousePosition;
#else
            touchPos = Input.GetTouch(0).position;
#endif

        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hitInfo;
        Physics.Raycast(ray, out hitInfo);

        if (hitInfo.collider != null)
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject != null)
            {
                if (hitObject.gameObject.tag.Equals(tag1))
                {
                    return hitObject;
                }
                else if (hitObject.gameObject.tag.Equals(tag2))
                {
                    return hitObject;
                }
            }
        }

        return null;
    }
}
