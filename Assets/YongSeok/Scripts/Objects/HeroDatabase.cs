using System.Collections.Generic;
using UnityEngine;

public class HeroDatabase : MonoBehaviour
{
    private static List<HeroStatus> heroList;

    static HeroDatabase()
    {
        heroList = new List<HeroStatus>
        {
            new HeroStatus {
                job = "전사", heroId = "전사1", prefabName = "Aran", championName = "니달리", cost = 1,
                hp = new[] { 650, 1170, 2106 }, maxHp = new[] { 650, 1170, 2106 },
                attack = new[] { 40, 60, 90 }, dps = new[] { 30f, 45f, 68f },
                attackRange = 1f, attackSpeed = 0.75f, defense = 40, magicResist = 40, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "전사", heroId = "전사2", prefabName = "Hero", championName = "자르반 4세", cost = 3,
                hp = new[] { 850, 1530, 2754 }, maxHp = new[] { 850, 1530, 2754 },
                attack = new[] { 60, 90, 135 }, dps = new[] { 39f, 59f, 88f },
                attackRange = 1f, attackSpeed = 0.65f, defense = 50, magicResist = 50, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "전사", heroId = "전사3", prefabName = "SoulMaster", championName = "가렌", cost = 5,
                hp = new[] { 1100, 1980, 3564 }, maxHp = new[] { 1100, 1980, 3564 },
                attack = new[] { 80, 120, 180 }, dps = new[] { 68f, 102f, 153f },
                attackRange = 1f, attackSpeed = 0.85f, defense = 60, magicResist = 60, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "마법사", heroId = "마법사1", prefabName = "Bishop", championName = "모르가나", cost = 1,
                hp = new[] { 500, 900, 1620 }, maxHp = new[] { 500, 900, 1620 },
                attack = new[] { 30, 45, 68 }, dps = new[] { 21f, 31f, 48f },
                attackRange = 4f, attackSpeed = 0.7f, defense = 15, magicResist = 15, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "마법사", heroId = "마법사2", prefabName = "Evan", championName = "베이가", cost = 2,
                hp = new[] { 550, 990, 1782 }, maxHp = new[] { 550, 990, 1782 },
                attack = new[] { 35, 53, 79 }, dps = new[] { 25f, 37f, 55f },
                attackRange = 4f, attackSpeed = 0.7f, defense = 20, magicResist = 20, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "마법사", heroId = "마법사3", prefabName = "FlameWizard", championName = "브랜드", cost = 4,
                hp = new[] { 800, 1440, 2592 }, maxHp = new[] { 800, 1440, 2592 },
                attack = new[] { 35, 53, 79 }, dps = new[] { 26f, 40f, 59f },
                attackRange = 4f, attackSpeed = 0.75f, defense = 30, magicResist = 30, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "도적", heroId = "도적1", prefabName = "DualBlade", championName = "에코", cost = 2,
                hp = new[] { 800, 1440, 2592 }, maxHp = new[] { 800, 1440, 2592 },
                attack = new[] { 45, 68, 101 }, dps = new[] { 31f, 48f, 71f },
                attackRange = 1f, attackSpeed = 0.7f, defense = 45, magicResist = 45, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "도적", heroId = "도적2", prefabName = "NightWalker", championName = "렝가", cost = 3,
                hp = new[] { 750, 1350, 2430 }, maxHp = new[] { 750, 1350, 2430 },
                attack = new[] { 63, 95, 142 }, dps = new[] { 50f, 76f, 114f },
                attackRange = 1f, attackSpeed = 0.8f, defense = 50, magicResist = 50, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "도적", heroId = "도적3", prefabName = "Phantom", championName = "제드", cost = 4,
                hp = new[] { 1050, 1890, 3402 }, maxHp = new[] { 1050, 1890, 3402 },
                attack = new[] { 50, 75, 113 }, dps = new[] { 48f, 71f, 107f },
                attackRange = 1f, attackSpeed = 0.95f, defense = 65, magicResist = 65, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "해적", heroId = "해적1", prefabName = "Captain", championName = "그레이브즈", cost = 2,
                hp = new[] { 800, 1440, 2592 }, maxHp = new[] { 800, 1440, 2592 },
                attack = new[] { 50, 75, 113 }, dps = new[] { 30f, 45f, 68f },
                attackRange = 2f, attackSpeed = 0.6f, defense = 55, magicResist = 55, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "해적", heroId = "해적2", prefabName = "Eunwol", championName = "드레이븐", cost = 3,
                hp = new[] { 700, 1260, 2268 }, maxHp = new[] { 700, 1260, 2268 },
                attack = new[] { 53, 80, 119 }, dps = new[] { 40f, 60f, 89f },
                attackRange = 4f, attackSpeed = 0.75f, defense = 25, magicResist = 25, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "해적", heroId = "해적3", prefabName = "Striker", championName = "자크", cost = 5,
                hp = new[] { 900, 1620, 2916 }, maxHp = new[] { 900, 1620, 2916 },
                attack = new[] { 60, 90, 135 }, dps = new[] { 45f, 68f, 101f },
                attackRange = 1f, attackSpeed = 0.75f, defense = 40, magicResist = 40, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "궁수", heroId = "궁수1", prefabName = "Marksman", championName = "코그모", cost = 1,
                hp = new[] { 500, 900, 1620 }, maxHp = new[] { 500, 900, 1620 },
                attack = new[] { 50, 75, 113 }, dps = new[] { 35f, 53f, 79f },
                attackRange = 4f, attackSpeed = 0.7f, defense = 15, magicResist = 15, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "궁수", heroId = "궁수2", prefabName = "Mercedes", championName = "자야", cost = 4,
                hp = new[] { 800, 1440, 2592 }, maxHp = new[] { 800, 1440, 2592 },
                attack = new[] { 65, 98, 146 }, dps = new[] { 52f, 78f, 117f },
                attackRange = 4f, attackSpeed = 0.8f, defense = 30, magicResist = 30, mana = 100, addMana = 10
            },
            new HeroStatus {
                job = "궁수", heroId = "궁수3", prefabName = "WindBreaker", championName = "오로라", cost = 5,
                hp = new[] { 800, 1440, 2592 }, maxHp = new[] { 800, 1440, 2592 },
                attack = new[] { 50, 75, 113 }, dps = new[] { 40f, 60f, 90f },
                attackRange = 4f, attackSpeed = 0.58f, defense = 40, magicResist = 40, mana = 100, addMana = 10
            }
        };
    }

    public static HeroStatus GetHeroById(string heroId)
    {
        return heroList.Find(h => h.heroId == heroId);
    }

    public static HeroStatus GetHeroByPrefabName(string prefabName)
    {
        return heroList.Find(h => h.prefabName == prefabName);
    }

    public static List<HeroStatus> GetAllHeroes()
    {
        return heroList;
    }
}

