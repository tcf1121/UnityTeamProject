using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class MouseController : MonoBehaviour
{
    private Vector2 _lastTouchPos = Vector2.zero;
    private Vector2 _currentTouchPos = Vector2.zero;
    private Vector3 _prePos = Vector3.zero;

    private Vector3 _beforePosition;
    private Vector3 _offset = new Vector3(0.0f, 1.0f, 0.0f);
    public Tilemap _tileMap;
    [SerializeField] private GameObject _movableObj;

    private const string _moveableTAG = "Hero";


    private void Update()
    {
#if UNITY_EDITOR
        _lastTouchPos = _currentTouchPos;
        _currentTouchPos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            TouchBeganEvent();
            _prePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition != _prePos) { TouchMovedEvent(); }
            else { TouchStayEvent(); }
        }

        if (Input.GetMouseButtonUp(0))
        {
            TouchEndedEvent();
        }
#else
            // Multi-touch 방지 
            if (Input.touchCount <= 0) { return; }

            Touch touchInfo = Input.GetTouch(0);
            _lastTouchPos = _currentTouchPos;
            _currentTouchPos = touchInfo.position;

            switch (touchInfo.phase)
            {
                case TouchPhase.Began:
                    TouchBeganEvent();
                    break;

                case TouchPhase.Moved:
                    TouchMovedEvent();
                    break;

                case TouchPhase.Stationary:
                    TouchStayEvent();
                    break;

                case TouchPhase.Ended:
                    TouchEndedEvent();
                    break;
            }
#endif

#if UNITY_EDITOR
        // 캐릭터 Ray 확인용 
        if (_movableObj != null)
        {
            Ray ray = new Ray(_movableObj.transform.position, Camera.main.transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
        }


        // TEST Code
        Vector3 touchPos = new Vector3(0, 0, 0);

#if UNITY_EDITOR
        touchPos = Input.mousePosition;
#else
            touchPos = Input.GetTouch(0).position;
#endif

        Ray test_ray = Camera.main.ScreenPointToRay(touchPos);

        Debug.DrawRay(test_ray.origin, test_ray.direction * 1000, Color.blue);

#endif
    }

    private void TouchBeganEvent()
    {
        _movableObj = OnClickObjUsingTag(_moveableTAG);

        if (_movableObj != null)
        {
            _beforePosition = _movableObj.transform.position;
        }
    }

    private void TouchMovedEvent()
    {
//        if (_movableObj != null)
//        {
//            Vector3 touchPos = Vector3.zero;
//#if UNITY_EDITOR
//            touchPos = Input.mousePosition;
//#else
//                touchPos = Input.GetTouch(0).position;
//#endif
//            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touchPos.x, touchPos.y, 10));
//            Vector3 testPos = new Vector3(worldPos.x, 2, _movableObj.transform.position.z);
//            _movableObj.transform.position = testPos;
//            //_movableObj.transform.position = Vector3.MoveTowards(_movableObj.transform.position, worldPos, Time.deltaTime * 20f);
            
//        }

        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        if (_movableObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);



            if (hitInfo.collider != null)
            {
                Debug.Log("hit info : " + hitInfo.collider.gameObject.name);

                _movableObj.transform.position = new Vector3(hitInfo.collider.gameObject.transform.position.x,
                    2, hitInfo.collider.gameObject.transform.position.z);
                
                //_movableObj.transform.position += _offset;
            }
            else
            {
                _movableObj.transform.position = _beforePosition;
            }
        }
    }

    private void TouchStayEvent()
    {

    }

    private void TouchEndedEvent()
    {
        int layerMask = 1 << LayerMask.NameToLayer("Ground");
        if (_movableObj != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask);

            if (hitInfo.collider != null)
            {   if(_tileMap.WorldToCell(hitInfo.transform.position).y < 8)
                {
                    _movableObj.transform.position = hitInfo.collider.gameObject.transform.position;
                    _movableObj.transform.position += _offset;
                    _movableObj.GetComponent<Unit>().startPoint = _tileMap.WorldToCell(hitInfo.transform.position);
                }
                else
                {
                    _movableObj.transform.position = _beforePosition;
                }
                Debug.Log("hit info : " + hitInfo.collider.gameObject.name);
            }
            else
            {
                _movableObj.transform.position = _beforePosition;
            }
        }

        //_movableObj = null;
    }

    private GameObject OnClickObjUsingTag(string tag)
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
                if (hitObject.gameObject.tag.Equals(tag))
                {
                    return hitObject;
                }
            }
        }

        return null;
    }
}
