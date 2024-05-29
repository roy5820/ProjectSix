using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public KeyCode reseKey;

    public List<int> stageLevel;//���� ���൵ �� �������� ���̵� ����
    public int nowProgress = 0;//���� ���൵
    public List<ItemData> itemDB;//������ DB
    public List<PlayerHaveItemData> playerItemDB;//�÷��̾� ���� ������ DB
    public int moneyHeld { get; set; }//���� ��
    public int startMoney = 1000;//���� ���� ��
    public CharacterStatus playerStatus = null;//�÷��̾� �������ͽ�

    //Ĩ��� ���� ���� ����
    public List<CheepInfo> cheepDataBase;//Ĩ ������  ���̽�
    public List<int> cheepInventory;//Ĩ �κ��丮

    //Ȱ��ȭ�� �̺�Ʈ ����
    private void OnEnable()
    {
        TurnEventBus.Subscribe(TurnEventType.StageClear, StageClear);//TurnStart �̺�Ʈ ����
    }

    //��Ȱ��ȭ�� �̺�Ʈ ����
    private void OnDisable()
    {
        TurnEventBus.Unsubscribe(TurnEventType.StageClear, StageClear);//TurnStart �̺�Ʈ ����
    }

    private void Start()
    {
        moneyHeld = startMoney;//������ ���۵����� �ʱ�ȭ
    }

    private void Update()
    {
        if (Input.GetKeyUp(reseKey))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    //�������� Ŭ���� �̺�Ʈ ó��
    public void StageClear()
    {
        //�������� Ŭ����� ĳ���� �������ͽ� �� �����ϱ�
        PlayerController player = BattleManager.Instance.onPlayer;
        if (player != null)
        {
            playerStatus = player._characterStatus;
        }

        nowProgress++;//���൵ ����
    }

    //������ ������ ���̽� ���� �Լ���
    //�� ���� �������� List<ItemData> �������� ����ϴ� �Լ� (number: ����� ������ ��)
    public List<PlayerHaveItemData> RandomOutputOfUnownedItems(int number)
    {
        List<PlayerHaveItemData> outputList = new List<PlayerHaveItemData>();//������ �̺��� ������ ����Ʈ
        List<PlayerHaveItemData> unownedItems = new List<PlayerHaveItemData>();//�̺��� ������ ����Ʈ
        //�̺��� �������� outputList�� �߰��ϴ� �ݺ���
        foreach (ItemData data in itemDB)
        {
            PlayerHaveItemData findData = playerItemDB.Find(item => item.itemId.Equals(data.itemId));//�÷��̾� �κ��丮�� ���� �������� ��������
            //�߰��� ������ ���� �� �� �ʱ�ȭ
            PlayerHaveItemData addData = new PlayerHaveItemData();
            addData.itemId = data.itemId;
            addData.itemLevel = 0;

            //������ ������ level�� �ִ����� üũ, �ִ��� ��� ��Ƽ��
            if (findData != null)
            {
                if (findData.itemLevel >= data.itemInfo.Count - 1)
                    continue;
                addData.itemLevel = findData.itemLevel + 1;//���� ���� ���� �������� ��� ���� ������ ���������� ����
            }

            addData.itemInfo = data.itemInfo[addData.itemLevel];//������ ������ �ʱ�ȭ

            //�̺��� �� ��� outputList�� �ش� ������ ����
            unownedItems.Add(addData);
        }

        //��� ���� ��ŭ outputList����Ʈ�� �������� ��� �߰�
        while(outputList.Count < number && unownedItems.Count > 0)
        {
            int ranNum = Random.Range(0, unownedItems.Count);//���� ���� ����
            outputList.Add(unownedItems[ranNum]);//�� �߰�
            unownedItems.RemoveAt(ranNum);
        }

        return outputList;
    }

    //���� �������� ��� List<itemInfo> �������� ����ϴ� �Լ�
    public List<ItemInfo> OutputOfHaveItems()
    {
        List<ItemInfo> outputList = new List<ItemInfo>();
        
        //playerItemDB�� ������ ���� itemDB�� ������ ������ ������ outputList�� ü��� �ݺ���
        for (int i = 0; i < playerItemDB.Count; i++)
        {
            //�÷��̾� ������ DB�� ������ ��� ������ ���ؼ� ������ ���� ������ outputList�� �߰�
            ItemData getItem = itemDB.Find(item => item.itemId.Equals(playerItemDB[i].itemId));
            outputList.Add(getItem.itemInfo[playerItemDB[i].itemLevel]);
        }
        
        return outputList;
    }

    //�÷��̾� ������ ȹ�� ���� �Լ�
    public void PlayerGetItem(int itemID, int itemLV, ItemInfo itemINFO)
    {
        Debug.Log(itemID + ", " + itemLV + ", " + itemINFO);
        PlayerHaveItemData getItem = playerItemDB.Find(item => item.itemId == itemID);//���� �������� ID���� �˻�
        
        //�̺��� �������ϰ� ����Ʈ�� ������ ���� �߰�
        if (getItem == null)
        {
            PlayerHaveItemData addItem = new PlayerHaveItemData();
            addItem.itemId = itemID;
            addItem.itemLevel = itemLV;
            addItem.itemInfo = itemINFO;
            playerItemDB.Add(addItem);
        }
        //���� �� ������ ���� ����
        else
        {
            int findItemIndex = playerItemDB.FindIndex(item => item.itemId == getItem.itemId);
            
            playerItemDB[findItemIndex].itemId = itemID;
            playerItemDB[findItemIndex].itemLevel = itemLV;
            playerItemDB[findItemIndex].itemInfo = itemINFO;
        }
    }

    //Ĩ ���� �Լ�
    //Ĩ ����Ʈ�� �������� �Լ� outType 0:��� Ĩ , 1:���� ���� Ĩ
    public List<CheepInfo> GetCheepList(int outType)
    {
        List<CheepInfo> returnList = new List<CheepInfo>();//������ Ĩ���� ����Ʈ

        switch (outType) {
            //cheepDataBase�� ��� ������ returnList�� ����
            case 0:
                returnList = cheepDataBase;
                break;
            //cheepDataBased�� ���� ���� Ĩ�� returnList�� ����
            case 1:

                break;
        }

        return returnList;//Ĩ ����Ʈ ���
    }
    
}