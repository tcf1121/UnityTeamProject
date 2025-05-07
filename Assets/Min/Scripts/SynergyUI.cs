using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class SynergyUI : MonoBehaviour
{
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

        // warrior
        if (synergy.warrior <= 2)
            warriorText.text = $"{synergy.warrior} / 2";
        else
            warriorText.text = $"{synergy.warrior} / 3";

        // wizard
        if (synergy.wizard <= 2)
            wizardText.text = $"{synergy.wizard} / 2";
        else
            wizardText.text = $"{synergy.wizard} / 3";

        // thief
        if (synergy.thief <= 2)
            thiefText.text = $"{synergy.thief} / 2";
        else
            thiefText.text = $"{synergy.thief} / 3";

        // archer
        if (synergy.archer <= 2)
            archerText.text = $"{synergy.archer} / 2";
        else
            archerText.text = $"{synergy.archer} / 3";

        // pirate
        if (synergy.pirate <= 2)
            pirateText.text = $"{synergy.pirate} / 2";
        else
            pirateText.text = $"{synergy.pirate} / 3";

        // adventurer
        if (synergy.adventurer <= 2)
            adventurerText.text = $"{synergy.adventurer} / 2";
        else
            adventurerText.text = $"{synergy.adventurer} / 5";

        // hero
        if (synergy.hero <= 3)
            heroText.text = $"{synergy.hero} / 3";
        else
            heroText.text = $"{synergy.hero} / 5";

        // cygnus
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
