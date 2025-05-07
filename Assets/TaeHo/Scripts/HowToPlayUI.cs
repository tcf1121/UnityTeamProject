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
        // ESC ������ �� ����â�� ���� ������ �ݱ�
        if (howToPlayPanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            howToPlayPanel.SetActive(false);
        }
    }

}
