using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using YongSeok;

public class HeroInfo : MonoBehaviour
{
    [SerializeField] public GameObject hero;
    [SerializeField] HeroStatus_ heroStatus;

    [Header("UI")]
    [SerializeField] private Image heroImage;
    [SerializeField] private TMP_Text heroName;
    [SerializeField] private TMP_Text maxHp;
    [SerializeField] private TMP_Text attack;
    [SerializeField] private TMP_Text defense;
    [SerializeField] private TMP_Text magicResist;
    [SerializeField] private TMP_Text range;
    [SerializeField] private TMP_Text attackSpeed;
    [SerializeField] private TMP_Text maxMp;
    [SerializeField] private TMP_Text addMana;
    [SerializeField] private TMP_Text critical;
    [SerializeField] private TMP_Text criticalDamage;

    private void Awake()
    {

    }

    public void SetInfo()
    {
        heroImage.sprite = hero.GetComponent<Hero>().sprite;
        heroStatus = hero.GetComponent<HeroStatus_>();
        setStatus(heroStatus.GetStatus());

    }


    public void setStatus(HeroStatus_.Status status)
    {
        heroName.text = heroStatus.name;
        maxHp.text = status.maxHp[0].ToString();
        attack.text = status.attack[0].ToString();
        defense.text = status.defense.ToString();
        magicResist.text = status.magicResist.ToString();
        range.text = status.range.ToString();
        attackSpeed.text = status.attackSpeed.ToString();
        maxMp.text = status.maxMp.ToString();
        addMana.text = status.addMana.ToString();
        critical.text = status.critical.ToString();
        criticalDamage.text = status.criticalDamage.ToString();
    }
}
