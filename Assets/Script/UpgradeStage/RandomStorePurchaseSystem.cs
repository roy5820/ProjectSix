using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomStorePurchaseSystem : MonoBehaviour
{
    GameManager _gameManager;//���ӸŴ���
    //������ ������ ǥ���� ������Ʈ ������ ��� Ŭ����
    [System.Serializable]
    public class sellItemInfoDisplay
    {
        public Image itemImg; //������ �̹���
        public Text itemName;//������ ��
        public Text itemLv;//������ ����
        public Text itemPrice;//������ ����
        public Text coast;//��� �ڽ�Ʈ
        public Text offensTxt;//��� ����
        public Text offens;//���
        public Text effectTxt;//ȿ�� ǥ�� �ؽ�Ʈ
        public PlayerHaveItemData getItemInfo;//ȹ���� ������ ����
    }
    public List<sellItemInfoDisplay> sellItemInfoLIst = new List<sellItemInfoDisplay>();//ȭ�鿡 ������ �Ǹ޾����� ����Ʈ
    public List<PlayerHaveItemData> getRandItems = new List<PlayerHaveItemData>();//
    List<bool> soldOutIndexList = new List<bool>();//ǰ������Ʈ
    public Text haveMoneyTxt;//������ ǥ�� �ؽ�Ʈ ��ü
    private int haveMoney;//���� ��
    public Sprite binItemImg = null;//�� ������ ������ �̹���

    public GameObject rerollBtn;//���� ��ư ������Ʈ
    private bool isReroll = true;//���� ���� ����

    public AudioClip buyItemSfx = null;
    public AudioClip rerollItemSfx = null;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//���� �Ŵ��� �ʱ�ȭ

        //�ʱ� �Ǹ� ������ ����
        SetItems();
    }

    private void Update()
    {
        //���� �� ����
        haveMoney = _gameManager.moneyHeld;
        haveMoneyTxt.text = haveMoney.ToString();
    }

    //�Ǹ� ������ ���� �Լ�
    public void SetItems()
    {
        //sellItemInfoLIst�� �� ��ŭ ���� ������ ��������
        if(getRandItems.Count == 0)
            getRandItems = _gameManager.RandomOutputOfUnownedItems(_gameManager.itemDB.Count);


        //soldOutIndexList(ǰ�� ���� ����Ʈ)�� ���̸� ������ ������ ����Ʈ�� �� ��ŭ  �� �ʱ�ȭ
        if(soldOutIndexList.Count == 0 )
        {
            for (int i = 0; i < getRandItems.Count; i++)
                soldOutIndexList.Add(false);
        }

        int nowGetListIndex = getRandItems.Count-1;//���� �а� �ִ� ����Ʈ ��ȣ
        

        //getRandItems����Ʈ�� ���� ȭ�鿡 ������ ���� ����
        for (int i = 0; i < sellItemInfoLIst.Count; i++)
        {
            //������ ���� �ʱⰪ ����
            Sprite itemImg = binItemImg;
            string itemName = "";
            string itemLv = "";
            string itemPrice = "";
            string itemCoast = "";
            string offensTxt = "���ݷ�(%)";
            string offense = "";
            string effectDescription = "";
            PlayerHaveItemData getItemInfo = null;

            //ǰ�� ���� üũ
            if (nowGetListIndex < getRandItems.Count && !soldOutIndexList[i])
            {

                //������ �������� ������ ������ ����
                ItemInfo item = getRandItems[nowGetListIndex].itemInfo;
                
                
                if (item != null)
                {
                    itemImg = item.itemImg;//������ �̹��� ����
                    itemName = item.itemName;//������ �� ����
                    itemLv = getRandItems[nowGetListIndex].itemLevel.ToString();//������ ����;
                    itemPrice = item.price.ToString();//������ ���� ����
                    itemCoast = item.useCost.ToString();//������ �ڽ�Ʈ
                    //������ Ÿ�Կ� ���� ������и� ����
                    if (item.itemType == 0)
                        offensTxt = "������(%)";
                    else
                        offensTxt = "������";
                    offense = item.offense.ToString();//������ ��� ����
                    effectDescription = item.storeDescription;//ȿ�� ����
                    getItemInfo = getRandItems[nowGetListIndex];//������ ���� ����
                }
                if (getRandItems.Count > sellItemInfoLIst.Count)
                    getRandItems.RemoveAt(nowGetListIndex);
                nowGetListIndex--;//���� ����Ʈ ��ȣ ����
            }

            //������ ���� ����
            sellItemInfoLIst[i].itemImg.sprite = itemImg;
            sellItemInfoLIst[i].itemName.text = itemName;
            sellItemInfoLIst[i].itemLv.text = itemLv;
            sellItemInfoLIst[i].itemPrice.text = itemPrice;
            sellItemInfoLIst[i].coast.text = itemCoast;
            sellItemInfoLIst[i].offensTxt.text = offensTxt;
            sellItemInfoLIst[i].offens.text = offense;
            sellItemInfoLIst[i].effectTxt.text = effectDescription;
            sellItemInfoLIst[i].getItemInfo = getItemInfo;
        }
    }

    //���� �Ҹ��Ͽ� ������ �����̺�Ʈ ���� �Լ�
    public void OnRerollItems(int expense)
    {
        if (haveMoney >= expense && isReroll)
        {
            if(rerollItemSfx)
                SoundManger.Instance.PlaySFX(rerollItemSfx);
            _gameManager.moneyHeld -= expense;//��� ����
            DisableReroll();
            SetItems();//����
        }
    }

    //������ ���� �̺�Ʈ ó�� �Լ�
    public void OnBuyItem(int btnIndex)
    {
        //��ư �ε����� ��ȿ���� üũ
        if(btnIndex < sellItemInfoLIst.Count && btnIndex >= 0)
        {
            //�� ���� üũ
            if (sellItemInfoLIst[btnIndex].getItemInfo == null)
                return;
            int price = sellItemInfoLIst[btnIndex].getItemInfo.itemInfo.price;//������ ���� ��������
            //������ ���� üũ
            if (price <= haveMoney)
            {
                if (buyItemSfx)
                    SoundManger.Instance.PlaySFX(buyItemSfx);
                _gameManager.moneyHeld -= price;//�� ����
                //������ ȹ�� ����
                _gameManager.PlayerGetItem
                    (sellItemInfoLIst[btnIndex].getItemInfo.itemId,
                    sellItemInfoLIst[btnIndex].getItemInfo.itemLevel,
                    sellItemInfoLIst[btnIndex].getItemInfo.itemInfo);

                //��Ͽ��� �ش� index�� ������ ��� ǰ�� ó��
                soldOutIndexList[btnIndex] = true;

                //������ ���� ǰ�� ó��
                sellItemInfoLIst[btnIndex].itemImg.sprite = binItemImg;
                sellItemInfoLIst[btnIndex].itemName.text = "";
                sellItemInfoLIst[btnIndex].itemLv.text = "";
                sellItemInfoLIst[btnIndex].itemPrice.text = "";
                sellItemInfoLIst[btnIndex].coast.text = "";
                sellItemInfoLIst[btnIndex].offens.text = "";
                sellItemInfoLIst[btnIndex].effectTxt.text = "";
                sellItemInfoLIst[btnIndex].getItemInfo = null;
            }
        }

        //��� ǰ�� ���ο� ���� ���� ��ư ��Ȱ��ȭ
        int soldOutCnt = 0;
        for (int i = 0; i < soldOutIndexList.Count; i++)
        {
            if (soldOutIndexList[i])
                soldOutCnt++;
        }
        if (soldOutCnt >= sellItemInfoLIst.Count)
            DisableReroll();
    }

    //���� ��Ȱ��ȭ
    private void DisableReroll()
    {
        isReroll = false;
        rerollBtn.GetComponent<Image>().color = Color.black;
    }
}
