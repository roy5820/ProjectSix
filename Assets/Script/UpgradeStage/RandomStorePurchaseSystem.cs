using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RandomStorePurchaseSystem : MonoBehaviour
{
    GameManager _gameManager;//게임매니저
    //아이템 정보를 표시할 오브젝트 정보를 담는 클래스
    [System.Serializable]
    public class sellItemInfoDisplay
    {
        public Image itemImg; //아이템 이미지
        public Text itemName;//아이템 명
        public Text itemLv;//아이템 레벨
        public Text itemPrice;//아이템 가격
        public Text coast;//사용 코스트
        public Text offensTxt;//계수 구분
        public Text offens;//계수
        public Text effectTxt;//효과 표시 텍스트
        public PlayerHaveItemData getItemInfo;//획득할 아이템 정보
    }
    public List<sellItemInfoDisplay> sellItemInfoLIst = new List<sellItemInfoDisplay>();//화면에 보여줄 판메아이템 리스트
    public List<PlayerHaveItemData> getRandItems = new List<PlayerHaveItemData>();//
    List<bool> soldOutIndexList = new List<bool>();//품절리스트
    public Text haveMoneyTxt;//보유돈 표시 텍스트 객체
    private int haveMoney;//보유 돈
    public Sprite binItemImg = null;//빈 상태의 아이템 이미지

    public GameObject rerollBtn;//리롤 버튼 오브젝트
    private bool isReroll = true;//리롤 가능 여부

    public AudioClip buyItemSfx = null;
    public AudioClip rerollItemSfx = null;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameManager.Instance;//게임 매니저 초기화

        //초기 판매 아이템 설정
        SetItems();
    }

    private void Update()
    {
        //보유 돈 갱신
        haveMoney = _gameManager.moneyHeld;
        haveMoneyTxt.text = haveMoney.ToString();
    }

    //판매 아이템 설정 함수
    public void SetItems()
    {
        //sellItemInfoLIst의 수 만큼 랜덤 아이템 가져오기
        if(getRandItems.Count == 0)
            getRandItems = _gameManager.RandomOutputOfUnownedItems(_gameManager.itemDB.Count);


        //soldOutIndexList(품절 여부 리스트)가 빈값이면 가져온 아이템 리스트의 수 만큼  값 초기화
        if(soldOutIndexList.Count == 0 )
        {
            for (int i = 0; i < getRandItems.Count; i++)
                soldOutIndexList.Add(false);
        }

        int nowGetListIndex = getRandItems.Count-1;//현재 읽고 있는 리스트 번호
        

        //getRandItems리스트를 토대로 화면에 아이템 정보 설정
        for (int i = 0; i < sellItemInfoLIst.Count; i++)
        {
            //아이템 정보 초기값 설정
            Sprite itemImg = binItemImg;
            string itemName = "";
            string itemLv = "";
            string itemPrice = "";
            string itemCoast = "";
            string offensTxt = "공격력(%)";
            string offense = "";
            string effectDescription = "";
            PlayerHaveItemData getItemInfo = null;

            //품절 여부 체크
            if (nowGetListIndex < getRandItems.Count && !soldOutIndexList[i])
            {

                //아이템 정보값을 가져와 변수의 저장
                ItemInfo item = getRandItems[nowGetListIndex].itemInfo;
                
                
                if (item != null)
                {
                    itemImg = item.itemImg;//아이템 이미지 변경
                    itemName = item.itemName;//아이템 명 변경
                    itemLv = getRandItems[nowGetListIndex].itemLevel.ToString();//아이템 레벨;
                    itemPrice = item.price.ToString();//아이템 가격 변경
                    itemCoast = item.useCost.ToString();//아이템 코스트
                    //아이템 타입에 따른 계수구분명 변경
                    if (item.itemType == 0)
                        offensTxt = "데미지(%)";
                    else
                        offensTxt = "지속턴";
                    offense = item.offense.ToString();//아이템 계수 변경
                    effectDescription = item.storeDescription;//효과 변경
                    getItemInfo = getRandItems[nowGetListIndex];//아이템 정보 갱신
                }
                if (getRandItems.Count > sellItemInfoLIst.Count)
                    getRandItems.RemoveAt(nowGetListIndex);
                nowGetListIndex--;//읽을 리스트 번호 증가
            }

            //아이템 정보 갱신
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

    //돈을 소모하여 아이템 리롤이벤트 구현 함수
    public void OnRerollItems(int expense)
    {
        if (haveMoney >= expense && isReroll)
        {
            if(rerollItemSfx)
                SoundManger.Instance.PlaySFX(rerollItemSfx);
            _gameManager.moneyHeld -= expense;//비용 지불
            DisableReroll();
            SetItems();//리롤
        }
    }

    //아이템 구매 이벤트 처리 함수
    public void OnBuyItem(int btnIndex)
    {
        //버튼 인덱스가 유효한지 체크
        if(btnIndex < sellItemInfoLIst.Count && btnIndex >= 0)
        {
            //빈값 여부 체크
            if (sellItemInfoLIst[btnIndex].getItemInfo == null)
                return;
            int price = sellItemInfoLIst[btnIndex].getItemInfo.itemInfo.price;//아이템 가격 가져오기
            //돈지불 여부 체크
            if (price <= haveMoney)
            {
                if (buyItemSfx)
                    SoundManger.Instance.PlaySFX(buyItemSfx);
                _gameManager.moneyHeld -= price;//돈 지불
                //아이템 획득 구현
                _gameManager.PlayerGetItem
                    (sellItemInfoLIst[btnIndex].getItemInfo.itemId,
                    sellItemInfoLIst[btnIndex].getItemInfo.itemLevel,
                    sellItemInfoLIst[btnIndex].getItemInfo.itemInfo);

                //목록에서 해당 index의 아이템 목록 품절 처리
                soldOutIndexList[btnIndex] = true;

                //아이템 정보 품절 처리
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

        //모두 품절 여부에 따른 리롤 버튼 비활성화
        int soldOutCnt = 0;
        for (int i = 0; i < soldOutIndexList.Count; i++)
        {
            if (soldOutIndexList[i])
                soldOutCnt++;
        }
        if (soldOutCnt >= sellItemInfoLIst.Count)
            DisableReroll();
    }

    //리롤 비활성화
    private void DisableReroll()
    {
        isReroll = false;
        rerollBtn.GetComponent<Image>().color = Color.black;
    }
}
