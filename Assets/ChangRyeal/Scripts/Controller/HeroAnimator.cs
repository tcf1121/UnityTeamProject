using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class HeroAnimator : ObjectAnimator
{
    [SerializeField] public List<Animator> animators;
    Coroutine dieCor;

    // 기본 공격
    public override void Attack()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Attack");
    }
    // 치명타 공격
    public override void CriticalAttack()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Critical");
    }
    // 스킬
    public override void Skill()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Skill");
    }

    // 대기
    public override void Wait(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Wait", value);
    }

    // 선택
    public override void Pick(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Pick", value);
    }

    // 이동
    public override void Move(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Move", value);
    }

    // 죽음
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
