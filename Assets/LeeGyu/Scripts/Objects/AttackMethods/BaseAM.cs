using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseAM : ParentsAM
{
    [Header("Speed")]
    [SerializeField] float moveSpeed;
    

    public override void MoveMethod(Zombie target)
    {
        transform.position = Vector3.MoveTowards
            (transform.position, target.transform.position, moveSpeed * Time.deltaTime);  
    }
}