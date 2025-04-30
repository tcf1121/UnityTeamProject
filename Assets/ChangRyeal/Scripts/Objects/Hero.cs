using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string heroname;
    [SerializeField] public int cost;
    [SerializeField] public int star = 1;
    [SerializeField] public bool battle = false;
    [SerializeField] public GameObject heroObject;
    [SerializeField] private Unit unit;

    public void SetBattle()
    {
        if (unit.startPoint.y == 3)
        {
            gameObject.tag = "Storage";
            battle = false;
        }

        else
        {
            gameObject.tag = "Hero";
            battle = true;
        }
            
    }

}
