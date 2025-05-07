using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayUI : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
    }

    public void Update()
    {
        // ESC 눌렀을 때 설명창이 켜져 있으면 닫기
        if (howToPlayPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayPanel.SetActive(false);
        }
    }

}
