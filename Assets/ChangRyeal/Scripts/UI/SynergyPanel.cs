using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SynergyPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] public GameObject ExplanePanel;
    [SerializeField] public Text ExplaneText;
    [SerializeField] public string synergyName;
    [SerializeField] public string synergyEffect;
    [SerializeField] public string effectNum;


    public void OnPointerEnter(PointerEventData eventData)
    {
        ExplanePanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ExplanePanel.SetActive(false);
    }

    public void SetExplane()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($" ({synergyName}) ");
        sb.Append($"{synergyEffect} +{effectNum} ");
        ExplaneText.text = sb.ToString();
    }


}
