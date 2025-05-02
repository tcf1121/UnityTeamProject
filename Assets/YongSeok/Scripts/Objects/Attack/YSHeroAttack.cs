using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSHeroAttack : MonoBehaviour
{
    [SerializeField] private YSShooter ySShooter;
    [SerializeField] private float delay = 0.5f;
    [SerializeField] private float bulletSpeed = 10f;

    private Coroutine _myCoroutien;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && _myCoroutien == null)
        {

            _myCoroutien = StartCoroutine(myRoutain());


        }


    }



    private IEnumerator myRoutain()
    {

        WaitForSeconds delayIN = new WaitForSeconds(delay);

        while (Input.GetKey(KeyCode.Alpha1))
        {

            ySShooter.Fire(bulletSpeed);
            yield return new WaitForSeconds(delay);
        }


        _myCoroutien = null;

    }







}
