using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiroRelated
{
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
}
