using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YongSeok;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] public GameObject hero;
    [SerializeField] HeroStatus_ heroStatus;

    [Header("UI")]
    [SerializeField] private Image heroImage;
    [SerializeField] private Button sellBtn;
    [SerializeField] private TMP_Text statsText;


    private void Awake()
    {
        if (statsText == null)
            statsText = transform.Find("Text (TMP)").GetComponent<TMP_Text>();
    }

    public void SetInfo()
    {
        heroImage.sprite = hero.GetComponent<Hero>().sprite;
        heroStatus = hero.GetComponent<HeroStatus_>();
        statsText.text = heroStatus.GetStatus();
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
