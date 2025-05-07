using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Skills : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;
    [SerializeField] GameObject rangedProjectilePrefab; // 범위 공격 프리팹

    private string targetTag;
    private string tag;
    private void Awake()
    {
        tag = gameObject.tag;

        if (tag == "Hero")
            targetTag = "Monster";

        else if (tag == "Monster")
            targetTag = "Hero";
    }

    public void Skill(string skillName, int range, int attack, float coef, Transform target, GameObject prefab)
    {
        int skillDamage = (int)(attack * coef);

        switch (skillName)
        {
            // 투사체 생성도 필요할 듯
            case ("Targeting"):
                if (range == 1) // 근거리
                {
                    MonsterStatus ms = target.GetComponent<MonsterStatus>();
                    ms.TakeDamage(skillDamage);
                }
                else // 원거리
                {
                    GameObject projectile = Instantiate(
                        prefab,
                        new Vector3(transform.position.x, 1f, transform.position.z),
                        transform.rotation
                        );
                    projectile.GetComponent<Projectile_s>().Initialize(skillDamage, target.gameObject);

                    MonsterStatus ms = target.GetComponent<MonsterStatus>();
                    ms.TakeDamage(skillDamage);
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
                        HeroStatus_ hero = ally.GetComponent<HeroStatus_>();
                        hero.CurHp += skillDamage;

                        if (hero.CurHp >= hero.b_Status.maxHp[0])
                            hero.CurHp = hero.b_Status.maxHp[0];
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
                        GameObject rangedProjectile = Instantiate(rangedProjectilePrefab,
                            new Vector3(transform.position.x, 1f, transform.position.z),
                            transform.rotation
                            );
                        rangedProjectile.GetComponent<RangedSkillProjectiles>().Initialize(skillDamage, target.gameObject, range);

                        Destroy(rangedProjectile, (float)((range * 0.8666) / 2f)); // 일정 범위 후 파괴
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
