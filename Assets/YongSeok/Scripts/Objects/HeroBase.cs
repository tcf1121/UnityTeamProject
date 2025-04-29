using System;
using System.Linq;
using UnityEngine;
using static YongSeok.GameManager;
using YongSeok;

namespace YongSeok
{
    public class HeroBase : MonoBehaviour
    {
        [Header("Status")]
        [SerializeField] private int hp;
        public int Hp { get { return hp; } set { hp = value; } }
        // Maxhp 필요
        [SerializeField] private int maxHp;
        public int MaxHp
        {
            get { return maxHp; }
        }
        [SerializeField] private int defense;

        // 기물을 구입하기 위해 필요한 금액
        [SerializeField] private int cost;
        public int Cost
        {
            get { return cost; }
        }
        // 기물의 마나
        [SerializeField] private int mana;
        public int Mana { get { return mana; } set { mana = value; } }
        //마나 추가에 따른 매직넘버 제거용.
        [SerializeField] private int addMana = 0;
        [Header("Rank info")]
        [SerializeField] private int currentRank = 1;
        public int CurrentRank => currentRank;

        [SerializeField] private int maxRank = 10;
        public int MaxRank
        {
            get { return maxRank; }
        }


        [Header("HeroInfo")]
        ///추가로 필요할 것이라 생각되는 필드
        [SerializeField] private GameObject heroObject;
        public GameObject HeroObject => heroObject;
        //같은 종류의 hero를 구분하기 위한 heroType 선언
        [Header("hero Info")]
        [Header("Hero Info")]
        [SerializeField] private HeroType heroType;
        public HeroType HeroType => heroType;




        //랭크 업 이벤트
        public event Action OnRankUp;
        //생성 이벤트
        public event Action OnBuy;
        // 다음 스테이지 이동 이벤트
        public event Action NextStage;

        // 랭크업 관련 기물 갯수 파악용
        private int pieceCount;
        // 랭크 단계

        // 생성 위치를 인터페이스로부터 위한 자표 변수 향후 변경예정
        public Vector2Int GridPosition { get; private set; }

        //Test를 위한 선언
        [SerializeField] int damage;




        private void Start()
        {


        }

        private void Update()
        {



        }




        //데미지 받음에 따라 마나 증가 및 체력 0 오브젝트 비활성화
        private void OnDamageTaken(int damage)
        {
            Debug.Log($"데미지 {damage}을/를 받았습니다.");
            if (hp + defense - damage < 0)
            {
                hp = 0;
            }
            else if (defense < damage)
            {
                hp = hp + defense - damage;
                Debug.Log($"데미지로 인해 체력이 {hp}이/가 되었습니다.");
            }
            else
            {

            }


            mana += addMana;

            if (hp <= 0)
            {
                if (heroObject != null)
                {
                    heroObject.SetActive(false);
                    Debug.Log("채력이 0보다 낮아 죽었습니다.");

                }
            }

        }



        // 맵의 스폰 좌표 얻기
        public void Init(Vector2Int gridPos, Vector3 worldPos)
        {
            GridPosition = gridPos;
            transform.position = worldPos;
        }

        //월드맵 상 좌표로 스폰 위치 할당 Y축 위치 임의 선정
        private Vector3 GridToWorldPosition(Vector2Int gridPos)
        {
            float fixedHeight = 1f; // 원하는 높이 설정
            return new Vector3(gridPos.x, fixedHeight, gridPos.y);
        }

        //다음 스테이지로 이동시 (이벤트)
        private void OnStageChange()
        {
            //죽은 경우
            if (hp == 0)
            {
                heroObject.SetActive(true);
                Debug.Log($"스테이지 이동으로 죽었던 {heroObject.name}이/가 되살아났습니다.");

            }
            // 체력이 떨어진 경우
            else
            {

                Debug.Log($"스테이지 이동으로 체력이 떨어진 {heroObject.name}이/가 회복했습니다.");
            }

            hp = maxHp;
            NextStage?.Invoke();


        }


        public void IncreaseRank()
        {
            if (currentRank < maxRank)
            {
                currentRank++;
                Debug.Log($"{HeroType} 랭크가 {currentRank}로 상승했습니다!");
            }
            else
            {
                Debug.Log($"{HeroType}은 이미 최대 랭크({maxRank})입니다. 랭크업 불가.");
            }
        }

        //GameManger로 이동

        ////승습 랭크업 (이벤트)
        //private void RankUp()
        //{
        //
        //
        //    if (pieceCount >= 3)
        //    {
        //        Debug.Log($"3개 이상 {heroObject.name} Rank {currentRank} 기물이 있습니다. 랭크업 이벤트 발생!");
        //        OnRankUp?.Invoke();
        //        currentRank += 1;
        //        Debug.Log($"현재 랭크는 : {currentRank} 입니다.");
        //    }
        //}
        //
        //// 기물 생성 (이벤트)
        //
        //private void SpawnPiece()
        //{
        //    //금액 관련 플레이어와 상호작용 필요.
        //    //if (plyaer.CanBuy(cost)
        //    //{
        //    //생성 위치 정보 획득에 관한 논의 필요.
        //    Vector3 spawnPosition = GridToWorldPosition(GridPosition);
        //    Instantiate(heroObject, spawnPosition, Quaternion.identity);
        //
        //    OnBuy?.Invoke();
        //    Debug.Log($"{heroObject.name} 구매 완료.");
        //
        //    //}
        //}

    }
}