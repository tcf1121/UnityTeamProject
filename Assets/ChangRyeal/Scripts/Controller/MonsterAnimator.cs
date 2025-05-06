using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    [SerializeField] public GameObject[] prefab;    // ¿ÜÇü

    [SerializeField] List<Animator> animators;
    Coroutine DieCor;

    private void OnEnable()
    {
        int shape = 0;
        if (gameObject.GetComponent<MonsterStatus>().boss) shape = 0;
        else
        {
            if (GameManager.Instance.player.Stage < 7)
                shape = 0;
            else if (GameManager.Instance.player.Stage < 14)
                shape = 1;
            else
                shape = 2;
        }

        GameObject monster = Instantiate(prefab[shape], new Vector3(0, 0, 0), Quaternion.Euler(new Vector3(0, 180, 0)));
        monster.AddComponent<MonsterStatus>();
        monster.GetComponent<MonsterStatus>().SetStatus(gameObject.GetComponent<MonsterStatus>());
        monster.name = name;
    }
    public void Move(bool value)
    {
        foreach (Animator ani in animators)
            ani.SetBool("Move", value);
    }

    public void Attack()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Attack");
    }

    public void Spawn()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Spawn");
    }

    public void Die()
    {
        foreach (Animator ani in animators)
            ani.SetTrigger("Die");
        DieCor = StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        yield return new WaitForSeconds(1.333f);
        if (DieCor != null)
            StopCoroutine(DieCor);
    }
}
