using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using YongSeok;

namespace MonsterRelated
{
    public class SkillAM : ParentsAM
    {
        [Header("Speed")]
        [SerializeField] float moveSpeed;
        [SerializeField] float rotatespeed;


        public override void MoveMethod(HeroBase target)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, moveSpeed);
            transform.Rotate(Vector3.forward, rotatespeed * Time.deltaTime);
        }
    }
}