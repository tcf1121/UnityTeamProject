using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeroShooter : MonoBehaviour
{

    [Header("Property")]
    [SerializeField] Transform muzzlePoint;

    [Header("AttackMethod")]
    [SerializeField] BaseAM basePrefab;
    [SerializeField] SkillAM skillPrefab;

    [Header("Components")]
    [SerializeField] int Mp;
   

    private void Update()
    {
        FireActing();
    }

    private void Fire(ParentsAM bullets)
    {
        ParentsAM instance = Instantiate(bullets, muzzlePoint.position, muzzlePoint.rotation);
        instance.Shot();
    }

    private void FireActing()
    {    
        if (Mp <= 100)
        {
            Fire(basePrefab);
            StopCoroutine(basePrefab.shotCorutine);
            basePrefab.shotCorutine = null;
        }
        else if (Mp > 100)
        {
            Fire(skillPrefab);
            Mp = 0;
        }
        else if (gameObject == null)
        {
            StopCoroutine(basePrefab.shotCorutine);
            basePrefab.shotCorutine = null;
        }
    }
}
