using EPOOutline;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

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
    private GraphicRaycaster g_raycater;
    private HeroAnimator heroAnimator;
    private Outlinable heroOutline;
    [Header("UI")]
    [SerializeField] private GameObject heroInfo;
    [SerializeField] private GameObject sellobject;
    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        playerHero = GameManager.Instance.player.playerHero;
    }
    private void Update()
    {
        
        lastTouchPos = currentTouchPos;
        currentTouchPos = Input.mousePosition;
        if (!GameManager.Instance.player.Battling)
        {
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
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                HeroInfoEvent();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            HeroInfoEvent();
        }
    }

    // Ŭ�� ���� �̺�Ʈ
    private void TouchBeganEvent()
    {
        moveAbleObject = OnClickObjUsingTag(moveAbleTag1, moveAbleTag2);
        
        if (moveAbleObject != null)
        {
            ChangeLayer(moveAbleObject.transform, "OverUI");
            sellobject.SetActive(true);
            heroOutline = moveAbleObject.GetComponent<Outlinable>();
            heroOutline.enabled = true;
            heroAnimator = moveAbleObject.GetComponent<HeroAnimator>();
            heroAnimator.Wait(false);
            heroAnimator.Pick(true);
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

    // �巡�� �̺�Ʈ
    private void TouchMovedEvent()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Plane");
        int GridMask = 2 << LayerMask.NameToLayer("Ground");
        
        if (moveAbleObject != null)
        {
            heroOutline.enabled = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);
            moveAbleObject.transform.position = new Vector3(hitInfo.point.x, 1, hitInfo.point.z);

        }
    }

    private void TouchStayEvent()
    {

    }

    private void TouchEndedEvent()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        int sellLayer = 1 << LayerMask.NameToLayer("Sell");
        
        if (moveAbleObject != null)
        {
            ChangeLayer(moveAbleObject.transform, "Default");
            sellobject.SetActive(false);
            heroOutline.enabled = false;
            heroAnimator.Pick(false);
            heroAnimator.Wait(true);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

            RaycastHit hitsell;
            Physics.Raycast(ray, out hitsell, Mathf.Infinity, sellLayer);


            // �Ǹ� ĭ�� �巡�� ���� ���
            if (hitsell.collider != null)
            {
                GameManager.Instance.player.SellHero(moveAbleObject);
            }
            else
            {
                // ���� �巡�� ���� ���
                if (hitInfo.collider != null)
                {
                    if ((tile.tileMap.WorldToCell(hitInfo.transform.position).y < 8 &&
                        tile.tileMap.WorldToCell(hitInfo.transform.position).y > 2) &&
                        (tile.tileMap.WorldToCell(hitInfo.transform.position).x < 1 &&
                        tile.tileMap.WorldToCell(hitInfo.transform.position).x > -9))
                    {
                        // ���� ĭ�� �巡�� �� ���
                        if(tile.tileMap.WorldToCell(hitInfo.transform.position) == moveAbleObject.GetComponent<Unit>().startPoint)
                            moveAbleObject.transform.position = beforePosition;
                        // �ش� ĭ�� ������ ���� ���

                        if (playerHero.CanMove(tile.tileMap.WorldToCell(hitInfo.transform.position)))
                        {
                            // �ش� ĭ�� �д�.
                            playerHero.MoveHero(moveAbleObject.GetComponent<Unit>().startPoint,
                                tile.tileMap.WorldToCell(hitInfo.transform.position),
                                moveAbleObject.GetComponent<Hero>());
                            moveAbleObject.transform.position = hitInfo.collider.gameObject.transform.position;
                            moveAbleObject.transform.position += offSet;
                            moveAbleObject.GetComponent<Unit>().startPoint = tile.tileMap.WorldToCell(hitInfo.transform.position);
                            moveAbleObject.GetComponent<Hero>().SetBattle();
                        }
                        // �ش� ĭ�� ������ ���� ���
                        else
                        {
                            // �ش� ĭ�� ������ �ڸ��� �� �ٲ۴�.
                            playerHero.ChangeHero(moveAbleObject.GetComponent<Hero>(), moveAbleObject.GetComponent<Unit>().startPoint,
                                tile.tileMap.WorldToCell(hitInfo.transform.position), beforePosition);
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

        }

        //_movableObj = null;
    }

    private GameObject OnClickObjUsingTag(string tag1, string tag2)
    {
        Vector3 touchPos = new Vector3(0, 0, 0);
        touchPos = Input.mousePosition;

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

    public void ChangeLayer(Transform trans, string name)
    {
        ChangeLayersRecursively(trans, name);
    }

    public void ChangeLayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChangeLayersRecursively(child, name);
        }
    }
}
