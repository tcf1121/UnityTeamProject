using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectAnimator : MonoBehaviour
{

    public abstract void Attack();

    // 치명타 공격
    public abstract void CriticalAttack();

    // 스킬
    public abstract void Skill();

    // 대기
    public abstract void Wait(bool value);

    // 선택
    public abstract void Pick(bool value);

    // 이동
    public abstract void Move(bool value);

    // 죽음
    public abstract void Die();

    // 스폰
    public abstract void Spawn();

}
