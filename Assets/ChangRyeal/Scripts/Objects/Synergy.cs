using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synergy : MonoBehaviour
{
    [Header ("Job")]
    public int warrior;
    public int wizard;
    public int archer;
    public int thief;
    public int pirate;
    [Header("Synergy")]
    public int adventurer;
    public int hero;
    public int cygnus;

    public void OnBattle()
    {
        ResetSynergy();
        SetSynergy();
    }

    private void ResetSynergy()
    {
        warrior = 0;
        wizard = 0;
        archer = 0;
        thief = 0;
        pirate = 0;
        adventurer = 0;
        hero = 0;
        cygnus = 0;
    }

    // 총 시너지 확인
    private void SetSynergy()
    {
        if (GetSynergy("Aran"))
        {
            warrior++;
            hero++;
        }
        if (GetSynergy("Bishop"))
        {
            wizard++;
            adventurer++;
        }
        if (GetSynergy("Captain"))
        {
            pirate++;
            adventurer++;
        }
        if (GetSynergy("DualBlade"))
        {
            thief++;
            adventurer++;
        }
        if (GetSynergy("Eunwol"))
        {
            pirate++;
            hero++;
        }
        if (GetSynergy("Evan"))
        {
            wizard++;
            hero++;
        }
        if (GetSynergy("FlameWizard"))
        {
            wizard++;
            cygnus++;
        }
        if (GetSynergy("Hero"))
        {
            warrior++;
            adventurer++;
        }
        if (GetSynergy("Marksman"))
        {
            archer++;
            adventurer++;
        }
        if (GetSynergy("Mercedes"))
        {
            archer++;
            hero++;
        }
        if (GetSynergy("NightWalker"))
        {
            thief++;
            cygnus++;
        }
        if (GetSynergy("Phantom"))
        {
            thief++;
            hero++;
        }
        if (GetSynergy("SoulMaster"))
        {
            warrior++;
            cygnus++;
        }
        if (GetSynergy("Striker"))
        {
            pirate++;
            cygnus++;
        }
        if (GetSynergy("WindBreaker"))
        {
            archer++;
            cygnus++;
        }
    }


    // 영웅 종류 확인
    private bool GetSynergy(string name)
    {
        Hero findHero = GameManager.Instance.player.playerHero.BattleHero.Find(hero => hero.heroname == name);
        if (findHero != null)
            return true;
        else
            return false;
    }
}
