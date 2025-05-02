using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    private bool isAttacking;

    private Coroutine AttackCoroutine;

    // 바꿔야될 클래스
    //private TestPlant targetPlant;

    protected abstract string Name { get; set; }

    protected abstract int CurrentHealth { get; set; }
    public int currentHealth => CurrentHealth;
    protected abstract int MaxHealth { get; set; }

    protected abstract int Power { get; set; }

    protected abstract float AttackSpeed { get; set; }

    protected abstract float MoveSpeed { get; set; }

    protected abstract int Level { get; set; }

    protected abstract int DropGold { get; set; }

    protected abstract int DropExp { get; set; }

    protected abstract float AttackRange { get; set; }


    private void Update()
    {
        if (!isAttacking)
            Move();
    }


    public virtual void SpawnPoint()
    {
        // 몬스터가 스폰하는 함수 구현
    }

    public virtual void Move()
    {
        // 추후 수정할 로직
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"현재 체력: {CurrentHealth}");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        // 죽었을 때 이벤트 발생
        // OnZombieDied?.Invoke();

        // 아니면 게임 매니저 호출하여 돈 및 경험치 올리기
        // GameManager.Instance.Gold += DropGold;
        // GameManager.Instance.Exp += DropExp;
    }

    private void ArrivedEnd()
    {
        // GameManger.Instance.HP--;
        // Destroy(gameObject);
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Turret"))
    //    {
    //        targetPlant = other.GetComponent<TestPlant>();
    //        if (targetPlant != null)
    //        {
    //            isAttacking = true;
    //            AttackCoroutine = StartCoroutine(AttackRoutine());
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Turret"))
    //    {
    //        if (targetPlant == other.GetComponent<TestPlant>())
    //        {
    //            isAttacking = false;
    //            StopCoroutine(AttackCoroutine);
    //        }
    //    }
    //}


    //IEnumerator AttackRoutine()
    //{
    //    while (true)
    //    {
    //        if (targetPlant == null)
    //        {
    //            isAttacking = false;
    //            yield break;
    //        }

    //        targetPlant.TakeDamage(Power);
    //        yield return new WaitForSeconds(AttackSpeed);
    //    }
    //}
}
