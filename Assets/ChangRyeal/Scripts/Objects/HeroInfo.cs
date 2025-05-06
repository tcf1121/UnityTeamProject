using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YongSeok;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] public GameObject hero;

    [Header("UI")]
    [SerializeField] private Image heroImage;
    [SerializeField] private Button sellBtn;
    public void SetInfo()
    {
        heroImage.sprite = hero.GetComponent<Hero>().sprite;
        
    }

    private void OnEnable()
    {
        sellBtn.onClick.AddListener(SellHero);
    }

    private void OnDisable()
    {
        sellBtn.onClick.RemoveListener(SellHero);
    }
    private void SellHero()
    {
        GameManager.Instance.player.SellHero(hero);
        gameObject.SetActive(false);
    }
}
