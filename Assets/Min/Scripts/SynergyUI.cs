using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class SynergyUI : MonoBehaviour
{
    [SerializeField] TMP_Text synergyText;

    
    public void UpdateSynergyText(Synergy synergy)
    {
        StringBuilder sb = new StringBuilder();

        // warrior
        if (synergy.warrior <= 2)
            sb.AppendLine($"Warrior: {synergy.warrior} / 2");
        else
            sb.AppendLine($"Warrior: {synergy.warrior} / 3");

        // wizard
        if (synergy.wizard <= 2)
            sb.AppendLine($"Wizard: {synergy.wizard} / 2");
        else
            sb.AppendLine($"Wizard: {synergy.wizard} / 3");

        // thief
        if (synergy.thief <= 2)
            sb.AppendLine($"Thief: {synergy.thief} / 2");
        else
            sb.AppendLine($"Thief: {synergy.thief} / 3");

        // archer
        if (synergy.archer <= 2)
            sb.AppendLine($"Archer: {synergy.archer} / 2");
        else
            sb.AppendLine($"Archer: {synergy.archer} / 3");

        // pirate
        if (synergy.pirate <= 2)
            sb.AppendLine($"Pirate: {synergy.pirate} / 2");
        else
            sb.AppendLine($"Pirate: {synergy.pirate} / 3");

        // adventurer
        if (synergy.adventurer <= 2)
            sb.AppendLine($"Adventurer: {synergy.adventurer} / 2");
        else
            sb.AppendLine($"Adventurer: {synergy.adventurer} / 5");

        // hero
        if (synergy.hero <= 3)
            sb.AppendLine($"Hero: {synergy.hero} / 3");
        else
            sb.AppendLine($"Hero: {synergy.hero} / 5");

        // cygnus
        if (synergy.cygnus <= 2)
            sb.AppendLine($"Cygnus: {synergy.cygnus} / 2");
        else if (synergy.cygnus <= 3)
            sb.AppendLine($"Cygnus: {synergy.cygnus} / 3");
        else if (synergy.cygnus <= 4)
            sb.AppendLine($"Cygnus: {synergy.cygnus} / 4");
        else
            sb.AppendLine($"Cygnus: {synergy.cygnus} / 5");

        synergyText.text = sb.ToString();
    }
}
