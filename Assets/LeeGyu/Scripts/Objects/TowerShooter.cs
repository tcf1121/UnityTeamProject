using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TowerShooter : MonoBehaviour
{
    [SerializeField] Transform Muzzle;
    [SerializeField] Bullets baseBullets;
    [SerializeField] Bullets skillBullets;
    [SerializeField] float delayTime;
    [SerializeField] int Mp;
    private Coroutine shotCorutine;

    IEnumerator FireRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(delayTime);
        while (true)
        {
            baseBullets.Shot();
            yield return delay;
        }
    }

    private void SkilShot()
    {
        skillBullets.Shot();
    }

    private void FireActing()
    {
        while (true)
        {
            if (Mp <= 100)
            {
                Instantiate(baseBullets,Muzzle.position,Muzzle.rotation);
                shotCorutine = null;
                shotCorutine = StartCoroutine(FireRoutine());
            }
            else if (Mp > 100)
            {
                Instantiate(skillBullets, Muzzle.position, Muzzle.rotation);
                StopCoroutine(shotCorutine);
                SkilShot();
                Mp = 0;
            }
            else if (gameObject == null)
            {
                shotCorutine = null;
                StopCoroutine(shotCorutine);
                break;
            }
        }

    }
    private void OnEnable()
    {
        FireActing();
    }
}
