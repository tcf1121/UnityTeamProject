using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : Bullets
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotatespeed;

    protected override void AttackMethod()
    {
        gameObject.transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        gameObject.transform.RotateAround(gameObject.transform.position, Vector3.up, rotatespeed * Time.deltaTime);
        
    }
}