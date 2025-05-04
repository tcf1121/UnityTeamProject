using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManaManager : MonoBehaviour
{
    // �����ϰ� �������� �Ծ��� �� ������ ���� �κ�

    public int mana;
    public int maxMana;
    public int skillCost;

    public UnityEvent onSkillReady;  // �ӽ÷� ����Ƽ �̺�Ʈ�� ���� (��ų �ߵ�)

    public void AddMana(int manaAmount)
    {
        mana = Mathf.Min(mana + manaAmount, maxMana);
        Debug.Log($"{gameObject.name}�� ������ {manaAmount} ȸ���Ǿ� ���� {mana}�Դϴ�.");

        if (mana >= skillCost)
        {
            TriggerSkill();
        }

    }

    private void TriggerSkill()
    {
        mana = 0; // ��ų ��� �� ���� �ʱ�ȭ
        onSkillReady?.Invoke(); // ����� ��ų Ʈ���� ����
    }


}
