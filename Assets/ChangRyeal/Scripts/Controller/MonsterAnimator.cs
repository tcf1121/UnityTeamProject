using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : ObjectAnimator
{
    [SerializeField] public GameObject[] prefab;    // ¿ÜÇü
    [SerializeField] public Animator animators;
    Coroutine DieCor;

    public override void Move(bool value)
    {
        animators.SetBool("Move", value);
    }

    public override void Attack()
    {
        animators.SetTrigger("Attack");
    }

    public override void Spawn()
    {

        animators.SetTrigger("Spawn");
    }

    public override void Die()
    {
        animators.SetTrigger("Die");
        DieCor = StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1.333f);
        if (DieCor != null)
            StopCoroutine(DieCor);
    }

    public override void CriticalAttack()
    {
    }

    public override void Skill()
    {
    }

    public override void Wait(bool value)
    {
    }

    public override void Pick(bool value)
    {
    }
}
