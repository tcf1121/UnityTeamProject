using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
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
                shotCorutine = null;
                shotCorutine = StartCoroutine(FireRoutine());
            }
            else if (Mp > 100)
            {
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
