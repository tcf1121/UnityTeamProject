using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HeroAnimator : ObjectAnimator
{
    [SerializeField] public List<Animator> animators;
    Coroutine dieCor;

    // �⺻ ����
    public override void Attack()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Attack");
    }
    // ġ��Ÿ ����
    public override void CriticalAttack()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Critical");
    }
    // ��ų
    public override void Skill()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Skill");
    }

    // ���
    public override void Wait(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Wait", value);
    }

    // ����
    public override void Pick(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Pick", value);
    }

    // �̵�
    public override void Move(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Move", value);
    }

    // ����
    public override void Die()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Die");
        dieCor = StartCoroutine(DieCoroutine());
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(2f);
        if (dieCor != null)
            StopCoroutine(dieCor);
    }

    public override void Spawn()
    {
    }
}
