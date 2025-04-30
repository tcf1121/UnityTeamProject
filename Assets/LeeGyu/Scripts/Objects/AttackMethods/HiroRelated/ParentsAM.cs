using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HiroRelated
{
    public abstract class ParentsAM : MonoBehaviour
    {

        [Header("Property")]
        [SerializeField] private int attackPower;
        [SerializeField] private string tagName;

        private Zombie target;

        public int AttackPower
        { get { return attackPower; } set { attackPower = value; } }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagName))
            {
                Zombie zombie = other.gameObject.GetComponent<Zombie>();
                TakeDamge(zombie);
                Debug.Log("¸Â¾Ò´Ù!");
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            MoveMethod(target);
        }
        public void Shot(Zombie target)
        {
            this.target = target;
        }
    
        public abstract void MoveMethod(Zombie target);

        public void TakeDamge(Zombie target)
        {
            target.TakeDamage(attackPower);
        }

    }
}
