using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Skills : MonoBehaviour
{
    // private MonsterUnitAnimator animator;
    [SerializeField] Tilemap tilemap;

    private string targetTag;
    private string tag;
    private void Awake()
    {
        // animator = GetComponent<HeroUnitAnimator>();
        tag = gameObject.tag;

        if (tag == "Hero")
            targetTag = "Monster";

        else if (tag == "Monster")
            targetTag = "Hero";

    }

    public void Skill(string skillName, int range, int attack, float coef, Transform target)
    {
        PlaySkillAnimation(skillName);

        switch (skillName)
        {
            // 투사체 생성도 필요할 듯
            case ("Targeting"):
                if (tag == "Hero")
                {
                    MonsterStatus ms = target.GetComponent<MonsterStatus>();
                    ms.CurHp -= (int)(attack * coef);
                }
                else if (tag == "Monster")
                {
                    HeroStatus_ hs = target.GetComponent<HeroStatus_>();
                    hs.CurHp -= (int)(attack * coef);
                }

                break;

            case ("Heal"):
                Vector2Int myCell = GetCellOf(transform.position);
                GameObject[] allies = GameObject.FindGameObjectsWithTag(tag);

                foreach (GameObject ally in allies)
                {
                    Vector2Int allyCell = GetCellOf(ally.transform.position);
                    int dist = HexDistance(myCell, allyCell);

                    if (dist <= range)
                    {
                        if (tag == "Hero")
                        {
                            HeroStatus_ hero = ally.GetComponent<HeroStatus_>();
                            hero.CurHp += (int)(attack * coef);

                            if (hero.CurHp >= hero.b_Status.maxHp[0])
                                hero.CurHp = hero.b_Status.maxHp[0];
                        }

                        else if (tag == "Monster")
                        {
                            MonsterStatus mon = ally.GetComponent<MonsterStatus>();
                            mon.CurHp += (int)(attack * coef);

                            if (mon.CurHp >= mon.battleStatus.maxHp)
                                mon.CurHp = mon.battleStatus.maxHp;
                        }
                    }
                }

                break;

            case ("Range"):
                Vector2Int targetCellPos = GetCellOf(target.position);
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(targetTag);

                foreach (GameObject enemy in enemies)
                {
                    Vector2Int enemyCell = GetCellOf(enemy.transform.position);
                    int dist = HexDistance(enemyCell, targetCellPos);

                    if (dist <= range)
                    {
                        if (tag == "Hero")
                        {
                            MonsterStatus mon = enemy.GetComponent<MonsterStatus>();
                            mon.CurHp -= (int)(attack * coef); // 데미지 만큼 깎기
                        }

                        else if (tag == "Monster")
                        {
                            HeroStatus_ hero = enemy.GetComponent<HeroStatus_>();
                            hero.CurHp -= (int)(attack * coef); // 데미지 만큼 깎기
                        }
                    }
                }

                break;

            case ("Taunt"):
                Vector2Int myCellPos = GetCellOf(transform.position);
                GameObject[] tauntedEnemies = GameObject.FindGameObjectsWithTag(targetTag);

                foreach (GameObject enemy in tauntedEnemies)
                {
                    Vector2Int enemyCell = GetCellOf(enemy.transform.position);
                    int dist = HexDistance(enemyCell, myCellPos);

                    if (dist <= range)
                    {
                        Trace enemyTrace = enemy.GetComponent<Trace>();
                        enemyTrace.Target = gameObject.transform;
                    }
                }

                break;
        }
    }

    private void PlaySkillAnimation(string skillName)
    {
        switch (skillName)
        {
            case ("타겟팅"):
                // animator.TargetSkill();
                break;

            case ("힐"):
                // animator.HealSkill();
                break;

            case ("범위"):
                // animator.SplashSkill();
                break;

            case ("도발"):
                // animator.TauntSkill();
                break;
        }
    }

    private Vector2Int GetCellOf(Vector3 worldPos)
    {
        Vector3Int cell = tilemap.WorldToCell(worldPos);
        return new Vector2Int(cell.x, cell.y);
    }

    int HexDistance(Vector2Int a, Vector2Int b)
    {
        Vector3Int ac = OffsetToCube(a);
        Vector3Int bc = OffsetToCube(b);

        return (Mathf.Abs(ac.x - bc.x) + Mathf.Abs(ac.y - bc.y) + Mathf.Abs(ac.z - bc.z)) / 2;
    }

    Vector3Int OffsetToCube(Vector2Int offset)
    {
        int x = offset.x - (offset.y - (offset.y & 1)) / 2;
        int z = offset.y;
        int y = -x - z;

        return new Vector3Int(x, y, z);
    }
}
