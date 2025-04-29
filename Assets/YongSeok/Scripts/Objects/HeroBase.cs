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
        // Maxhp �ʿ�
        [SerializeField] private int maxHp;
        public int MaxHp
        {
            get { return maxHp; }
        }
        [SerializeField] private int defense;

        // �⹰�� �����ϱ� ���� �ʿ��� �ݾ�
        [SerializeField] private int cost;
        public int Cost
        {
            get { return cost; }
        }
        // �⹰�� ����
        [SerializeField] private int mana;
        public int Mana { get { return mana; } set { mana = value; } }
        //���� �߰��� ���� �����ѹ� ���ſ�.
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
        ///�߰��� �ʿ��� ���̶� �����Ǵ� �ʵ�
        [SerializeField] private GameObject heroObject;
        public GameObject HeroObject => heroObject;
        //���� ������ hero�� �����ϱ� ���� heroType ����
        [Header("hero Info")]
        [Header("Hero Info")]
        [SerializeField] private HeroType heroType;
        public HeroType HeroType => heroType;




        //��ũ �� �̺�Ʈ
        public event Action OnRankUp;
        //���� �̺�Ʈ
        public event Action OnBuy;
        // ���� �������� �̵� �̺�Ʈ
        public event Action NextStage;

        // ��ũ�� ���� �⹰ ���� �ľǿ�
        private int pieceCount;
        // ��ũ �ܰ�

        // ���� ��ġ�� �������̽��κ��� ���� ��ǥ ���� ���� ���濹��
        public Vector2Int GridPosition { get; private set; }

        //Test�� ���� ����
        [SerializeField] int damage;




        private void Start()
        {


        }

        private void Update()
        {



        }




        //������ ������ ���� ���� ���� �� ü�� 0 ������Ʈ ��Ȱ��ȭ
        private void OnDamageTaken(int damage)
        {
            Debug.Log($"������ {damage}��/�� �޾ҽ��ϴ�.");
            if (hp + defense - damage < 0)
            {
                hp = 0;
            }
            else if (defense < damage)
            {
                hp = hp + defense - damage;
                Debug.Log($"�������� ���� ü���� {hp}��/�� �Ǿ����ϴ�.");
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
                    Debug.Log("ä���� 0���� ���� �׾����ϴ�.");

                }
            }

        }



        // ���� ���� ��ǥ ���
        public void Init(Vector2Int gridPos, Vector3 worldPos)
        {
            GridPosition = gridPos;
            transform.position = worldPos;
        }

        //����� �� ��ǥ�� ���� ��ġ �Ҵ� Y�� ��ġ ���� ����
        private Vector3 GridToWorldPosition(Vector2Int gridPos)
        {
            float fixedHeight = 1f; // ���ϴ� ���� ����
            return new Vector3(gridPos.x, fixedHeight, gridPos.y);
        }

        //���� ���������� �̵��� (�̺�Ʈ)
        private void OnStageChange()
        {
            //���� ���
            if (hp == 0)
            {
                heroObject.SetActive(true);
                Debug.Log($"�������� �̵����� �׾��� {heroObject.name}��/�� �ǻ�Ƴ����ϴ�.");

            }
            // ü���� ������ ���
            else
            {

                Debug.Log($"�������� �̵����� ü���� ������ {heroObject.name}��/�� ȸ���߽��ϴ�.");
            }

            hp = maxHp;
            NextStage?.Invoke();


        }


        public void IncreaseRank()
        {
            if (currentRank < maxRank)
            {
                currentRank++;
                Debug.Log($"{HeroType} ��ũ�� {currentRank}�� ����߽��ϴ�!");
            }
            else
            {
                Debug.Log($"{HeroType}�� �̹� �ִ� ��ũ({maxRank})�Դϴ�. ��ũ�� �Ұ�.");
            }
        }

        //GameManger�� �̵�

        ////�½� ��ũ�� (�̺�Ʈ)
        //private void RankUp()
        //{
        //
        //
        //    if (pieceCount >= 3)
        //    {
        //        Debug.Log($"3�� �̻� {heroObject.name} Rank {currentRank} �⹰�� �ֽ��ϴ�. ��ũ�� �̺�Ʈ �߻�!");
        //        OnRankUp?.Invoke();
        //        currentRank += 1;
        //        Debug.Log($"���� ��ũ�� : {currentRank} �Դϴ�.");
        //    }
        //}
        //
        //// �⹰ ���� (�̺�Ʈ)
        //
        //private void SpawnPiece()
        //{
        //    //�ݾ� ���� �÷��̾�� ��ȣ�ۿ� �ʿ�.
        //    //if (plyaer.CanBuy(cost)
        //    //{
        //    //���� ��ġ ���� ȹ�濡 ���� ���� �ʿ�.
        //    Vector3 spawnPosition = GridToWorldPosition(GridPosition);
        //    Instantiate(heroObject, spawnPosition, Quaternion.identity);
        //
        //    OnBuy?.Invoke();
        //    Debug.Log($"{heroObject.name} ���� �Ϸ�.");
        //
        //    //}
        //}

    }
}