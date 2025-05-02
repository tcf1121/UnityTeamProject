using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Zombie : MonoBehaviour
{
    private bool isAttacking;

    private Coroutine AttackCoroutine;

    // �ٲ�ߵ� Ŭ����
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
        // ���Ͱ� �����ϴ� �Լ� ����
    }

    public virtual void Move()
    {
        // ���� ������ ����
        transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log($"���� ü��: {CurrentHealth}");
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
        // �׾��� �� �̺�Ʈ �߻�
        // OnZombieDied?.Invoke();

        // �ƴϸ� ���� �Ŵ��� ȣ���Ͽ� �� �� ����ġ �ø���
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
