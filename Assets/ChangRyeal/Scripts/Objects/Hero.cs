using EPOOutline.Demo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    [SerializeField] public Sprite sprite;
    [SerializeField] public string heroname;
    [SerializeField] public int cost;
    [SerializeField] public int star;
    [SerializeField] public bool battle = false;
    [SerializeField] public GameObject heroObject;
    [SerializeField] private Unit unit;

    [Header("Propertis")]
    [SerializeField] GameObject TwoStarEffet;
    [SerializeField] GameObject ThreeStarEffet;

    private void OnEnable()
    {


        if (star == 2)
            TwoStarEffet.SetActive(true);
        else if (star == 3)
            ThreeStarEffet.SetActive(true);
        
    }

    public void SetHero()
    {
        name = heroname;
        foreach (var ani in GetComponent<HeroAnimator>().animators)
        {
            ani.enabled = true;
        }
        GetComponent<InteractableObject>().enabled = true;
        GetComponent<TraceS>().enabled = true;
        GetComponent<AttackBase_s>().enabled = true;
        GetComponent<UI_ObjBar>().enabled = true;
        GetComponent<UI_ObjBar>().SetUI();
    }
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
