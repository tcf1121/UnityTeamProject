using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TowerShooter : MonoBehaviour
{

    [Header("Property")]
    [SerializeField] Transform muzzle;
    [SerializeField] BaseBullets baseBulletPrefab;
    [SerializeField] Bullets skillBulletPrefab;

    [Header("Components")]
    [SerializeField] int Mp;
   

    private void Update()
    {
        FireActing();
    }

    private void Fire(Bullets bullets)
    {
        Bullets instance = Instantiate(bullets, muzzle.position, muzzle.rotation);
        instance.Shot();
    }

    private void FireActing()
    {    
        if (Mp <= 100)
        {
            Fire(baseBulletPrefab);
            StopCoroutine(baseBulletPrefab.shotCorutine);
            baseBulletPrefab.shotCorutine = null;
        }
        else if (Mp > 100)
        {
            Fire(skillBulletPrefab);
            Mp = 0;
        }
        else if (gameObject == null)
        {
            StopCoroutine(baseBulletPrefab.shotCorutine);
            baseBulletPrefab.shotCorutine = null;
        }
    }
}
