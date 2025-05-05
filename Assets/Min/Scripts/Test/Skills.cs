using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Skills : MonoBehaviour
{
    // private MonsterUnitAnimator animator;
    [SerializeField] Trace trace;
    [SerializeField] Tilemap tilemap;

    private string targetTag;
    private string tag;
    private void Awake()
    {
        // animator = GetComponent<HeroUnitAnimator>();
        tag = gameObject.tag;

        if (tag == "Hero")
        {
            targetTag = "Monster";
        }
        else if (tag == "Monster")
        {
            targetTag = "Hero";
        }
    }

    public void Skill(string skillName, int range)
    {
        PlaySkillAnimation(skillName);

        Transform target = trace.Target;

        switch (skillName)
        {
            // 투사체 생성도 필요할 듯
            case ("타겟팅"):
                if (tag == "Hero")
                {
                    MonsterStatus ms = target.GetComponent<MonsterStatus>();
                    ms.battleStatus.maxHp -= 100; // curHp로 변경해야됨
                }
                else if (tag == "Monster")
                {
                    HeroStatus_ hs = target.GetComponent<HeroStatus_>();
                    hs.b_Status.maxMp -= 100;
                }

                break;

            case ("힐"):
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
                            //hero.battleStatus.curHp += 10;

                            //if (hero.battleStatus.curHp >= hero.battleStatus.maxHp)
                            //{
                            //    hero.battleStatus.curHp = hero.battleStatus.maxHp;
                            //}
                        }

                        else if (tag == "Monster")
                        {
                            MonsterStatus mon = ally.GetComponent<MonsterStatus>();
                            //mon.battleStatus.curHp += 10;

                            //if (mon.battleStatus.curHp >= mon.battleStatus.maxHp)
                            //{
                            //    mon.battleStatus.curHp = mon.battleStatus.maxHp;
                            //}
                        }
                    }
                }

                break;

            case ("범위"):
                Vector2Int targetCellPos = GetCellOf(trace.Target.transform.position);
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
                            mon.battleStatus.maxHp -= 100; // 데미지 만큼 깎기
                        }

                        else if (tag == "Monster")
                        {
                            HeroStatus_ hero = enemy.GetComponent<HeroStatus_>();
                            hero.b_Status.maxMp -= 100; // 데미지 만큼 깎기
                        }
                    }
                }

                break;

            case ("도발"):
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
