using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayUI : MonoBehaviour
{
    public GameObject howToPlayPanel;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HowToPlay();
        }
    }

    public void HowToPlay()
    {
        howToPlayPanel.SetActive(!howToPlayPanel.activeSelf);
    }

}
