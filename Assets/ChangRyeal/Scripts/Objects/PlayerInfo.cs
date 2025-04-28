using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text levelUI;
    [SerializeField] private TMP_Text goldUI;
    [SerializeField] private Slider HpBar;
    [SerializeField] private Slider ExpBar;
    private StringBuilder setstring = new StringBuilder();

    private void OnEnable()
    {
        SetLevel();
        SetGold();
        SetExp();
        SetHp();

        GameManager.Instance.player.OnLevelChanged += SetLevel;
        GameManager.Instance.player.OnGoldChanged += SetGold;
        GameManager.Instance.player.OnHelthChanged += SetHp;
        GameManager.Instance.player.OnExpChanged += SetExp;
    }

    private void OnDisable()
    {
        GameManager.Instance.player.OnLevelChanged -= SetLevel;
        GameManager.Instance.player.OnGoldChanged -= SetGold;
        GameManager.Instance.player.OnHelthChanged -= SetHp;
        GameManager.Instance.player.OnExpChanged -= SetExp;
    }


    private void SetLevel()
    {
        setstring.Clear();
        setstring.Append("Lv. ");
        setstring.Append(GameManager.Instance.player.Level);
        ExpBar.maxValue = GameManager.Instance.player.MaxExp;
        levelUI.text = setstring.ToString();
    }

    private void SetGold()
    {
        setstring.Clear();
        setstring.Append(GameManager.Instance.player.Gold);
        setstring.Append("G");
        goldUI.text = setstring.ToString();
    }

    private void SetHp()
    {
        float hpValue = (float)GameManager.Instance.player.Health / GameManager.Instance.player.MaxHealth;
        HpBar.value = hpValue;
    }

    private void SetExp()
    {
        ExpBar.value = GameManager.Instance.player.Exp;
    }
}
