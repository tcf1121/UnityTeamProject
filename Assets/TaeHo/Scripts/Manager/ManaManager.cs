using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ManaManager : MonoBehaviour
{
    // 공격하고 데미지를 입었을 때 마나가 차는 부분

    public int mana;
    public int maxMana;
    public int skillCost;

    public UnityEvent onSkillReady;  // 임시로 유니티 이벤트를 넣음 (스킬 발동)

    public void AddMana(int manaAmount)
    {
        mana = Mathf.Min(mana + manaAmount, maxMana);
        Debug.Log($"{gameObject.name}의 마나가 {manaAmount} 회복되어 현재 {mana}입니다.");

        if (mana >= skillCost)
        {
            TriggerSkill();
        }

    }

    private void TriggerSkill()
    {
        mana = 0; // 스킬 사용 후 마나 초기화
        onSkillReady?.Invoke(); // 연결된 스킬 트리거 실행
    }


}
