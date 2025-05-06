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
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider expBar;
    [SerializeField] private GameObject stagePanel;
    [SerializeField] private TMP_Text stage;
    private StringBuilder setstring = new StringBuilder();


    private void OnEnable()
    {
        SetLevel();
        SetGold();
        SetExp();
        SetHp();
        SetStage();

        GameManager.Instance.player.OnLevelChanged += SetLevel;
        GameManager.Instance.player.OnGoldChanged += SetGold;
        GameManager.Instance.player.OnHelthChanged += SetHp;
        GameManager.Instance.player.OnExpChanged += SetExp;
        GameManager.Instance.player.OnStageChanged += SetStage;

        stagePanel.SetActive(true);
    }

    private void OnDisable()
    {
        GameManager.Instance.player.OnLevelChanged -= SetLevel;
        GameManager.Instance.player.OnGoldChanged -= SetGold;
        GameManager.Instance.player.OnHelthChanged -= SetHp;
        GameManager.Instance.player.OnExpChanged -= SetExp;
        GameManager.Instance.player.OnStageChanged -= SetStage;

        stagePanel.SetActive(false);
    }


    private void SetLevel()
    {
        setstring.Clear();
        setstring.Append("Lv. ");
        setstring.Append(GameManager.Instance.player.Level);
        expBar.maxValue = GameManager.Instance.player.MaxExp;
        levelUI.text = setstring.ToString();
    }

    private void SetGold()
    {
        goldUI.text = GameManager.Instance.player.Gold.ToString();
    }

    private void SetHp()
    {
        float hpValue = (float)GameManager.Instance.player.Health / GameManager.Instance.player.MaxHealth;
        hpBar.value = hpValue;
    }

    private void SetExp()
    {
        expBar.value = GameManager.Instance.player.Exp;
    }

    private void SetStage()
    {
        stage.text = GameManager.Instance.player.Stage.ToString();
    }
}
