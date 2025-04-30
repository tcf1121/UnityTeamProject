using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using YongSeok;

namespace MonsterRelated
{
    public abstract class ParentsAM : MonoBehaviour
    {

        [Header("Property")]
        [SerializeField] private int attackPower;
        [SerializeField] private string tagName;

        private HeroBase target;

        public int AttackPower
        { get { return attackPower; } set { attackPower = value; } }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagName))
            {
                HeroBase zombie = other.gameObject.GetComponent<HeroBase>();
                TakeDamge(zombie);
                Debug.Log("¸Â¾Ò´Ù!");
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            MoveMethod(target);
        }
        public void Shot(HeroBase target)
        {
            this.target = target;
        }
    
        public abstract void MoveMethod(HeroBase target);

        public void TakeDamge(HeroBase target)
        {
            target.Hp -= attackPower;
        }
        

            
    }
}
