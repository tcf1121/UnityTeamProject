using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectAnimator : MonoBehaviour
{

    public abstract void Attack();

    // ġ��Ÿ ����
    public abstract void CriticalAttack();

    // ��ų
    public abstract void Skill();

    // ���
    public abstract void Wait(bool value);

    // ����
    public abstract void Pick(bool value);

    // �̵�
    public abstract void Move(bool value);

    // ����
    public abstract void Die();

    // ����
    public abstract void Spawn();

}
