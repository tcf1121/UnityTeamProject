using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBullet : Bullets
{
    [SerializeField] float moveSpeed;
    protected override void AttackMethod()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed *  Time.deltaTime);
    }
}