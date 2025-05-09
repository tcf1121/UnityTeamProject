using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class UI_ObjBar : MonoBehaviour
{
    public GameObject prfObjBar;
    public GameObject canvas;

    public RectTransform objBar;

    public Slider hpBar;
    public Slider MpBar;

    public void SetUI()
    {
        canvas = GameObject.Find("UICanvas");
        objBar = Instantiate(prfObjBar, canvas.transform).GetComponent<RectTransform>();
        hpBar = objBar.Find("HpBar").GetComponent<Slider>();
        MpBar = objBar.Find("MpBar").GetComponent<Slider>();
    }

    private void Update()
    {
        objBar.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 2f, 0));
        //카메라와 같은 방향을 보도록 수정
        objBar.transform.rotation = Camera.main.transform.rotation;
    }
}
