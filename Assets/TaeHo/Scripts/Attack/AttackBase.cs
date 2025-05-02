using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackBase : MonoBehaviour // 공통적인 공격
{
    public int damage;  // 공격 데미지
    public float attackCoolDown; // 공격 쿨타임 (시간 상관없이 때리면 안되기 때문에)
    private float lastAttackTime; // 마지막으로 공격한 시점 (쿨타임 맞추기 위해서 필요)


    public virtual void TryAttack()
    {
        if (Time.time > lastAttackTime + attackCoolDown) 
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }


    protected abstract void Attack(); // 원거리, 근거리 마다 공격 모션 다르게 구성

}
