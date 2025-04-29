using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseBullets : Bullets
{
    [Header("Speed")]
    [SerializeField] float moveSpeed;
    public Coroutine shotCorutine;

    IEnumerator FireRoutine()
    {
        WaitForSeconds delay = new WaitForSeconds(delayTime);
        while (true)
        {
            Shot();
            yield return delay;
        }
    }

    public override void Shot()
    {
        AttackMethod();
        base.Shot();
        shotCorutine = StartCoroutine(FireRoutine());
    }

    public override void AttackMethod()
    {
        base.AttackMethod();
        transform.position = Vector3.MoveTowards
            (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
    }
}