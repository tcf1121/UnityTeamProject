using UnityEngine;
using YongSeok;

namespace MonsterRelated
{
    public class BaseAM : ParentsAM
    {
        [Header("Speed")]
        [SerializeField] float moveSpeed;


        public override void MoveMethod(HeroBase target)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}