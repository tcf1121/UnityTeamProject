using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.Events;

public class PlantBase : MonoBehaviour
{
    /// <summary>
    /// 식물 기본 Status
    /// </summary>
    // 기물의 HP
    [SerializeField] private int hp;
    public int Hp { get { return hp; } set { hp = value; } }

    [SerializeField] private float defense;
    // 기물을 구입하기 위해 필요한 금액
    [SerializeField] private int cost;
    public int Cost
    {
        get { return cost; }
    }
    // 기물의 마나
    [SerializeField] private int mana;
    public int Mana { get { return mana; } set { mana = value; } }

    ///추가로 필요할 것이라 생각되는 필드
    
    [SerializeField] GameObject PlantObject;
    //마나 추가에 따른 매직넘버 제거용.
    private int addMana;
    //랭크 업 이벤트
    public event Action OnRankUp;
    //기물 리스트 향후 플레이어에서 처리할 것으로 보임.
    //private List<GameObject> Units = new List<GameObject>();
    // 랭크업 관련 기물 갯수 파악용
    private int pieceCount;
    // 랭크 단계
    private int currentRank = 1;
    // Maxhp 필요
    [SerializeField] private int maxHp;
    public int MaxHp
    {
        get { return maxHp; }
    }
    // shop 구매를 위한 플레이어 데이터
    

    //데미지 받음에 따라 마나 증가 및 체력 0 오브젝트 비활성화
    private void OnDamageTaken(int damage)
    {
        if (hp - damage < 0)
        {
            hp = 0;
        }
        else
        {
            hp -= damage;
        }

            mana += addMana;

    if (hp <= 0)
        {
            
            PlantObject.SetActive(false);
            Debug.Log("채력이 0보다 낮아 죽었습니다.");
        }
    }

    //승습 랭크업 (이벤트)
    private void RankUp()
    {
        if(pieceCount >= 3)
        {
            Debug.Log("3개 이상 기물이 있습니다. 랭크업 이벤트 발생!");
            OnRankUp?.Invoke();
            currentRank += 1;
            Debug.Log($"현재 랭크는 : {currentRank} 입니다.");
        }
    }

    // 기물 생성 (이벤트)
    private void SpawmPiece()
    {
        //금액 관련 플레이어와 상호작용 필요.
        if(cost == 1000)
        {
            //생성 위치에 대한 정보가 필요함.
            Instantiate(PlantObject, Vector3.zero, Quaternion.identity);

        }
    }

    //다음 스테이지로 이동시 (이벤트)
    private void StageChange()
    {
        //죽은 경우
        if (hp == 0)
        {
            PlantObject.SetActive(true);
            Debug.Log($"스테이지 이동으로 죽었던 {PlantObject.name}이/가 되살아났습니다.");

        }
        // 체력이 떨어진 경우
        else
        {
            hp = maxHp;
        Debug.Log($"스테이지 이동으로 체력이 떨어진 {PlantObject.name}이/가 회복했습니다.")
        }

    }

}
