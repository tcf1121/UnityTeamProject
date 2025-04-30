using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SkillAM : ParentsAM
{
    [Header("Speed")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotatespeed;


    public override void Shot()
    {
        AttackMethod();
        base.Shot();
    }
    public override void AttackMethod()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.forward, rotatespeed * Time.deltaTime);
    }
}