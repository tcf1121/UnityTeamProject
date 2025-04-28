using System;
using System.Linq;
using UnityEngine;

public class HereBase : MonoBehaviour
{
    /// <summary>
    /// �Ĺ� �⺻ Status
    /// </summary>
    // �⹰�� HP
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

    ///�߰��� �ʿ��� ���̶� �����Ǵ� �ʵ�

    [SerializeField] private GameObject hereObject;
    //���� �߰��� ���� �����ѹ� ���ſ�.
    [SerializeField] private int addMana = 0;
    //��ũ �� �̺�Ʈ
    public event Action OnRankUp;
    //���� �̺�Ʈ
    public event Action OnBuy;
    // ���� �������� �̵� �̺�Ʈ
    public event Action NextStage;
    //�⹰ ����Ʈ ���� �÷��̾�� ó���� ������ ����.
    //private List<GameObject> Units = new List<GameObject>();
    // ��ũ�� ���� �⹰ ���� �ľǿ�
    private int pieceCount;
    // ��ũ �ܰ�
    [SerializeField] private int currentRank = 1;
    public int CurrentRank => currentRank;
    
    [SerializeField] private int maxRank;
    public int MaxRank
    {
        get { return maxRank; }
    }

    //���� ������ here�� �����ϱ� ���� hereType ����
    [Header("here Info")]
    [SerializeField] private string hereType = "normalFlower";
    
    // ���� ��ġ�� �������̽��κ��� ���� ��ǥ ���� ���� ���濹��
    public Vector2Int GridPosition { get; private set; }

    
    //Player ����
    //[SerializeField] private Player player;

    //Test�� ���� ����
    [SerializeField] int damage;
    //[SerializeField] private Vector2Int gridPosition;
    //public Vector2Int GridPosition => gridPosition;



    //Test�� ���� ������
    private void Start()
    {
        OnDamageTaken(damage);

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
            if (hereObject != null)
            {
                hereObject.SetActive(false);
                Debug.Log("ä���� 0���� ���� �׾����ϴ�.");

            }
        }

    }

    //�½� ��ũ�� (�̺�Ʈ)
    private void RankUp()
    {
        

        if (pieceCount >= 3)
        {
            Debug.Log($"3�� �̻� {hereObject.name} Rank {currentRank} �⹰�� �ֽ��ϴ�. ��ũ�� �̺�Ʈ �߻�!");
            OnRankUp?.Invoke();
            currentRank += 1;
            Debug.Log($"���� ��ũ�� : {currentRank} �Դϴ�.");
        }
    }

    // �⹰ ���� (�̺�Ʈ)

    private void SpawnPiece()
    {
        //�ݾ� ���� �÷��̾�� ��ȣ�ۿ� �ʿ�.
        //if (plyaer.CanBuy(cost)
        //{
            //���� ��ġ ���� ȹ�濡 ���� ���� �ʿ�.
            Vector3 spawnPosition = GridToWorldPosition(GridPosition);
            Instantiate(hereObject, spawnPosition, Quaternion.identity);

        OnBuy?.Invoke();
        Debug.Log($"{hereObject.name} ���� �Ϸ�.");

        //}
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
            hereObject.SetActive(true);
            Debug.Log($"�������� �̵����� �׾��� {hereObject.name}��/�� �ǻ�Ƴ����ϴ�.");

        }
        // ü���� ������ ���
        else
        {
            hp = maxHp;
            Debug.Log($"�������� �̵����� ü���� ������ {hereObject.name}��/�� ȸ���߽��ϴ�.");
        }

        NextStage?.Invoke();
       

    }

}
