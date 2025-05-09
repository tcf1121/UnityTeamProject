using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SynergyUI : MonoBehaviour
{
    [SerializeField] GameObject warriorSynergy;
    [SerializeField] GameObject wizardSynergy;
    [SerializeField] GameObject thiefSynergy;
    [SerializeField] GameObject archerSynergy;
    [SerializeField] GameObject pirateSynergy;
    [SerializeField] GameObject adventurerSynergy;
    [SerializeField] GameObject heroSynergy;
    [SerializeField] GameObject cygnusSynergy;
    [SerializeField] TMP_Text warriorText;
    [SerializeField] TMP_Text wizardText;
    [SerializeField] TMP_Text thiefText;
    [SerializeField] TMP_Text archerText;
    [SerializeField] TMP_Text pirateText;
    [SerializeField] TMP_Text adventurerText;
    [SerializeField] TMP_Text heroText;
    [SerializeField] TMP_Text cygnusText;



    public void UpdateSynergyText(Synergy synergy)
    {
        UpdateSynergyPanel(synergy);
        // warrior
        if (synergy.warrior == 0)
            warriorSynergy.SetActive(false);
        else
        {
            warriorSynergy.SetActive(true);
            if (synergy.warrior <= 2)
                warriorText.text = $"{synergy.warrior} / 2";
            else
                warriorText.text = $"{synergy.warrior} / 3";
        }
        // wizard
        if (synergy.wizard == 0)
            wizardSynergy.SetActive(false);
        else
        {
            wizardSynergy.SetActive(true);
            if (synergy.wizard <= 2)
                wizardText.text = $"{synergy.wizard} / 2";
            else
                wizardText.text = $"{synergy.wizard} / 3";
        }
        // thief
        if (synergy.thief == 0)
            thiefSynergy.SetActive(false);
        else
        {
            thiefSynergy.SetActive(true);
            if (synergy.thief <= 2)
                thiefText.text = $"{synergy.thief} / 2";
            else
                thiefText.text = $"{synergy.thief} / 3";
        }


        // archer
        if (synergy.archer == 0)
            archerSynergy.SetActive(false);
        else
        {
            archerSynergy.SetActive(true);
            if (synergy.archer <= 2)
                archerText.text = $"{synergy.archer} / 2";
            else
                archerText.text = $"{synergy.archer} / 3";
        }
        // pirate
        if (synergy.pirate == 0)
            pirateSynergy.SetActive(false);
        else
        {
            pirateSynergy.SetActive(true);
            if (synergy.pirate <= 2)
                pirateText.text = $"{synergy.pirate} / 2";
            else
                pirateText.text = $"{synergy.pirate} / 3";
        }
        // adventurer
        if (synergy.adventurer == 0)
            adventurerSynergy.SetActive(false);
        else
        {
            adventurerSynergy.SetActive(true);
            if (synergy.adventurer <= 2)
                adventurerText.text = $"{synergy.adventurer} / 2";
            else
                adventurerText.text = $"{synergy.adventurer} / 5";
        }
        // hero
        if (synergy.hero == 0)
            heroSynergy.SetActive(false);
        else
        {
            heroSynergy.SetActive(true);
            if (synergy.hero <= 3)
                heroText.text = $"{synergy.hero} / 3";
            else
                heroText.text = $"{synergy.hero} / 5";
        }
        // cygnus
        if (synergy.cygnus == 0)
            cygnusSynergy.SetActive(false);
        else
        {
            cygnusSynergy.SetActive(true);
            if (synergy.cygnus <= 2)
                cygnusText.text = $"{synergy.cygnus} / 2";
            else if (synergy.cygnus <= 3)
                cygnusText.text = $"{synergy.cygnus} / 3";
            else if (synergy.cygnus <= 4)
                cygnusText.text = $"{synergy.cygnus} / 4";
            else
                cygnusText.text = $"{synergy.cygnus} / 5";
        }
    }

    public void UpdateSynergyPanel(Synergy synergy)
    {

        //warriorSynergy.SetActive(true);
        if(synergy.warrior < 2)
            warriorSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.warrior < 3)
            warriorSynergy.GetComponent<SynergyPanel>().effectNum = "200";
        else if(synergy.warrior == 3)
            warriorSynergy.GetComponent<SynergyPanel>().effectNum = "400";
        warriorSynergy.GetComponent<SynergyPanel>().SetExplane();

        //wizardSynergy.SetActive(true);
        if (synergy.wizard < 2)
            wizardSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.wizard < 3)
            wizardSynergy.GetComponent<SynergyPanel>().effectNum = "5";
        else if (synergy.wizard == 3)
            wizardSynergy.GetComponent<SynergyPanel>().effectNum = "15";
        wizardSynergy.GetComponent<SynergyPanel>().SetExplane();

        //thiefSynergy.SetActive(true);
        if (synergy.thief < 2)
            thiefSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.thief < 3)
            thiefSynergy.GetComponent<SynergyPanel>().effectNum = "20%";
        else if (synergy.thief == 3)
            thiefSynergy.GetComponent<SynergyPanel>().effectNum = "40%";
        thiefSynergy.GetComponent<SynergyPanel>().SetExplane();

        //archerSynergy.SetActive(true);
        if (synergy.archer < 2)
            archerSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.archer < 3)
            archerSynergy.GetComponent<SynergyPanel>().effectNum = "1";
        else if (synergy.archer == 3)
            archerSynergy.GetComponent<SynergyPanel>().effectNum = "2";
        archerSynergy.GetComponent<SynergyPanel>().SetExplane();

        //pirateSynergy.SetActive(true);
        if (synergy.pirate < 2)
            pirateSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.pirate < 3)
            pirateSynergy.GetComponent<SynergyPanel>().effectNum = "15";
        else if (synergy.pirate == 3)
            pirateSynergy.GetComponent<SynergyPanel>().effectNum = "30";
        pirateSynergy.GetComponent<SynergyPanel>().SetExplane();

        //adventurerSynergy.SetActive(true);
        if (synergy.adventurer < 2)
            adventurerSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.adventurer < 5)
            adventurerSynergy.GetComponent<SynergyPanel>().effectNum = "0.1";
        else if (synergy.adventurer == 5)
            adventurerSynergy.GetComponent<SynergyPanel>().effectNum = "0.2";
        adventurerSynergy.GetComponent<SynergyPanel>().SetExplane();

        //heroSynergy.SetActive(true);
        if (synergy.hero < 3)
            heroSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.hero < 5)
            heroSynergy.GetComponent<SynergyPanel>().effectNum = "1.5น่";
        else if (synergy.hero == 5)
            heroSynergy.GetComponent<SynergyPanel>().effectNum = "2น่";
        heroSynergy.GetComponent<SynergyPanel>().SetExplane();

        //cygnusSynergy.SetActive(true);
        if (synergy.cygnus < 2)
            cygnusSynergy.GetComponent<SynergyPanel>().effectNum = "0";
        else if (synergy.cygnus < 3)
            cygnusSynergy.GetComponent<SynergyPanel>().effectNum = "5";
        else if (synergy.cygnus < 4)
            cygnusSynergy.GetComponent<SynergyPanel>().effectNum = "15";
        else if (synergy.cygnus < 5)
            cygnusSynergy.GetComponent<SynergyPanel>().effectNum = "25";
        else if (synergy.cygnus == 5)
            cygnusSynergy.GetComponent<SynergyPanel>().effectNum = "40";
        cygnusSynergy.GetComponent<SynergyPanel>().SetExplane();
    }
}
