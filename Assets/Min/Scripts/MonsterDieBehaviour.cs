using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDieBehaviour : MonoBehaviour
{
    [SerializeField] int dropGold;
    [SerializeField] MonsterStatus ms;

    private void Awake()
    {
        ms.SetBattleStatus();
        

        // �̺�Ʈ ���� DieCheck()
    }

    //private void DieCheck()
    //{
    //    if (ms.battleStatus.curHp <= 0)
    //    {
    //        Die();
    //    }
    //}

    //private void Die()
    //{
    //    GameManager.Instance.player.Gold += dropGold;
    //    Destroy(gameObject);
    //}
}
