using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.VisualScripting;
using UnityEngine;


public enum SkillType { Targeting, AreaAttack, Heal, Taunt }

public class Skill : MonoBehaviour
{
    

    public string skillName;
    public SkillType skillType;
    public int skillCooldown;

    protected Skill(SkillType type, string name, int cooldown)
    {
        skillType = type;
        skillName = name;
        skillCooldown = cooldown;
    }



}
