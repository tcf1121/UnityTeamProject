using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HiroRelated;

public class HeroShooter : MonoBehaviour
{
    [Header("Range and Tageting")]
    [SerializeField] protected float attackRange;
    [SerializeField] private string tagName;

    public Zombie target
    {  get; private set; }

    [Header("Property")]
    [SerializeField] Transform muzzlePoint;

    [Header("AttackMethod")]
    [SerializeField] BaseAM basePrefab;
    [SerializeField] SkillAM skillPrefab;
    [SerializeField] float delayTime;
    private Coroutine FireCorotine;


    [Header("Components")]
    [SerializeField] private int Mp;
    [SerializeField] int upSetMp;


    private void Update()
    {
        AcquireTarget();
        FireActing();
    }

    IEnumerator FireRoutine()
    {
        WaitForSeconds delay = new(delayTime);
        while (true)
        {
            if (Mp <= 100)
            {
                Fire(basePrefab);
                Debug.Log("기본평타!");
                SetMp(upSetMp);
                yield return delay;
            }
            if (Mp > 100)
            {
                Fire(skillPrefab);
                Debug.Log("스킬!");
                Mp = 0;
                yield return delay;
            }

        }
    }

    private void SetMp(int mp)
    {
        Mp += mp;
    }
    
    private void AcquireTarget()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, attackRange);

        float minDist = float.MaxValue;
        Zombie closest = null;
        foreach (var hit in hits)
        {
            if (hit.CompareTag(tagName))
            { 
                Zombie tagName = hit.GetComponent<Zombie>();
                float dist = Vector3.Distance(transform.position, tagName.transform.position);
                if (dist < minDist)
                {
                    minDist = dist;
                    closest = tagName;
                }
            }
        }
        target = closest;
    }
    
    private void Fire(ParentsAM bullets)
    {
        ParentsAM instance = Instantiate(bullets, muzzlePoint.position, muzzlePoint.rotation);
        instance.Shot(target);
    }

    private void FireActing()
    {
        if (target != null)
        {
            if (FireCorotine == null)
                FireCorotine = StartCoroutine(FireRoutine());
        }
        else
        {
            if (FireCorotine != null)
            {
                StopCoroutine(FireCorotine);
                FireCorotine = null;
            }
        }

    }
}
